using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Models.RequestModels;
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

namespace POCSampleService.POCSampleRepository.Implementation
{
    public class ProductRepository : IProductRepository
    {
        public IUnitOfWork _ContextRepository;
        public SpContext _datacontext;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProductRepository(IUnitOfWork contextRepository, IHostingEnvironment hostingEnvironment, SpContext spContext)
        {
            _ContextRepository = contextRepository;
            _hostingEnvironment = hostingEnvironment;
            _datacontext = spContext;
        }

        #region List Product
        public async Task<Page> ListProduct(string CompititorCode, NewSearchRequestModel parameters)
        {
            int compititortype;
            if (CompititorCode == Constants.Compititor.LKS.ToString())
            {
                compititortype = (int)Constants.Compititor.LKS;
            }
            else if (CompititorCode == Constants.Compititor.KPL.ToString())
            {
                compititortype = (int)Constants.Compititor.KPL;
            }
            else if (CompititorCode == Constants.Compititor.DSS.ToString())
            {
                compititortype = (int)Constants.Compititor.DSS;
            }
            else {
                compititortype = (int)Constants.Compititor.LKS;
            }
            string sqlQuery = "call get_Products_List ({0},{1},{2},{3},{4},{5},{6})";
            object[] param = { parameters.SortColumn, parameters.SortOrder, parameters.SearchText, parameters.Filters, parameters.Page, parameters.PageSize , compititortype };
            var result = await _datacontext.ExecuteStoreProcedureForSearchList(sqlQuery, param);
            //if (string.IsNullOrWhiteSpace((string?)result.Result))
            //{
            //    throw new HttpStatusCodeException(400, "Products list not found");
            //}
            return result;
        }
        #endregion

        #region Get Product
        public async Task<Product> GetProduct(int Compititorcode,string sku)
        {
            var product = await _ContextRepository.GetRepository<Product>().SingleOrDefaultAsync(x => x.CompititorId == Compititorcode && x.Status != (int)Constants.DBStatus.Delete);
            if (product == null)
            {
                throw new HttpStatusCodeException(StatusCodes.Status404NotFound, "Product not found.");
            }
            return product;
        }
        #endregion

        #region Create Product
        public async Task<ProductResponse> CreateProduct(ProductRequest productRequest)
        {
            try
            {
                var app = await _ContextRepository.GetRepository<Compititor>().SingleOrDefaultAsync(x => x.CompititorsId == productRequest.CompititorId);
                if (app == null)
                {
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound, "Compititor not found.");
                }
                var product = CommonHelper.ToDocumentData<ProductRequest, Product>(productRequest);
                //if(productRequest.Compititor == Constants.Compititor.KPL.ToString())
                //{
                //    product.CompititorId = (int)Constants.Compititor.KPL;
                //}
                //else if(productRequest.Compititor == Constants.Compititor.LKS.ToString())
                //{
                //    product.CompititorId = (int)Constants.Compititor.LKS;
                //}
                //else if (productRequest.Compititor == Constants.Compititor.DSS.ToString())
                //{
                //    product.CompititorId = (int)Constants.Compititor.DSS;
                //}
                product.CreatedDateTime = DateTime.UtcNow;
                product.ModifiedDateTime = DateTime.UtcNow;
                product.CreatedByUserId = 1;
                product.ModifiedByUserId = 1;
                product.Status = (int)Constants.DBStatus.Active;

                await _ContextRepository.GetRepository<Product>().InsertAsync(product);
                await _ContextRepository.CommitAsyncWithTransaction();

                Productpricehistory productpricehistory = ProPriceHisObject(product.ProductId, (decimal)product.CurrentPrice);

                await _ContextRepository.GetRepository<Productpricehistory>().InsertAsync(productpricehistory);
                await _ContextRepository.CommitAsyncWithTransaction();
            }
            catch (Exception ex)
            {
                _ContextRepository.dbContextTransaction.Rollback();
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in save records in db " + ex.Message);
            }
            //product.CompititorId == coduct.CompititorId = productRequest.Compititor == Constants.Compititor.KPL.ToString() ? ;
            ProductResponse productResponse = new ProductResponse();
            productResponse.ProductName = productRequest.Name;
            if (productRequest.CompititorId == (int)Constants.Compititor.KPL)
            {
                productResponse.Compititor = Constants.Compititor.KPL.ToString();
            }
            else if (productRequest.CompititorId == (int)Constants.Compititor.LKS)
            {
                productResponse.Compititor = Constants.Compititor.LKS.ToString();
            }
            else if (productRequest.CompititorId == (int)Constants.Compititor.DSS)
            {
                productResponse.Compititor = Constants.Compititor.DSS.ToString();
            }
            productResponse.SKU = productRequest.SKU;
            productResponse.Description = productRequest.Description;
            productResponse.Category = productRequest.Category;
            productResponse.CurrentPrice = productRequest.CurrentPrice;
            return productResponse;

        }
        #endregion

        #region Product Price History Object
        private static Productpricehistory ProPriceHisObject(int ProductID, decimal price)
        {
            var productpricehistory = new Productpricehistory();
            productpricehistory.ProductPriceHistoryId = 1;
            productpricehistory.ProductId = ProductID;
            productpricehistory.Price = price;
            productpricehistory.Date = DateTime.UtcNow;
            productpricehistory.ModifiedDateTime = DateTime.UtcNow;
            productpricehistory.CreatedDateTime = DateTime.UtcNow;
            productpricehistory.CreatedByUserId = 1;
            productpricehistory.ModifiedByUserId = 1;
            productpricehistory.Status = (int)Constants.DBStatus.Active;
            return productpricehistory;
        }
        #endregion

        #region Update Product Price
        public async Task<string> UpdateProductPrice(ProductPriceUpdateRequest productRequest, int ProductID)
        {
            var product = await _ContextRepository.GetRepository<Product>().SingleOrDefaultAsync(x => x.ProductId == ProductID && x.Status != (int)Constants.DBStatus.Delete);
            if (product == null)
            {
                throw new HttpStatusCodeException(StatusCodes.Status404NotFound, "Product not found.");
            }
            product.CurrentPrice = productRequest.Price;
            product.ModifiedDateTime = DateTime.UtcNow;
            _ContextRepository.GetRepository<Product>().Update(product);
            await _ContextRepository.CommitAsync();

            Productpricehistory productpricehistory = ProPriceHisObject(ProductID, productRequest.Price);
            await _ContextRepository.GetRepository<Productpricehistory>().InsertAsync(productpricehistory);
            await _ContextRepository.CommitAsync();

            string res = "Price for Product '" + product.Name + "' changed from " + product.CurrentPrice + " to " + productpricehistory.Price;
            return res;
        }
        #endregion

        #region Delete Product
        public async Task DeleteProduct(int ProductID)
        {
            try
            {
                var product = await _ContextRepository.GetRepository<Product>().SingleOrDefaultAsync(x => x.ProductId == ProductID && x.Status != (int)Constants.DBStatus.Delete);
                if (product == null)
                {
                    throw new HttpStatusCodeException(StatusCodes.Status404NotFound, "Product not found.");
                }
                product.Status = (int)Constants.DBStatus.Delete;
                product.ModifiedDateTime = DateTime.UtcNow;
                product.ModifiedByUserId = 1;

                _ContextRepository.GetRepository<Product>().Update(product);
                await _ContextRepository.CommitAsync();

                List<Productpricehistory> Productpricehistory = new List<Productpricehistory>();
                var propricehis = await _ContextRepository.GetRepository<Productpricehistory>().GetAllAsync(x => x.ProductId == product.ProductId);
                if (propricehis != null && propricehis.Count > 0)
                {
                    foreach (var item in propricehis)
                    {
                        item.Status = (int)Constants.DBStatus.Delete;
                        item.ModifiedDateTime = DateTime.UtcNow;
                        item.ModifiedByUserId = 1;
                        Productpricehistory.Add(item);
                    }
                }

                if (Productpricehistory != null && Productpricehistory.Count > 0)
                {
                    _ContextRepository.GetRepository<Productpricehistory>().Update(Productpricehistory);
                    await _ContextRepository.CommitAsync();
                }

                //todo : delete from map tables as well 

                var lakeshoremap = await _ContextRepository.GetRepository<Productmap>().GetAllAsync(x => x.LakeshoreProductId == product.ProductId && x.Status != (int)Constants.DBStatus.Delete);
                var compititormap = await _ContextRepository.GetRepository<Productmap>().GetAllAsync(x => x.CompititorsProductId == product.ProductId && x.Status != (int)Constants.DBStatus.Delete);
                List<Productmap> productmaps = new List<Productmap>();
                productmaps.AddRange(lakeshoremap);
                productmaps.AddRange(compititormap);

                foreach (var item in productmaps)
                {
                    item.Status = (int)Constants.DBStatus.Delete;
                    item.ModifiedDateTime = DateTime.UtcNow;
                }

                if (productmaps != null && productmaps.Count > 0)
                {
                    _ContextRepository.GetRepository<Productmap>().Update(productmaps);
                    await _ContextRepository.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                _ContextRepository.dbContextTransaction.Rollback();
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in save records in db " + ex.Message);
            }
        }
        #endregion

        #region Update Product Map Type
        public async Task UpdateProductMapType(int Type, int ProductMapID)
        {
            if (Type != (int)Constants.ProductComparisonType.perfectmatch && Type != (int)Constants.ProductComparisonType.notmatch && Type != (int)Constants.ProductComparisonType.partialmatch)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Please select the correct type.");
            }
            var productmap = await _ContextRepository.GetRepository<Productmap>().SingleOrDefaultAsync(x => x.ProductMapId == ProductMapID);
            if (productmap == null)
            {
                throw new HttpStatusCodeException(StatusCodes.Status404NotFound, "Product not found.");
            }
            productmap.Type = Type;
            productmap.ModifiedDateTime = DateTime.UtcNow;

            _ContextRepository.GetRepository<Productmap>().Update(productmap);
            await _ContextRepository.CommitAsync();
        }
        #endregion


        #region Create Bulk Mapping
        public async Task CreateBulkMapping(List<ProductMapCreate> productMapCreate, int BaseCompititorID, int TargetCompititorID)
        {
            try
            {
                var BaseSKUs = productMapCreate.Select(x => x.BaseSKU).ToList();
                var TargetSKUs = productMapCreate.Select(x => x.TargetSKU).ToList();

                var BaseProducts = await _ContextRepository.GetRepository<Product>().GetAllAsync(x => BaseSKUs.Contains(x.Sku) && x.CompititorId == BaseCompititorID && x.Status != (int)Constants.DBStatus.Delete);
                var TargetProducts = await _ContextRepository.GetRepository<Product>().GetAllAsync(x => TargetSKUs.Contains(x.Sku) && x.CompititorId == TargetCompititorID && x.Status != (int)Constants.DBStatus.Delete);
                List<Productmap> productmaps = new List<Productmap>();
                foreach (var item in productMapCreate)
                {
                    var basesku = BaseProducts.FirstOrDefault(x => x.Sku == item.BaseSKU);
                    var targetsku = TargetProducts.FirstOrDefault(x => x.Sku == item.TargetSKU);
                    Productmap productmap = new Productmap();
                    if (basesku != null && targetsku != null)
                    {
                        productmap.LakeshoreProductId = basesku.ProductId;
                        productmap.CompititorsProductId = targetsku.ProductId;
                        productmap.CreatedDateTime = DateTime.UtcNow;
                        productmap.ModifiedDateTime = DateTime.UtcNow;
                        productmap.Status = (int)Constants.DBStatus.Active;
                        if (item.MatchType.ToLower() == Constants.ProductComparisonType.notmatch.ToString())
                        {
                            productmap.Type = (int)Constants.ProductComparisonType.notmatch;
                        }
                        else if (item.MatchType.ToLower() == Constants.ProductComparisonType.partialmatch.ToString())
                        {
                            productmap.Type = (int)Constants.ProductComparisonType.partialmatch;
                        }
                        else if (item.MatchType.ToLower() == Constants.ProductComparisonType.perfectmatch.ToString())
                        {
                            productmap.Type = (int)Constants.ProductComparisonType.perfectmatch;
                        }
                        productmaps.Add(productmap);
                    }
                }

                if (productmaps != null && productmaps.Count > 0)
                {
                    await _ContextRepository.GetRepository<Productmap>().InsertAsync(productmaps);
                    await _ContextRepository.CommitAsync();
                }
            }
            catch (Exception ex)
            {
                _ContextRepository.dbContextTransaction.Rollback();
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in save records in db " + ex.Message);
            }


        }
        #endregion

    }
}
