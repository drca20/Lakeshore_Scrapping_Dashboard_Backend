using POCSampleService.POCSampleRepository.Implementation;
using POCSampleService.POCSampleRepository.Interface;

namespace POCSample.DependencyInjection
{
    public static class DomainCollectionExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDomains(this IServiceCollection services)
        {
            services.AddScoped<ICompititorRepository, CompititorRepository>();
            services.AddScoped<ILakeshoreRepository, LakeshoreRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}
