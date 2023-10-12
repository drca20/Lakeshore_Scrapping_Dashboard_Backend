using System.Collections.Generic;

namespace POCSampleModel.CustomModels
{
    /// <summary>
    /// SearchResult
    /// </summary>
    public interface ISearchResult<T>
    {
        /// <summary>
        /// The string that assign as Meta
        /// </summary>
        Meta Meta { get; set; }
        /// <summary>
        /// The string that assign as Results
        /// </summary>
        IList<T> Results { get; set; }
    }

}
