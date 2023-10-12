﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResponseModels
{
    public partial class UserResponse
    {
        [JsonProperty("user_id")]
        public int UserId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        //[JsonProperty("password")]
        //public string Password { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("profile")]
        public string Profile { get; set; }
    }
}
