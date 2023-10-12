using POCSampleService.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POCSampleService.RepositoryFactory
{
    public interface IRepositoryFactory
    {
        IRepository<T> GetRepository<T>() where T : class;

    }
}
