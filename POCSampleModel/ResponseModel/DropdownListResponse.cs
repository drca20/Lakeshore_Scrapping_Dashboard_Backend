using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.ResponseModel
{
    public class DropdownListResponse
    {
        /// <summary>
        /// The unique string that we created to identify Entity.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }



        /// <summary>
        /// The string that assigend a value of key.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }


        
    }
 
}
