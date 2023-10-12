using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.RequestModel
{
    public class ProductRequest
    {
        [JsonProperty("compititor_id")]
        public int CompititorId { get; set; }

        [JsonProperty("product_name")]
        public string Name { get; set; }

        [JsonProperty("sku")]
        public string SKU { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("Current_price")]
        public decimal CurrentPrice { get; set; }
    }

    public class ProductPriceUpdateRequest
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }
    }

    public class ProductMapTypeUpdateRequest
    {
        [JsonProperty("type")]
        public int Type { get; set; }
    }

    public class ProductList
    {
        [JsonProperty("page_number")]
        public int PageNumber { get; set; }

        [JsonProperty("page_size")]
        public int PageSize { get; set; }

        [JsonProperty("search_text")]
        public string SearchText { get; set; }

        [JsonProperty("filters")]
        public string Filters { get; set; }

        [JsonProperty("sort_column")]
        public string SortColumn { get; set; }

        [JsonProperty("sort_order")]
        public string SortOrder { get; set; }
    }
    public class ProductMapCreate
    {
        [JsonProperty("base_sku")]
        public string BaseSKU { get; set; }

        [JsonProperty("target_sku")]
        public string TargetSKU { get; set; }

        [JsonProperty("match_type")]
        public string MatchType { get; set; }
    }

}
