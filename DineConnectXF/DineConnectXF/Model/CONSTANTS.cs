using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Amazon;

namespace DineConnectXF.Model
{
    public class CONSTANTS
    {

        // You should replace these values with your own
        public const string COGNITO_POOL_ID = "us-west-2:0a37fd86-28bc-4c26-80c5-0b3b5bd3c9ad";


        // Note, the bucket will be created in all lower case letters
        // If you don't enter an all lower case title, any references you add
        // will need to be sanitized
        public const string BUCKET_NAME = "DinePlan";

        public static RegionEndpoint REGION = RegionEndpoint.USWest2;

        public const HttpStatusCode NO_SUCH_BUCKET_STATUS_CODE = HttpStatusCode.NotFound;
        public const HttpStatusCode BUCKET_ACCESS_FORBIDDEN_STATUS_CODE = HttpStatusCode.Forbidden;
        public const HttpStatusCode BUCKET_REDIRECT_STATUS_CODE = HttpStatusCode.Redirect;

    }
}
