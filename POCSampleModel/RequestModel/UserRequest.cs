using FluentValidation;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.RequestModels
{
    public class CreateUserRequestModelValidator : AbstractValidator<UserRequest>
    {
        public CreateUserRequestModelValidator()
        {
            this.RuleFor(m => m.FirstName).NotEmpty().NotNull().OverridePropertyName("firstName");
            this.RuleFor(m => m.LastName).NotEmpty().NotNull().OverridePropertyName("lastName");
            this.RuleFor(m => m.Email).NotEmpty().NotNull().OverridePropertyName("email");
            this.RuleFor(m => m.Password).NotEmpty().NotNull().OverridePropertyName("password");
        }
    }

    public partial class UserRequest
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }
    }
}
