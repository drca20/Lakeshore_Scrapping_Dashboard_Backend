using System.Collections.Generic;

namespace POCSampleModel.CustomModels
{
    /// <summary>
    /// SearchResult
    /// </summary>
    public class SearchResult<T> : ISearchResult<T>
    {
        /// <summary>
        /// Search string to look up for matching results. 
        /// </summary>
        public IList<T> Results { get; set; }

        /// <summary>
        /// Initialization Meta
        /// </summary>
        public Meta Meta { get; set; }

    }
}
