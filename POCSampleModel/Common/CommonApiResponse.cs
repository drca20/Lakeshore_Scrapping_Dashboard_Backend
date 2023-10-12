using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.Common
{
    public class CommonApiResponse
    {
        public object Content { get; set; }
        public int StatusCode { get; set; }

        public int ErrorCode { get; set; }
        public string Errormessage { get; set; }
    }
    public class NotFoundAPIResponse
    {
        /// <summary>
        /// (Conditional) An error code to find help for the exception.
        /// </summary>
        [JsonProperty("code")]

        public int code { get; set; }

        /// <summary>
        /// A more descriptive message regarding the exception.
        /// </summary>
        [JsonProperty("message")]
        public string message { get; set; }

        /// <summary>
        /// The HTTP status code for the exception.
        /// </summary>
        [JsonProperty("status")]
        public int status { get; set; }

    }
}
