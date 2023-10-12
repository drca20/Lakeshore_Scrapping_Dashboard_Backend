using Newtonsoft.Json;
using System.Drawing.Printing;

namespace POCSampleModel.CustomModels
{
    /// <summary>
    /// SearchRequestModel
    /// </summary>
    public class SearchRequestModel
    {
        /// <summary>
        /// Initialization SearchRequestModel
        /// </summary>
        public SearchRequestModel()
        {
            Page = 1;
            PageSize = 5;
        }

        /// <summary>
        /// Search string to look up for matching results. 
        /// </summary>
        [JsonProperty(PropertyName = "search_text")]
        public string? SearchText { get; set; }

        /// <summary>
        /// Expected page number in the result set.
        /// </summary>
        [JsonProperty(PropertyName = "page")]

        public int Page { get; set; }

        /// <summary>
        /// Page size of the result set.
        /// </summary>
        [JsonProperty(PropertyName = "page_size")]


        public int PageSize { get; set; }

        /// <summary>
        /// The column / attribute by which the results shall be sorted.
        /// </summary>
        [JsonProperty(PropertyName = "sort_column")]
        public string? SortColumn { get; set; }

        /// <summary>
        /// The order by which the results shall be sorted.  Possible values are 'asc' for ascending order, 'desc' for descending order.
        /// </summary>
        [JsonProperty(PropertyName = "sort_order")]

        public string? SortOrder { get; set; }

        /// <summary>
        /// Search filter list to look up for matching results. If must be in format '[{key:'keyname',value:'keyvalue'},{key:'keyname',value:'keyvalue'}]'.
        /// </summary>
        public string? Filters { get; set; }
    }
    /// <summary>
    /// FilterRequestModel
    /// </summary>
    public class FilterRequestModel
    {
        /// <summary>
        /// The string that assign as Key
        /// </summary>
        [JsonProperty(PropertyName = "key", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Key { get; set; }

        /// <summary>
        /// The string that assign as Condition
        /// </summary>
        [JsonProperty(PropertyName = "condition", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Condition { get; set; }

        /// <summary>
        /// The string that assign as Value
        /// </summary>
        [JsonProperty(PropertyName = "value", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object? Value { get; set; }
        /// <summary>
        /// The string that assign as From
        /// </summary>
        [JsonProperty(PropertyName = "from", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object? From { get; set; }
        /// <summary>
        /// The string that assign as To
        /// </summary>
        [JsonProperty(PropertyName = "to", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object? To { get; set; }

        /// <summary>
        /// The string that assign as Type
        /// </summary>
        [JsonProperty(PropertyName = "type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string? Type { get; set; }
    }





    public class NewSearchRequestModel {
        public NewSearchRequestModel()
        {
            Page = 1;
            PageSize = 5;
        }

        /// <summary>
        /// Expected page number in the result set.
        /// </summary>
        //[JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        /// <summary>
        /// Page size of the result set.
        /// </summary>
        //[JsonProperty(PropertyName = "page_size")]
        public int PageSize { get; set; }

        ///// <summary>
        ///// Search string to look up for matching results. 
        ///// </summary>
        //[JsonProperty(PropertyName = "search_text")]
        public string SearchText { get; set; } = "null";

        /// <summary>
        /// Search filter list to look up for matching results. If must be in format '[{key:'keyname',value:'keyvalue'},{key:'keyname',value:'keyvalue'}]'.
        /// </summary>
        public string? Filters { get; set; }

        /// <summary>
        /// The column / attribute by which the results shall be sorted.
        /// </summary>
        //[JsonProperty(PropertyName = "sort_column")]
        public string? SortColumn { get; set; }

        /// <summary>
        /// The order by which the results shall be sorted.  Possible values are 'asc' for ascending order, 'desc' for descending order.
        /// </summary>
        //[JsonProperty(PropertyName = "sort_order")]

        public string? SortOrder { get; set; }
    }

}
