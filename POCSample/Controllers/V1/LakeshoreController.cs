using Microsoft.AspNetCore.Mvc;
using POCSample.Helper;
using POCSampleModel.CustomModels;
using POCSampleModel.SpDbContext;
using POCSampleService.POCSampleRepository.Implementation;
using POCSampleService.POCSampleRepository.Interface;
using System.ComponentModel.DataAnnotations;

namespace POCSample.Controllers.V1
{
    /// <summary>
    /// TaskController
    /// </summary>
    [Route("api/lakeshore")]
    [ApiController]
    public class LakeshoreController : BaseController
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}

        private ILakeshoreRepository _lakeshoreRepository;
        /// <summary>
        /// Initializing Tasks Controller 
        /// </summary>
        /// <param name="taskRepository">Tasks Repository</param>
        /// <param name="config">Configuration</param>
        public LakeshoreController(ILakeshoreRepository lakeshoreRepository)
        {
            _lakeshoreRepository = lakeshoreRepository;
        }

        #region Delete Lakeshore Product by SKU
        /// <summary>
        /// ProductList
        /// </summary>           
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpPost("{sku}/delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteLakeshore([FromRoute][Required] string sku)
        {
            await _lakeshoreRepository.DeleteLakeshore(sku);
            return Ok();

        }
        #endregion

        #region Lakeshoreproductwithfilter
        /// <summary>
        /// Get DropDown List of accesslevel by Department
        /// </summary>                
        /// <param name="model"></param>  
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpGet("lakeshoreproductwithfilter")]
        [ProducesResponseType(typeof(ExecutreStoreProcedureResultNew), StatusCodes.Status200OK)]
        //[AuthorizeAttributes(modulePermissionSId: Constants.Modules.AuthenticationOnly)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListLKSProducts([FromQuery] NewSearchRequestModel model)
        {
            var filters = FillParamesFromModel(model);
            var res = await _lakeshoreRepository.ListLKSProductsWithParam(filters);
            return GetResult(res);
        }
        #endregion Lakeshoreproductwithfilter

        #region ProductList
        /// <summary>
        /// ProductList
        /// </summary>           
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpGet("lkssku")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProductSKUList()
        {
            var res = await _lakeshoreRepository.GetProductSKU();
            return Ok(res);

        }
        #endregion
    }
}
