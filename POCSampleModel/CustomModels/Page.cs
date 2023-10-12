using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace POCSampleModel.CustomModels
{
    /// <summary>
    /// Page
    /// </summary>
    public class Page
    {
        /// <summary>
        /// Initialization Page
        /// </summary>
        public Page()
        {
            Meta = new Meta();
            Result = new JArray();
        }

        /// <summary>
        /// Initialization Meta
        /// </summary>
        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }

        /// <summary>
        /// Initialization Result
        /// </summary>
        [JsonProperty(PropertyName = "results")]
        public Object Result { get; set; }

    }


    /// <summary>
    /// 
    /// </summary>
    public class ConnectionPage
    {
        /// <summary>
        /// 
        /// </summary>
        public ConnectionPage()
        {
            Meta = new Meta();
            Result = new JArray();
        }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "meta")]
        public Meta Meta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "results")]
        public Object Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "total_invitation_count")]
        public long totalInvitationCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "total_sent_count")]
        public long totalInvitationSentCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty(PropertyName = "total_receive_count")]
        public long totalInvitationReceiveCount { get; set; }
    }
}
