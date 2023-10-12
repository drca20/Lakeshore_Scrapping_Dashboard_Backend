
using Microsoft.EntityFrameworkCore;
using POCSample.Helper;
using POCSampleService.POCSampleRepository.Implementation;
using POCSampleService.POCSampleRepository.Interface;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using System.Reflection;
using MicroElements.Swashbuckle.FluentValidation;
using POCSampleModel.Models;
using System;
using POCSampleModel.ResponseModel;
using POCSampleService.RepositoryFactory;
using POCSampleService.UnitOfWork;
using POCSampleModel.SpDbContext;
using POCSample.DependencyInjection;

namespace POCSample
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                   .AddNewtonsoftJson(options =>
                       options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                       );
            var _configuration = builder.Configuration;
            
            #region Set DB Connection
            string? connectionstring = _configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<LakeshoremasterContext>(options =>
            {
                options.UseMySQL(connectionstring);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging(true);
            }, ServiceLifetime.Transient);

            builder.Services.AddDbContext<SpContext>(options =>
            {
                options.UseMySQL(connectionstring);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging(true);
            }, ServiceLifetime.Transient);


            #endregion

            #region DependencyInjection
            UnitOfWorkServiceCollectionExtentions.AddUnitOfWork<LakeshoremasterContext>(builder.Services);
            DomainCollectionExtension.AddDomains(builder.Services);
            //builder.Services.AddScoped<ICompititorRepository, CompititorRepository>();
            //builder.Services.AddScoped<ILakeshoreRepository, LakeshoreRepository>();

            #endregion

            #region Add FluentValidation
            builder.Services
        .AddMvc()
        // Adds fluent validators to Asp.net
        .AddFluentValidation(c =>
        {
            c.RegisterValidatorsFromAssemblyContaining<CompititorResponseModel>();
            // Optionally set validator factory if you have problems with scope resolve inside validators.
            c.ValidatorFactoryType = typeof(HttpContextServiceProviderValidatorFactory);
        });

            builder.Services.AddTransient<IValidatorInterceptor, FluentInterceptor>();
            builder.Services.AddFluentValidationRulesToSwagger();

            #endregion

            #region Add Swagger
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "POCSample.xml");
                c.IncludeXmlComments(xmlPath);

                xmlPath = Path.Combine(basePath, "POCSampleModel.xml");
                c.IncludeXmlComments(xmlPath);

            });
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins("http://localhost:4200", "https://lakeshorescrapping.azurewebsites.net")
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        ); ;
            });
            builder.Services.AddSwaggerGenNewtonsoftSupport();
            #endregion

            builder.Services.AddLocalization(opt => { opt.ResourcesPath = "Resources"; });
            builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization();

            var app = builder.Build();

            app.UseHttpStatusCodeExceptionMiddleware();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            app.UseSwagger();
            app.UseCors("CorsApi");
            app.UseSwaggerUI();
            //}

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}