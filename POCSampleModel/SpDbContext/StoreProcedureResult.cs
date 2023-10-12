using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.SpDbContext
{
    /// <summary>
    /// 
    /// </summary>
    public class ExecutreStoreProcedureResult
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Result { get; set; }

    }

    public class ExecutreStoreProcedureResultNew
    {
      
        /// <summary>
        /// 
        /// </summary>
        public string RESULT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalPages { get; set; }


    }

    public class ExecutreStoreProcedureResultNewDemo
    {

        /// <summary>
        /// 
        /// </summary>
        public List<Demo> RESULT { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalPages { get; set; }
    }


    public class Demo {
        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("lakeshorepro_id")]
        public string productid { get; set; }
        [JsonProperty("LKS_product_name")]
        public string productname { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ExecutreStoreProcedureResultWithEntitySID
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string EntitiySID { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ExecutreStoreProcedureResultWithSID
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SID { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public class ExecuteStoreProcedureResultWithId
    {
        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }
    }

    public class ExecutreStoreProcedureResultList
    {
        //public string ErrorMessage { get; set; }

        public string Result { get; set; }
        //public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int TotalPages { get; set; }
    }
}
