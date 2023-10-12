using common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Models.RequestModels;
using Models.ResponseModels;
using Newtonsoft.Json;
using POCSampleModel.Common;
using POCSampleModel.Models;
using POCSampleModel.RequestModel;
using POCSampleService.POCSampleRepository.Interface;
using POCSampleService.UnitOfWork;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static POCSampleModel.Common.Constants;

namespace POCSampleService.POCSampleRepository.Implementation
{
    public class UserRepository : IUserRepository
    {
        public IUnitOfWork _ContextRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public UserRepository(IUnitOfWork contextRepository, IHostingEnvironment hostingEnvironment)
        {
            _ContextRepository = contextRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<List<User>> Get()
        {
            var users = await _ContextRepository.GetRepository<User>().GetAllAsync();

            //return users.Where(s => s.Status == (int)DBStatus.Active && s.IsAdmin == false).OrderByDescending(s => s.Id).ToList();
            return users.Where(s => s.Status == (int)DBStatus.Active).OrderByDescending(s => s.UserId).ToList();
        }

        public async Task Delete(int id)
        {
            var User = await Get(id);
            User.Status = (int)DBStatus.Delete;
            _ContextRepository.GetRepository<User>().Update(User);
            await _ContextRepository.CommitAsync();
        }

        public async Task<User> Get(int id)
        {
            var user = await _ContextRepository.GetRepository<User>().SingleOrDefaultAsync(t => t.UserId == id && t.Status == (int)DBStatus.Active);
            if (user == null)
            {
                throw new HttpStatusCodeException(400, "User not found");
            }
            return user;
        }

        #region Login
        public async Task<AuthenticateResponse> AuthenticateLogin(AuthenticateLoginRequest model)
        {
            var user = await _ContextRepository.GetRepository<User>().SingleOrDefaultAsync(x => x.Email.ToLower() == model.email.ToLower());
            if (user != null)
            {
                if (string.IsNullOrWhiteSpace(user.RefreshToken) || DateTime.UtcNow > user.RefreshTokenExpiry)
                {
                    user.RefreshToken = Guid.NewGuid().ToString();
                    user.RefreshTokenExpiry = DateTime.UtcNow.Add(TimeSpan.FromMinutes(300));
                    _ContextRepository.GetRepository<User>().Update(user);
                    await _ContextRepository.CommitAsync();
                }
            }

            return CommonAuthenticateMethod(model.Password, user, false);
        }

        private AuthenticateResponse CommonAuthenticateMethod(string Password, User? user, bool isrefreshtoken)
        {
            // return null if user not found
            if (user == null) return null;

            var decryptPassword = StringCipher.Decrypt(user.Password);
            var token = "";

            if (decryptPassword == Password || isrefreshtoken == true)
            {
                token = GenerateJSONWebToken(user);
            }
            else
            {
                return null;
            }
            // authentication successful so generate jwt token

            return new AuthenticateResponse(user, token);
        }
        #endregion

        #region Refresh Token
        public async Task<AuthenticateResponse> AuthenticateRefreshToken(AuthenticateRefereshTokenRequest model)
        {
            var user = await _ContextRepository.GetRepository<User>().SingleOrDefaultAsync(x => x.Email.ToLower() == model.email.ToLower() && model.RefreshToken == x.RefreshToken);
            if (user != null && !string.IsNullOrEmpty(user.RefreshToken))
            {
                if (user.RefreshTokenExpiry < DateTime.UtcNow)
                {
                    user.RefreshToken = null;
                    _ContextRepository.GetRepository<User>().Update(user);
                    await _ContextRepository.CommitAsync();
                    throw new HttpStatusCodeException(StatusCodes.Status401Unauthorized, "Token Expired");
                }
            }

            return CommonAuthenticateMethod(model.Password, user, true);

            // return null if user not found
            //if (user == null) return null;

            //var decryptPassword = StringCipher.Decrypt(user.Password);
            //var token = "";

            //if (decryptPassword == model.Password || isrefresh_token == true)
            //{
            //    token = GenerateJSONWebToken(user);
            //}
            //else
            //{
            //    return null;
            //}
            //// authentication successful so generate jwt token

            //return new AuthenticateResponse(user, token);
        }
        #endregion

        #region GenerateJSONWebToken
        private string GenerateJSONWebToken(User userInfo)
        {
            var userData = new JWTHelper();

            userData.Email = userInfo.Email;
            userData.FirstName = userInfo.FirstName;
            userData.LastName = userInfo.LastName;
            userData.AccountId = userInfo.UserId;
            //userData.IsAdmin = false;
            //userData.is_Seminar = false;
            userData.LastLogin = userInfo.LastLogin == null ? 1 : userInfo.LastLogin;

            var userDataString = JsonConvert.SerializeObject(userData);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                     new Claim("AccountData",userDataString)
                }),
                Expires = DateTime.UtcNow.AddMinutes(90),
                Audience = AppConfiguration.JwtAudience,
                Issuer = AppConfiguration.JwtIssuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfiguration.JwtKey)), SecurityAlgorithms.HmacSha256Signature)
            };


            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(securityToken);


            return token;
        }
        #endregion


        #region GetLogin UserDetails
        public async Task<LogINUser> GetLoggedinUser(int userid)
        {

            var user = await _ContextRepository.GetRepository<User>().SingleOrDefaultAsync(x => x.UserId == userid && x.Status != (int?)DBStatus.Delete);
            if (user == null)
            {
                throw new HttpStatusCodeException(400, "User not found");

            }
            LogINUser logINUser = new LogINUser
            { Id = user.UserId, LastLogin = user.LastLogin == null ? 1 : 2, UserName = user.FirstName };

            return logINUser;
        }
        #endregion

        #region Create User
        public async Task<UserResponse> Create(UserRequest model, string file)
        {

            var datetimesting = DateTime.Now.ToString("yyyyMMddHHmmssffff");

            //model.Password = "12345678";
            //model.Email = datetimesting+"@email.com";
            #region check is email and username is unique 
            var IsUnique = await ValidateEmail(model.Email);
            if (IsUnique == false)
            {
                throw new HttpStatusCodeException(StatusCodes.Status404NotFound, "Email already exists please log in");
            }
            #endregion check is email and username is unique 
            var user = CommonHelper.ToDocumentData<UserRequest, User>(model);
            user.Password = StringCipher.Encrypt(model.Password);
            //user.Email = model.Email;
            user.Profile = file;
            user.Status = (int)DBStatus.Active;
            user.CreatedDateTime = DateTime.UtcNow;
            user.ModifiedDateTime = DateTime.UtcNow;
            //user.IsAdmin = false;
            //user.ChurchName = model.ChurchName;
            //user.Address = model.Address;
            //user.SizeOfTheChurch = model.SizeOfTheChurch;
            await _ContextRepository.GetRepository<User>().InsertAsync(user);
            await _ContextRepository.CommitAsync();
            //var body = @"<p>WellCome TO iTIM</p>";
            //await _sendMail.Send(model.Email, "WelCome", body);

            #region Response 
            UserResponse userResponse = new UserResponse()
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Profile = user.Profile
            };
            #endregion
            return userResponse;
        }
        #endregion Add user

        public async Task<bool> ValidateEmail(string email)
        {
            var data = await this.CheckExistingEmail(email, null);
            bool IsUnique;
            if (data != true)
            {
                IsUnique = false;
            }
            else
            {
                IsUnique = true;
            }
            return IsUnique;
        }

        public async Task<bool> CheckExistingEmail(string email, int? Id)
        {
            User user;

            if (Id <= 0)
                user = await _ContextRepository.GetRepository<User>().SingleOrDefaultAsync(t => t.Email.ToLower() == email.ToLower() && t.Status == (int)DBStatus.Active);
            else
                user = await _ContextRepository.GetRepository<User>().SingleOrDefaultAsync(m => m.Email.ToLower() == email.ToLower() && m.UserId != Id && m.Status == (int)DBStatus.Active);


            if (user == null)
                return true;
            else
                return false;
        }

    }
}
