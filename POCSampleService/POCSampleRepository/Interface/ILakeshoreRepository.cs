using POCSampleModel.CustomModels;
using POCSampleModel.RequestModel;
using POCSampleModel.SpDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleService.POCSampleRepository.Interface
{
    public interface ILakeshoreRepository
    {
        Task DeleteLakeshore(string sku);
        //Task<Page> ListLKSProductsWithParam(object[] parameters);
        Task<Page> ListLKSProductsWithParam(NewSearchRequestModel parameters);
        Task<List<string>> GetProductSKU();
        //Task<Page> ListLKSProductsWithParam(List<dynamic> parameters);
    }
}
