using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.ResponseModel
{
    public class CompititorResponseModel
    {
        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("product_url")]
        public string ProductUrl { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("pro_img")]
        public List<string> ProductsImages { get; set; }
        public List<CompititorProductResponseModel> CompititorProduct { get; set; }
    }

    public class CompititorProductResponseModel
    {

        [JsonProperty("compititor_product_id")]
        public int CompititorProductID { get; set; }

        [JsonProperty("compititor_product_name")]
        public string CompititorProductName { get; set; }

        [JsonProperty("compititor_product_url")]
        public string CompititorProductUrl { get; set; }

        [JsonProperty("compititor_price")]
        public string CompititorPrice { get; set; }

        [JsonProperty("match_type")]
        public int MatchType { get; set; }

        [JsonProperty("com_image")]
        public List<string> CompititorImages { get; set; }
    }
}
