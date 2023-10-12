using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.ResponseModel
{
    public class LakeshoreProductResponse
    {
        [JsonProperty("lkssku")]
        public string LKSSKU { get; set; }
    }
}
