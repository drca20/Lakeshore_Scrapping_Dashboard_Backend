using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleModel.RequestModel
{
    public class JWTHelper
    {
        public int AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
        public string Address { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public int? LastLogin { get; set; }
        public bool? is_Seminar { get; set; }
    }
}
