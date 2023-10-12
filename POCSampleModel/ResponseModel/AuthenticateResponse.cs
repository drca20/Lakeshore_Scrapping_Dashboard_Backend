using POCSampleModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ResponseModels
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int? LastLogin { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.UserId;
            UserName = user.Email;
            Token = token;
            RefreshToken = user.RefreshToken;
            LastLogin = user.LastLogin;
        }
    }

    public class LogINUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int? LastLogin { get; set; }
    }
}
