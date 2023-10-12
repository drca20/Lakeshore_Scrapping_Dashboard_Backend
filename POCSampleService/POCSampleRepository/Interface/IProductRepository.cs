using POCSampleModel.CustomModels;
using POCSampleModel.Models;
using POCSampleModel.RequestModel;
using POCSampleModel.ResponseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleService.POCSampleRepository.Interface
{
    public interface IProductRepository
    {
        Task<Page> ListProduct(string CompititorCode, NewSearchRequestModel spListRequest);
        Task<Product> GetProduct(int Compititorcode, string sku);
        Task<ProductResponse> CreateProduct(ProductRequest productRequest);
        Task<string> UpdateProductPrice(ProductPriceUpdateRequest productRequest, int ProductID);
        Task DeleteProduct(int ProductID);
        Task UpdateProductMapType(int Type, int ProductMapID);
        Task CreateBulkMapping(List<ProductMapCreate> productMapCreate, int BaseCompititorID, int TargetCompititorID);

    }
}
