using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.ResponseModel
{
    public class ProductResponse
    {
        [JsonProperty("compititor")]
        public string Compititor { get; set; }

        [JsonProperty("product_name")]
        public string ProductName { get; set; }

        [JsonProperty("sku")]
        public string SKU { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("Current_price")]
        public decimal CurrentPrice { get; set; }
    }
}
