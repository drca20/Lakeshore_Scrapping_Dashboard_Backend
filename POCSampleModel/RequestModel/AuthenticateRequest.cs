using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestModels
{
    public class CreateAuthenticateLoginRequestModelValidator : AbstractValidator<AuthenticateLoginRequest>
    {
        public CreateAuthenticateLoginRequestModelValidator()
        {
            this.RuleFor(m => m.email).NotEmpty().NotNull().OverridePropertyName("email");
            this.RuleFor(m => m.Password).NotEmpty().NotNull().OverridePropertyName("Password");
        }
    }
    
    public class CreateAuthenticateRefereshTokenRequestModelValidator : AbstractValidator<AuthenticateRefereshTokenRequest>
    {
        public CreateAuthenticateRefereshTokenRequestModelValidator()
        {
            this.RuleFor(m => m.email).NotEmpty().NotNull().OverridePropertyName("email");
            this.RuleFor(m => m.Password).NotEmpty().NotNull().OverridePropertyName("Password");
            this.RuleFor(m => m.RefreshToken).NotEmpty().NotNull().OverridePropertyName("RefreshToken");
        }
    }
    public class AuthenticateLoginRequest
    {
        public string email { get; set; }

        public string Password { get; set; }
        //public string ResetPasswordToken { get; set; }
        //public string RefreshToken { get; set; }
    }
    
    public class AuthenticateRefereshTokenRequest
    {
        public string email { get; set; }

        public string Password { get; set; }
        //public string ResetPasswordToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
