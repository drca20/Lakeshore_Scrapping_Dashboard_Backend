using System;

namespace common
{
    public class AppConfiguration
    {

        public static string BlobStorageConnectionString = "";
        public static string BlobstorageContainers = "";
        public static string EmailConfirmationURL = "";
        public static string ForgotPaswordUrl = "";
        public static string JwtKey = "";
        public static string JwtIssuer = "";
        public static string JwtAudience = "";
        public static string token = "";

        public static string SaltKey = "";

        public static string SendGridApiKey;
        public static string SendGridFromEmail;
        public static string SendGridFromName;
        public static string SendGridCCEmail;
        public static string PostmarkKey;

        public static string PostmarkFromMail;
        public static long AllowedDocumentMaxFileSize = 0;
        public static string BulkMessgaeToEmailAddress = "";
        //"anyaddress@337Storage.com"




        public static string GalleryCDNStoragePath = "";

        static AppConfiguration()
        {
            var Configuration = ConfigHelper.GetConfig();
            BlobStorageConnectionString = !string.IsNullOrWhiteSpace(Configuration["BlobStorageConnection"]) ? Configuration["BlobStorageConnection"] : "";

            EmailConfirmationURL = !string.IsNullOrWhiteSpace(Configuration["EmailConfirmationURL"]) ? Configuration["EmailConfirmationURL"] : "";
            ForgotPaswordUrl = !string.IsNullOrWhiteSpace(Configuration["ForgotPaswordUrl"]) ? Configuration["ForgotPaswordUrl"] : "";

            JwtIssuer = !string.IsNullOrWhiteSpace(Configuration["JwtIssuer"]) ? Configuration["JwtIssuer"] : "";
            JwtKey = !string.IsNullOrWhiteSpace(Configuration["JwtKey"]) ? Configuration["JwtKey"] : "";
            JwtAudience = !string.IsNullOrWhiteSpace(Configuration["JwtAudience"]) ? Configuration["JwtAudience"] : "";
            token = !string.IsNullOrWhiteSpace(Configuration["token"]) ? Configuration["token"] : "";
           
            SaltKey = !string.IsNullOrWhiteSpace(Configuration["SaltKey"]) ? Configuration["SaltKey"] : "";
           
            GalleryCDNStoragePath = !string.IsNullOrWhiteSpace(Configuration["GalleryCDNStoragePath"]) ? Configuration["GalleryCDNStoragePath"] : "";
           
            SendGridApiKey = !string.IsNullOrWhiteSpace(Configuration["SendGridApiKey"]) ? Configuration["SendGridApiKey"] : "";
            SendGridFromEmail = !string.IsNullOrWhiteSpace(Configuration["SendGridFromEmail"]) ? Configuration["SendGridFromEmail"] : "";
            SendGridCCEmail = !string.IsNullOrWhiteSpace(Configuration["SendGridCCEmail"]) ? Configuration["SendGridCCEmail"] : "";
            SendGridFromName = !string.IsNullOrWhiteSpace(Configuration["SendGridFromName"]) ? Configuration["SendGridFromName"] : "";


            PostmarkKey = !string.IsNullOrWhiteSpace(Configuration["PostmarkKey"]) ? Configuration["PostmarkKey"] : "";
            PostmarkFromMail = !string.IsNullOrWhiteSpace(Configuration["PostmarkFromMail"]) ? Configuration["PostmarkFromMail"] : "";
            BulkMessgaeToEmailAddress = !string.IsNullOrWhiteSpace(Configuration["BulkToEmailAddress"]) ? Configuration["BulkToEmailAddress"] : "";

            AllowedDocumentMaxFileSize = !string.IsNullOrWhiteSpace(Configuration["AllowedDocumentMaxFileSize"]) ? Convert.ToInt64(Configuration["AllowedDocumentMaxFileSize"]) : 51200;

        }
    }
}
