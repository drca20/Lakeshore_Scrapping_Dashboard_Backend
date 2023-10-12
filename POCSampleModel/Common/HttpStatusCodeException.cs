using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.Common
{
    public class HttpStatusCodeException : Exception
    {
        public int StatusCode { get; set; }
        public string ContentType { get; set; } = @"text/plain";
        public string Code { get; set; }

        public HttpStatusCodeException(int statusCode)
        {
            this.StatusCode = statusCode;
        }

        public HttpStatusCodeException(int statusCode, string message, string Code = "0") : base(message)
        {
            this.ContentType = @"application/json";
            this.StatusCode = statusCode;
            this.Code = Code;
        }

        public HttpStatusCodeException(int statusCode, Exception inner, string Code = "0") : this(statusCode, inner.ToString(), Code) { }

        public HttpStatusCodeException(int statusCode, JObject errorObject, string Code = "0") : this(statusCode, errorObject.ToString(), Code)
        {
            this.ContentType = @"application/json";
        }
    }
}
