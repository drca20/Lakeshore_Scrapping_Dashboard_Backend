using POCSampleModel.CustomModels;
using POCSampleModel.Models;
using POCSampleModel.RequestModel;
using POCSampleModel.ResponseModel;
using POCSampleModel.SpDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POCSampleModel.Common.Constants;

namespace POCSampleService.POCSampleRepository.Interface
{
    public interface ICompititorRepository
    {
        Task<List<CompititorResponseModel>> GetProduct();

        Task<CompititorResponseModel> GetProduct(string productSKU);

        Task UpdateMatchType(UpdateCompititorTypeRequestModel updateCompititorTypeRequestModel);

        Task updateProductType(CompititorTypeUpdateRequestModel compititorTypeUpdateRequestModel, string lakeshoreSku, string compSku);
        Task<string> ListProductsWithSKU(NewSearchRequestModel parameters,int compititorid);
    }

}
