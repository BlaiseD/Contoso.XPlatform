using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using Contoso.AutoMapperProfiles;
using Contoso.Common.Configuration.Json;
using Contoso.Bsl.Flow;
using Contoso.Contexts;
using Contoso.Domain.Json;
using Contoso.Repositories;
using Contoso.Stores;
using Contoso.Utils;
using LogicBuilder.RulesDirector;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contoso.Bsl
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers().AddJsonOptions
            (
                options => 
                { 
                    options.JsonSerializerOptions.Converters.Add(new DescriptorConverter());
                    options.JsonSerializerOptions.Converters.Add(new ModelConverter());
                    options.JsonSerializerOptions.Converters.Add(new ObjectConverter());
                }
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Contoso.Bsl", Version = "v1" });
            })
            .AddDbContext<SchoolContext>
            (
                options => options.UseSqlServer
                (
                    Configuration.GetConnectionString("DefaultConnection")
                )
            )
            .AddScoped<ISchoolStore, SchoolStore>()
            .AddScoped<ISchoolRepository, SchoolRepository>()
            .AddSingleton<AutoMapper.IConfigurationProvider>
            (
                new MapperConfiguration(cfg =>
                {
                    cfg.AddExpressionMapping();

                    cfg.AddProfile<ParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<DescriptorToOperatorMappingProfile>();
                    cfg.AddProfile<SchoolProfile>();
                    cfg.AddProfile<ExpansionParameterToDescriptorMappingProfile>();
                    cfg.AddProfile<ExpansionDescriptorToOperatorMappingProfile>();
                })
            )
            .AddScoped<IMapper>(sp => new Mapper(sp.GetRequiredService<AutoMapper.IConfigurationProvider>(), sp.GetService))
            .AddScoped<IFlowManager, FlowManager>()
            .AddScoped<FlowActivityFactory, FlowActivityFactory>()
            .AddScoped<DirectorFactory, DirectorFactory>()
            .AddScoped<ICustomActions, CustomActions>()
            .AddMemoryCache()
            .AddSingleton<IRulesCache>(sp =>
            {
                IMemoryCache cache = sp.GetRequiredService<IMemoryCache>();
                if (!cache.TryGetValue<IRulesCache>("rules", out IRulesCache rulesCache))
                {
                    ILogger<Startup> logger = sp.GetRequiredService<ILogger<Startup>>();
                    //long before = GC.GetTotalMemory(false);
                    rulesCache = Bsl.Flow.Rules.RulesService.LoadRules().Result;
                    //long after = GC.GetTotalMemory(false);
                    //long size = after - before;
                    logger.LogInformation($"Setting rules cache: {DateTimeOffset.Now}");
                    cache.Set("rules", rulesCache, TimeSpan.FromHours(1));
                }

                return rulesCache;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contoso.Bsl v1"));
            }

            app.UseRouting();

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
