using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using POCSampleModel.Common;
using POCSampleModel.CustomModels;
using POCSampleModel.Models;
using POCSampleModel.RequestModel;
using POCSampleModel.ResponseModel;
using POCSampleModel.SpDbContext;
using POCSampleService.POCSampleRepository.Interface;
using POCSampleService.SpRepository;
using POCSampleService.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static POCSampleModel.Common.Constants;

namespace POCSampleService.POCSampleRepository.Implementation
{
    public class CompititorRepository : ICompititorRepository
    {

        #region Initialization
        //LakeshoremasterContext _dbcontext;
        private readonly SpContext _dataContext;

        public IUnitOfWork _unitOfWork;


        public CompititorRepository(/*LakeshoremasterContext dbcontext,*/ SpContext spContext, IUnitOfWork unitOfWork)
        {
            //_dbcontext = dbcontext;
            _dataContext = spContext;
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region List

        public async Task<List<CompititorResponseModel>> GetProduct()
        {
            var lakeshoreproducts = await _unitOfWork.GetRepository<Lakeshoreproduct>().GetAllAsync();
            var response = new List<CompititorResponseModel>();

            var productid = lakeshoreproducts.Select(x => x.Lakeshoreproductid).ToList();

            var compititorproductmapslist = await _unitOfWork.GetRepository<Compititorproductmap>().GetAllAsync(x => productid.Contains((int)x.LakeshoreprouctId));

            var compititorproductId = compititorproductmapslist.Select(x => x.CompititorproductId).Distinct().ToList();

            var compititorproductlist = await _unitOfWork.GetRepository<Compititorproduct>().GetAllAsync(x => compititorproductId.Contains(x.CompititorProductId));

            foreach (var product in lakeshoreproducts)
            {
                var setLP = new CompititorResponseModel();

                setLP.ProductName = product.Lksproductname;
                setLP.ProductUrl = product.Lksurl;
                setLP.Price = product.Lksprice;
                setLP.ProductsImages = JsonConvert.DeserializeObject<List<string>>(product.ImageUrl);

                var compititorproducts = new List<CompititorProductResponseModel>();

                var compititorproductmaps = compititorproductmapslist.Where(x => x.LakeshoreprouctId == product.Lakeshoreproductid);

                foreach (var inneritm in compititorproductmaps)
                {
                    var compititorproduct = new CompititorProductResponseModel();
                    var setCP = compititorproductlist.FirstOrDefault(x => x.CompititorProductId == inneritm.CompititorproductId);

                    compititorproduct.CompititorProductID = setCP.CompititorProductId;
                    compititorproduct.CompititorProductName = setCP.Compititorproductname;
                    compititorproduct.CompititorPrice = setCP.CompititorPrice;
                    compititorproduct.CompititorProductUrl = setCP.CompititorUrl;
                    compititorproduct.MatchType = (int)inneritm.Matchtype;
                    compititorproduct.CompititorImages = JsonConvert.DeserializeObject<List<string>>(setCP.ImageUrl); ;

                    compititorproducts.Add(compititorproduct);
                }
                setLP.CompititorProduct = compititorproducts;

                response.Add(setLP);
            }
            return response;
            
        }
        #endregion

        public async Task<CompititorResponseModel> GetProduct(string productSKU)
        {
            var lakeshoreproduct = await _unitOfWork.GetRepository<Lakeshoreproduct>().FirstOrDefaultAsync(x => x.Lkssku == productSKU );
            var response = new CompititorResponseModel();

            var compititorproductmaps = await _unitOfWork.GetRepository<Compititorproductmap>().FirstOrDefaultAsync(x => x.LakeshoreprouctId == lakeshoreproduct.Lakeshoreproductid);

            var compititorproducts = await _unitOfWork.GetRepository<Compititorproduct>().GetAllAsync(x => x.CompititorProductId == compititorproductmaps.CompititorproductId);

            response.ProductName = lakeshoreproduct.Lksproductname;
            response.ProductUrl = lakeshoreproduct.Lksurl;
            response.Price = lakeshoreproduct.Lksprice;
            response.ProductsImages = JsonConvert.DeserializeObject<List<string>>(lakeshoreproduct.ImageUrl);

            var compititorproductres = new List<CompititorProductResponseModel>();

            foreach (var inneritm in compititorproducts)
            {
                var compititorproduct = new CompititorProductResponseModel();

                compititorproduct.CompititorProductID = inneritm.CompititorProductId;
                compititorproduct.CompititorProductName = inneritm.Compititorproductname;
                compititorproduct.CompititorPrice = inneritm.CompititorPrice;
                compititorproduct.CompititorProductUrl = inneritm.CompititorUrl;
                compititorproduct.MatchType = (int)compititorproductmaps.Matchtype;
                compititorproduct.CompititorImages = JsonConvert.DeserializeObject<List<string>>(inneritm.ImageUrl); ;
                compititorproductres.Add(compititorproduct);
            }
            response.CompititorProduct = compititorproductres;

            return response;
        }



        #region Update
        public async Task UpdateMatchType(UpdateCompititorTypeRequestModel updateCompititorTypeRequestModel)
        {
            var compititorProduct = await _unitOfWork.GetRepository<Compititorproductmap>().FirstOrDefaultAsync(x => x.CompititorproductId == updateCompititorTypeRequestModel.CompititorID);

            compititorProduct.Matchtype = updateCompititorTypeRequestModel.MatchType;

            _unitOfWork.GetRepository<Compititorproductmap>().Update(compititorProduct);
            await _unitOfWork.CommitAsync();
        }
        #endregion


        #region Update ProductType
        public async Task updateProductType(CompititorTypeUpdateRequestModel compititorTypeUpdateRequestModel, string lakeshoreSku, string compSku)
        {
            var lakeshoreProduct = await _unitOfWork.GetRepository<Lakeshoreproduct>().SingleOrDefaultAsync(x => x.Lkssku == lakeshoreSku && x.Status != 3);

            var compititorProduct = await _unitOfWork.GetRepository<Compititorproduct>().SingleOrDefaultAsync(x => x.CompititorSku == compSku);

            var productMap = await _unitOfWork.GetRepository<Productmap>().SingleOrDefaultAsync(x => x.LakeshoreProductId == lakeshoreProduct.Lakeshoreproductid && x.CompititorsProductId == compititorProduct.CompititorProductId);

            productMap.Type = compititorTypeUpdateRequestModel.MatchType;
            productMap.ModifiedDateTime = DateTime.Now;

            _unitOfWork.GetRepository<Productmap>().Update(productMap);
            await _unitOfWork.CommitAsync();
        }
        #endregion

        public async Task<string> ListProductsWithSKU(NewSearchRequestModel parameters , int compititorid)
        {
            string sqlQuery = "call ddl_get_Products_By_Compititor ({0},{1},{2},{3})";
            object[] param = { parameters.Page , parameters.PageSize , parameters.SearchText , compititorid };
            var result = await _dataContext.ExecuteStoreProcedure(sqlQuery, param);
            return result;
        }


    }
}
