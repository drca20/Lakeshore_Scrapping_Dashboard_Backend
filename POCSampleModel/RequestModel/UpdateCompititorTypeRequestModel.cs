using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.RequestModel
{
    public class UpdateCompititorTypeRequestModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("compititor_id")]
        public int CompititorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("match_type")]
        public int MatchType { get; set; }
    }

    public class CompititorTypeUpdateRequestModel
    {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("match_type")]
        public int MatchType { get; set; }
    }
}
