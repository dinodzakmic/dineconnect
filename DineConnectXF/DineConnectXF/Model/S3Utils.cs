using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace DineConnectXF.Model
{
    public class S3Utils
    {
        public TransferUtility s3transferUtility;
        public static AmazonS3Client S3Client;

        public S3Utils()
        {
        }

        public void SetupAwsCredentials()
        {

            CognitoAWSCredentials credentials = new CognitoAWSCredentials(
                CONSTANTS.COGNITO_POOL_ID, CONSTANTS.REGION
            );

            var config = new AmazonS3Config() { RegionEndpoint = Amazon.RegionEndpoint.EUWest1, Timeout = TimeSpan.FromSeconds(30), UseHttp = true };

            AWSConfigsS3.UseSignatureVersion4 = true;

            S3Client = new AmazonS3Client(credentials, config);
            this.s3transferUtility = new TransferUtility(S3Client);

        }

        public async Task UploadPhoto(string filename)
        {

            try
            {

                var documentsPath = "";
                var filePath = Path.Combine(documentsPath, filename);

                TransferUtilityUploadRequest request = new TransferUtilityUploadRequest { BucketName = CONSTANTS.BUCKET_NAME, FilePath = filePath, Key = filePath, ContentType = "image/png" };

                CancellationToken cancellationToken = new CancellationToken();

                await this.s3transferUtility.UploadAsync(request, cancellationToken);

            }
            catch (AmazonS3Exception s3Exception)
            {

                Debug.WriteLine(s3Exception.StackTrace);
            }
        }
    }
}
