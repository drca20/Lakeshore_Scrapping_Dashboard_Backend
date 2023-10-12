using Models.RequestModels;
using Models.ResponseModels;
using POCSampleModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleService.POCSampleRepository.Interface
{
    public interface IUserRepository
    {
        Task<List<User>> Get();
        Task Delete(int id);
        Task<AuthenticateResponse> AuthenticateLogin(AuthenticateLoginRequest model);
        Task<AuthenticateResponse> AuthenticateRefreshToken(AuthenticateRefereshTokenRequest model);
        Task<LogINUser> GetLoggedinUser(int userid);
        Task<UserResponse> Create(UserRequest model, string file);

    }
}
