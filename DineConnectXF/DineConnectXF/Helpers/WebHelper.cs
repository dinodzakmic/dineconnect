using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Amazon.S3;
using Amazon.S3.Model;
using DineConnectXF.Model;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;

namespace DineConnectXF.Helpers
{
    public class WebHelper
    {
        public class UserLogin
        {
            public string tenancyName { get; set; }
            public string usernameOrEmailAddress { get; set; }
            public string password { get; set; }
        }

        private string _authString = "api/Account/Authenticate";
        private string _getPurcString = "api/services/app/location/ApiGetPurchaseLocationForUser";
        private string _getSuppString = "api/services/app/supplier/ApiGetSupplier";
        private string _getAllString = "api/services/app/uploadDocument/GetAll";
        private string _createString = "api/services/app/uploadDocument/CreateOrUpdateUploadDocument";

        private static WebHelper _instance;

        protected WebHelper(string url, User user)
        {
            this._url = url;
            this._user = user;
            this._client = new HttpClient();
            Uri uri = new Uri(this._url);
            this._client.BaseAddress = uri;
            this._client.DefaultRequestHeaders.Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this._login = new UserLogin
            {
                tenancyName = _user.TenacyName,
                usernameOrEmailAddress = _user.UsernameOrEmail,
                password = _user.Password
            };
        }

        public static WebHelper getInstance(string url, User user)
        {
            _instance = new WebHelper(url, user);
            return _instance;
        }

        public static WebHelper getInstance()
        {
            return _instance;
        }

        private string _url;
        User _user;
        HttpClient _client;
        UserLogin _login;

        public User GetUser()
        {
            return _user;
        }

        public static Locations Locations = new Locations() {Error =  new Error(), Result = new LocationResult() {Items = new List<Location>()} };
        public static Suppliers Suppliers = new Suppliers() {Error = new Error(), Result = new SupplierResult() {Items = new List<Supplier>()} };
        public async Task<bool> Auth()
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                string data = JsonConvert.SerializeObject(_instance._login);
                responseMessage = await _instance._client.PostAsync(_instance._authString, (HttpContent)new StringContent(data, System.Text.Encoding.UTF8, "application/json"));
                string response = await responseMessage.Content.ReadAsStringAsync();
                User user = JsonConvert.DeserializeObject<User>(response);
                if (user.Error != null)
                    return false;
                _instance._user.UnauthorizedRequest = user.UnauthorizedRequest;
                _instance._user.UserId = user.UserId;
                _instance._user.Result = user.Result;
                _instance._user.Error = user.Error;
                _instance._user.TenantId = user.TenantId;
                string token = "Bearer " + user.Result;
                _instance._client.DefaultRequestHeaders.Add("Authorization", token);
                Locations = await PurchLoc();
                Suppliers = await GetSuppliers();
                return true;

            }
            catch (Exception ex)
            {
                if (responseMessage == null)
                {
                    responseMessage = new HttpResponseMessage();
                }
                responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
                return false;
            }
        }

        public async Task<Locations> PurchLoc()
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                string userId = "{\n\t\"userId\": " + _instance._user.UserId + "\n}";
                responseMessage = await _instance._client.PostAsync(_instance._getPurcString, (HttpContent)new StringContent(userId, System.Text.Encoding.UTF8, "application/json"));
                string response = await responseMessage.Content.ReadAsStringAsync();
                Locations locations = JsonConvert.DeserializeObject<Locations>(response);
                return locations;
            }
            catch (Exception ex)
            {
                if (responseMessage == null)
                {
                    responseMessage = new HttpResponseMessage();
                }
                responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
                return null;
            }
        }

        public async Task<Suppliers> GetSuppliers()
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                string userId = "{\n\t\"tenantId\": " + _instance._user.TenantId + "\n}";
                responseMessage = await _instance._client.PostAsync(_instance._getSuppString, (HttpContent)new StringContent(userId, System.Text.Encoding.UTF8, "application/json"));
                string response = await responseMessage.Content.ReadAsStringAsync();
                Suppliers suppliers = JsonConvert.DeserializeObject<Suppliers>(response);
                return suppliers;
            }
            catch (Exception ex)
            {
                if (responseMessage == null)
                {
                    responseMessage = new HttpResponseMessage();
                }
                responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
                return null;
            }
        }

        public async Task<Documents> Documents(SearchDocument document)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                responseMessage = await _instance._client.PostAsync(_instance._getAllString, (HttpContent)new StringContent(JsonConvert.SerializeObject(document), System.Text.Encoding.UTF8, "application/json"));
                string response = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Documents>(response);
            }
            catch (Exception ex)
            {
                if (responseMessage == null)
                {
                    responseMessage = new HttpResponseMessage();
                }
                responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
                return null;
            }
        }

        public async Task<Documents> SendDocument(UploadDocument document)
        {
            HttpResponseMessage responseMessage = null;
            try
            {
                responseMessage = await _instance._client.PostAsync(_instance._getAllString, (HttpContent)new StringContent(JsonConvert.SerializeObject(document), System.Text.Encoding.UTF8, "application/json"));
                string response = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Documents>(response);
            }
            catch (Exception ex)
            {
                if (responseMessage == null)
                {
                    responseMessage = new HttpResponseMessage();
                }
                responseMessage.StatusCode = HttpStatusCode.InternalServerError;
                responseMessage.ReasonPhrase = string.Format("RestHttpClient.SendRequest failed: {0}", ex);
                return null;
            }
        }

        //private MediaFile _file;
        //private async void Upload()
        //{
        //    if (_file == null)
        //    {
        //        UserDialogs.Instance.Toast("No file to upload");
        //        return;
        //    }

        //    var s3Client = S3Utils.S3Client;
        //    try
        //    {
        //        var response = await s3Client.PutObjectAsync(new PutObjectRequest()
        //        {
        //            BucketName = CONSTANTS.BUCKET_NAME.ToLowerInvariant(),
        //            FilePath = _file.Path,
        //            Key = _file.Path
        //        });

        //        UserDialogs.Instance.Toast("File uploaded to S3 Bucket");

        //    }
        //    catch (AmazonS3Exception s3Exception)
        //    {
        //        UserDialogs.Instance.Toast("Upload failed, check logs for more information");
        //        Debug.WriteLine(s3Exception.StackTrace);
        //    }
        //}
    }
}
