using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Models.RequestModels;
using POCSampleModel.CustomModels;
using POCSampleModel.Models;
using POCSampleModel.RequestModel;
using POCSampleService.POCSampleRepository.Implementation;
using POCSampleService.POCSampleRepository.Interface;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace POCSample.Controllers.V1
{
    [Route("api/product")]
    public class ProductController : BaseController
    {
        private IProductRepository _productRepository { get; set; }
        private static IHostingEnvironment _hostingEnvironment { get; set; }

        public ProductController(IProductRepository productRepository, IHostingEnvironment hostingEnvironment)
        {
            _productRepository = productRepository;
            _hostingEnvironment = hostingEnvironment;

        }

        #region List Product
        /// <summary>
        /// Get All Prodcuts By compititor code
        /// </summary> 
        [HttpGet("list/{compititor_code}")]
        public async Task<IActionResult> ProductList([FromRoute][Required] string compititor_code, [FromQuery] NewSearchRequestModel spListRequest)
        {
            var filters = FillParamesFromModel(spListRequest);
            var response = await _productRepository.ListProduct(compititor_code, filters);
            return GetResult(response);
        }
        #endregion

        #region Get Product
        /// <summary>
        /// Get prodcut by ID
        /// </summary> 
        [HttpGet("get/{compititor_code}/{sku}")]
        public async Task<IActionResult> GetProduct([FromRoute][Required] string compititor_code, [FromRoute][Required] string sku)
        {
            //var response = await _productRepository.GetProduct(compititor_code, sku);
            //return Ok(response);
            return Ok();
        }
        #endregion

        #region Create Product
        /// <summary>
        /// Create Product
        /// </summary> 
        [HttpPost("")]
        public async Task<IActionResult> CreateProduct([FromBody][Required][CustomizeValidator(Interceptor = typeof(FluentInterceptor))] ProductRequest model)
        {
            var response = await _productRepository.CreateProduct(model);

            return Ok(response);
        }
        #endregion

        #region Update Product Price
        /// <summary>
        /// Update Product Price
        /// </summary> 
        [HttpPost("price_history/{product_id}")]
        public async Task<IActionResult> UpdateProductPrice([FromBody][Required][CustomizeValidator(Interceptor = typeof(FluentInterceptor))] ProductPriceUpdateRequest model, [FromRoute][Required] int product_id)
        {
            var response = await _productRepository.UpdateProductPrice(model, product_id);

            return Ok(response);
        }
        #endregion

        #region Delete Product
        /// <summary>
        /// Delete Product
        /// </summary> 
        [HttpDelete("{product_id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute][Required] int product_id)
        {
            await _productRepository.DeleteProduct(product_id);

            return Ok();
        }
        #endregion

        #region Update Product Map's Type
        /// <summary>
        /// Update Product Map's Type
        /// </summary> 
        [HttpPost("product_map_type/{product_map_id}")]
        public async Task<IActionResult> UpdateProductMapType([FromQuery][Required] int type, [FromRoute][Required] int product_map_id)
        {
            await _productRepository.UpdateProductMapType(type, product_map_id);

            return Ok();
        }
        #endregion

        #region Create bulk Product Map
        /// <summary>
        /// Create bulk Product Map
        /// </summary> 
        [HttpPost("product_map_bulk/base/{comp_id_base}/target/{comp_id_target}")]
        public async Task<IActionResult> CreateBulkMapping([FromBody][Required] List<ProductMapCreate> productMapCreate, [FromRoute][Required] int comp_id_base, [FromRoute][Required] int comp_id_target)
        {
            await _productRepository.CreateBulkMapping(productMapCreate, comp_id_base, comp_id_target);

            return Ok();
        }
        #endregion
    }
}
