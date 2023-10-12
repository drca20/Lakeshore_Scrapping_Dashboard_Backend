using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using POCSample.Helper;
using POCSampleModel.CustomModels;
using POCSampleModel.RequestModel;
using POCSampleModel.ResponseModel;
using POCSampleModel.SpDbContext;
using POCSampleService.POCSampleRepository.Implementation;
using POCSampleService.POCSampleRepository.Interface;
using System.ComponentModel.DataAnnotations;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace POCSample.Controllers.V1
{
    /// <summary>
    /// TaskController
    /// </summary>
    [Route("api/compititor")]
    [ApiController]
    public class CompititorController : BaseController
    {
        #region Declaration
        private ICompititorRepository _compititorRepository;
        /// <summary>
        /// Initializing Tasks Controller 
        /// </summary>
        /// <param name="taskRepository">Tasks Repository</param>
        /// <param name="config">Configuration</param>
        public CompititorController(ICompititorRepository compititorRepository)
        {
            _compititorRepository = compititorRepository;
        }
        #endregion

        #region ProductList
        /// <summary>
        /// ProductList
        /// </summary>           
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ProductList()
        {
            var res = await _compititorRepository.GetProduct();
            return Ok(res);

        }
        #endregion

        #region Update Compititor Match Type
        /// <summary>
        /// Update Compititor Match Type
        /// </summary>           
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpPost("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutProduct([FromBody][Required][CustomizeValidator(Interceptor = typeof(FluentInterceptor))] UpdateCompititorTypeRequestModel updateCompititorTypeRequestModel)
        {
            await _compititorRepository.UpdateMatchType(updateCompititorTypeRequestModel);
            return Ok();

        }
        #endregion

        #region Get Product
        /// <summary>
        /// Get Product
        /// </summary>           
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpGet("{sku}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetProduct([FromRoute][Required] string sku)
        {

            var res = await _compititorRepository.GetProduct(sku);
            return Ok(res);

        }
        #endregion

        #region Update Product Type
        /// <summary>
        /// Update Product Type
        /// </summary>           
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpPost("product_map/base/{lakeshore_sku}/target/{comp_sku}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PutProductType([FromBody][Required][CustomizeValidator(Interceptor = typeof(FluentInterceptor))] CompititorTypeUpdateRequestModel compititorTypeUpdateRequestModel, string lakeshore_sku, string comp_sku)
        {
            await _compititorRepository.updateProductType(compititorTypeUpdateRequestModel, lakeshore_sku, comp_sku);
            return Ok();

        }
        #endregion

        #region ProductsByCompititor_DropdownList
        /// <summary>
        /// Get DropDown List of ProductsByCompititor
        /// </summary>                
        /// <param name="model"></param>  
        /// <response code="200">OK: The request was successful and the response body contains the representation requested.</response>
        /// <response code="400">BAD REQUEST: The data given in the POST or PUT failed validation. Inspect the response body for details.</response>
        /// <response code="401">UNAUTHORIZED: The supplied credentials, if any, are not sufficient to access the resource.</response>
        /// <response code="404">NOT FOUND</response>
        /// <response code="500">SERVER ERROR: We couldn't return the representation due to an internal server error.</response>
        [HttpGet("ddl/{compititorid}")]
        [ProducesResponseType(typeof(List<ddlProductByCompititorID>), StatusCodes.Status200OK)]
        //[AuthorizeAttributes(modulePermissionSId: Constants.Modules.AuthenticationOnly)]
        [ProducesResponseType(typeof(UnauthorizedResponse), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(NotFoundResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(InternalServerErrorReponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListCompitiorProductsWithSKU([FromQuery] NewSearchRequestModel model, [FromRoute][Required] int compititorid)
        {
            List<ddlProductByCompititorID> listResponses = new List<ddlProductByCompititorID>();
            var filters = FillParamesFromModel(model);
            var res = await _compititorRepository.ListProductsWithSKU(filters, compititorid);
            if (!string.IsNullOrWhiteSpace(res))
            {
                listResponses = JsonConvert.DeserializeObject<List<ddlProductByCompititorID>>(res);
            }
            return GetResult(listResponses);
        }
        #endregion ProductsByCompititor_DropdownList

    }
}
