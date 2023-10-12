using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModels;
using Newtonsoft.Json;
using POCSampleService.POCSampleRepository.Interface;
using System.ComponentModel.DataAnnotations;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace POCSample.Controllers.V1
{
    [Route("auth")]
    public class AuthController : BaseController
    {
        private IUserRepository _userRepository { get; set; }
        private static IHostingEnvironment _hostingEnvironment { get; set; }
        public AuthController(IUserRepository userRepository, IHostingEnvironment hostingEnvironment)
        {
            _userRepository = userRepository;
            _hostingEnvironment = hostingEnvironment;

        }

        /// <summary>
        /// Get All
        /// </summary> 
        [HttpGet("get_all_users")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _userRepository.Get();
            return Ok(response);
        }

        /// <summary>
        /// delete
        /// </summary> 
        //[Authorize]
        [HttpDelete("delete_user/{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _userRepository.Delete(id);
            return Ok();

        }

        #region Login
        /// <summary>
        /// Authenticate
        /// </summary> 
        [HttpPost("login")]
        public async Task<IActionResult> Authenticate([FromBody][Required][CustomizeValidator(Interceptor = typeof(FluentInterceptor))] AuthenticateLoginRequest model)
        {
            var response = await _userRepository.AuthenticateLogin(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        #endregion

        #region Login Refresh Token
        /// <summary>
        /// Authenticate Refreshtoken
        /// </summary> 
        [HttpPost("refresh_token")]
        public async Task<IActionResult> AuthenticateRefreshtoken([FromBody][Required][CustomizeValidator(Interceptor = typeof(FluentInterceptor))] AuthenticateRefereshTokenRequest model)
        {

            var response = await _userRepository.AuthenticateRefreshToken(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }
        #endregion


        //#region LoggedInUser
        ///// <summary>
        /////LoggedInUser
        ///// </summary> 
        //[HttpGet("logeedin_user")]
        ////[Authorize]
        //public async Task<IActionResult> GetLoggedinUser()
        //{

        //    var response = await _userRepository.GetLoggedinUser(GetUserDetails().AccountId);
        //    return Ok(response);
        //}
        //#endregion

        #region Register
        /// <summary>
        /// Register User
        /// </summary> 
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromForm][Required][CustomizeValidator(Interceptor =typeof(FluentInterceptor))] UserRequest model)
        {

            #region upload profile image

            var datetimesting = DateTime.Now.ToString("yyyyMMddHHmmssffff");

            var fileName = "";
            var fileExpentestion = "";
            var fullFileName = "";
            if (HttpContext.Request.Form.Files.Count != 0)
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    var fileWholeName = file.FileName.Split(".");
                    fileExpentestion = fileWholeName[fileWholeName.Length - 1];
                    fileName = fileWholeName[fileWholeName.Length - 2] + datetimesting;
                }
            }
            fullFileName = fileName + "." + fileExpentestion;

            var filepath = await UploadAsync(_hostingEnvironment, fullFileName);

            #endregion
            //var request = JsonConvert.DeserializeObject<UserRequest>(model);
            if (HttpContext.Request.Form.Files.Count != 0)
            {
                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    filepath = "/uploads/" + fullFileName;
                }
            }
            var users = await _userRepository.Create(model, filepath);
            return Ok(users);
        }

        #endregion


        ///// <summary>
        ///// Update
        ///// </summary> 
        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<IActionResult> Update([FromForm] string model, [FromRoute] int id)
        //{
        //    #region upload profile image

        //    var datetimesting = DateTime.Now.ToString("yyyyMMddHHmmssffff");

        //    var fileName = "";
        //    var fileExpentestion = "";
        //    var fullFileName = "";
        //    if (HttpContext.Request.Form.Files.Count != 0)
        //    {
        //        var file = Request.Form.Files[0];
        //        if (file.Length > 0)
        //        {
        //            var fileWholeName = file.FileName.Split(".");
        //            fileExpentestion = fileWholeName[fileWholeName.Length - 1];
        //            fileName = fileWholeName[fileWholeName.Length - 2] + datetimesting;
        //        }
        //    }
        //    fullFileName = fileName + "." + fileExpentestion;

        //    var filepath = await UploadAsync(_hostingEnvironment, fullFileName);
        //    #endregion

        //    var request = JsonConvert.DeserializeObject<UserRequest>(model);
        //    if (HttpContext.Request.Form.Files.Count != 0)
        //    {
        //        var file = Request.Form.Files[0];
        //        if (file.Length > 0)
        //        {
        //            filepath = "/uploads/" + fullFileName;
        //        }
        //    }
        //    var response = await _userRepository.Put(filepath, request, id);
        //    return Ok(response);

        //}

        //#region Update User Section Certificate
        ///// <summary>
        ///// Update
        ///// </summary> 
        //[HttpPut("user/update/{id}")]
        //[Authorize]
        //public async Task<IActionResult> UpdateSectionCertificatge([FromBody] UserSectionRequest request, [FromRoute] int id)
        //{
        //    //var request = JsonConvert.DeserializeObject<UserSectionRequest>(model);
        //    await _userRepository.UpdateSectionandCertificate(request, id);
        //    return Ok();
        //}

        //#endregion

        ///// <summary>
        ///// Reset password
        ///// </summary> 
        //[HttpPost("reset-password")]
        //public async Task<IActionResult> ResetPassword([FromBody] AuthenticateRequest model)
        //{
        //    var users = await _userRepository.ForgotPasswordReset(model);
        //    return Ok(users);
        //}

        ///// <summary>
        ///// GET BY ID
        ///// </summary> 
        //[HttpGet("{userid}")]
        //public async Task<IActionResult> Get([FromRoute] int userid)
        //{
        //    var users = await _userRepository.GetByID(userid);
        //    return Ok(users);
        //}

        ///// <summary>
        ///// 
        ///// </summary> 
        //[HttpPost("contact")]
        //public async Task<IActionResult> ContactWithUs([FromBody] ContactUsRequest request)
        //{
        //    await _userRepository.ConnectUs(request);
        //    return Ok();
        //}


        ///// <summary>
        ///// 
        ///// </summary> 
        //[HttpGet("contact")]
        //public async Task<IActionResult> GetContactWithUs()
        //{
        //    var result = await _userRepository.GetConnectUs();
        //    return Ok(result);
        //}

        //#region User List
        ///// <summary>
        ///// Get ALl users
        ///// </summary> 
        ///// 
        //[HttpGet("users")]
        //public async Task<IActionResult> GetUsers()
        //{
        //    var result = await _userRepository.GetUsers();
        //    return Ok(result);
        //}
        //#endregion

        //#region Update User Section Certificate
        ///// <summary>
        ///// Update
        ///// </summary> 
        //[HttpGet("user_step/{id}")]
        //[Authorize]
        //public async Task<IActionResult> UpdateStepQuiz([FromRoute] int id)
        //{

        //    await _userRepository.UpdateUserdata(id);
        //    return Ok();
        //}

        //#endregion
    }
}
