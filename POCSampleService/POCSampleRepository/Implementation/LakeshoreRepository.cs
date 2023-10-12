using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using POCSampleModel.Common;
using POCSampleModel.CustomModels;
using POCSampleModel.Models;
using POCSampleModel.RequestModel;
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
    public class LakeshoreRepository : ILakeshoreRepository
    {
        #region Initialization
        //LakeshoremasterContext _dbcontext;
        public IUnitOfWork _unitOfWork;
        private readonly SpContext _dataContext;
        public LakeshoreRepository(SpContext spContext, IUnitOfWork unitOfWork)
        {
            _dataContext = spContext;
            _unitOfWork = unitOfWork;
        }
        #endregion


        public async Task DeleteLakeshore(string sku)
        {
            var lakeshoreproduct = await _unitOfWork.GetRepository<Lakeshoreproduct>().FirstOrDefaultAsync(x => x.Lkssku == sku);

            lakeshoreproduct.Status = 2;

            var compititorproductmaps = await _unitOfWork.GetRepository<Compititorproductmap>().GetAllAsync();

            var selectedcompititorproductmaps = compititorproductmaps.Where(a => a.LakeshoreprouctId == lakeshoreproduct.Lakeshoreproductid).ToList();

            foreach ( var item in selectedcompititorproductmaps)
            {
                item.Status = 2;
            }

            _unitOfWork.GetRepository<Lakeshoreproduct>().Update(lakeshoreproduct);

            _unitOfWork.GetRepository<Compititorproductmap>().Update(selectedcompititorproductmaps);

            await _unitOfWork.CommitAsync();
        }

        public async Task<List<string>> GetProductSKU()
        {
            List<string> lksskudata = new List<string>();
            var result = await _unitOfWork.GetRepository<Lakeshoreproduct>().GetAllAsync();
            foreach (var item in result)
            {
                lksskudata.Add(item.Lkssku);
            }
            return lksskudata;
        }

        public async Task<Page> ListLKSProductsWithParam(NewSearchRequestModel parameters)
        {
            string sqlQuery = "call test2_procedure ({0},{1},{2},{3},{4},{5})";
            object[] filters = { parameters.SortColumn , parameters.SortOrder ,parameters.SearchText , parameters.Filters , parameters.Page , parameters.PageSize };
            var result = await _dataContext.ExecuteStoreProcedureForSearchList(sqlQuery, filters);
            return result;
        }

    }
}
