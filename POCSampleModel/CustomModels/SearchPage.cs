using Newtonsoft.Json;
using System.Collections.Generic;

namespace POCSampleModel.CustomModels
{
    /// <summary>
    /// SearchPage
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SearchPage<T>
    {
        /// <summary>
        /// Initialization SearchPage
        /// </summary>
        public SearchPage()
        {
            List = new List<T>();
            Meta = new Meta();
        }

        /// <summary>
        /// Initialization Meta
        /// </summary>
        [JsonProperty(PropertyName = "meta")]

        public Meta Meta { get; set; }

        /// <summary>
        /// Initialization List
        /// </summary>
        [JsonProperty(PropertyName = "results")]
        public List<T> List { get; set; }

    }
    /// <summary>
    /// Meta
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// The current page number. the first page is 1.
        /// </summary>
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }

        /// <summary>
        /// Page size of the result set.
        /// </summary>
        [JsonProperty(PropertyName = "page_size")]
        public int PageSize { get; set; }
        /// <summary>
        /// Resource key name.
        /// </summary>
        [JsonProperty(PropertyName = "key")]
        public string? Key { get; set; }
        /// <summary>
        /// The URL of the current page.
        /// </summary>
        [JsonProperty(PropertyName = "url")]
        public string? Url { get; set; }
        /// <summary>
        /// The URL for the first page of this list.
        /// </summary>
        [JsonProperty(PropertyName = "first_page_url")]
        public string? FirstPageUrl { get; set; }
        /// <summary>
        /// The URL for the previous page of this list.
        /// </summary>
        [JsonProperty(PropertyName = "previous_page_url")]
        public string? PreviousPageUrl { get; set; }
        /// <summary>
        /// The URL for the next page of this list.
        /// </summary>
        [JsonProperty(PropertyName = "next_page_url")]
        public string? NextPageUrl { get; set; }
        /// <summary>
        /// Total Count of results.
        /// </summary>
        [JsonProperty(PropertyName = "total_results")]
        public int TotalResults { get; set; }

        /// <summary>
        /// Total No of pages of the result set.
        /// </summary>
        [JsonProperty(PropertyName = "total_page_num")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Total No of pages of the NextPageExists.
        /// </summary>
        [JsonIgnore]
        public bool NextPageExists { get; set; }
    }
}
