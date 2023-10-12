using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using POCSampleModel.Common;
using POCSampleService.Repository;
using POCSampleService.RepositoryFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleService.UnitOfWork
{
    public class UnitOfWork<TContext> : IRepositoryFactory, IUnitOfWork<TContext>
        where TContext : DbContext, IDisposable
    {
        private Dictionary<(Type type, string name), object> _repositories;

        public UnitOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return (IRepository<TEntity>)GetOrAddRepository(typeof(TEntity), new Repository<TEntity>(Context));
        }


        public TContext Context { get; }

        public int Commit(bool autoHistory = false)
        {
            if (autoHistory) Context.EnsureAutoHistory();
            return Context.SaveChanges();
        }

        public async Task<int> CommitAsync(bool autoHistory = false)
        {
            try
            {
                if (autoHistory) Context.EnsureAutoHistory();

                return await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw;

            }
        }
        public IDbContextTransaction dbContextTransaction { get; set; }

        public async Task<int> CommitAsyncWithTransaction()
        {
            try
            {
                // var transaction = Context.Database.BeginTransaction();
                //using (var transaction = Context.Database.BeginTransaction())
                // {
                try
                {

                    if (dbContextTransaction == null)
                        dbContextTransaction = Context.Database.BeginTransaction();
                    // else
                    //  transaction = dbContextTransaction;

                    int result = await Context.SaveChangesAsync();
                    //  transaction.Commit();
                    return result;
                }
                catch (Exception ex)
                {
                    // transaction.Rollback();
                    throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in qruery " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException != null ? ex.InnerException.ToString() : "");
                }
                //  }

            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in qruery " + ex.Message + " " + ex.StackTrace + " " + ex.InnerException != null ? ex.InnerException.ToString() : "");
                //  throw new HttpStatusCodeException(StatusCodes.Status400BadRequest, "Error in qruery " + ex.Message);
            }

        }

        public void Dispose()
        {
            // Context?.Dispose();
        }

        internal object GetOrAddRepository(Type type, object repo)
        {
            _repositories ??= new Dictionary<(Type type, string Name), object>();

            if (_repositories.TryGetValue((type, repo.GetType().FullName), out var repository)) return repository;
            _repositories.Add((type, repo.GetType().FullName), repo);
            return repo;
        }
    }
}
