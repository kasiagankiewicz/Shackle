﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Shackle.Core.Factories;
using Shackle.Core.Factories.Hash;
using Shackle.Core.Services;
using Shackle.Host.Framework;

namespace Shackle.Host
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddDataAnnotations()
                .AddJsonFormatters()
                .AddJsonOptions(o => o.SerializerSettings.Formatting = Formatting.Indented);

            services.AddCors(options =>
            {
                options.AddPolicy("Cors", cors =>
                    cors.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddHostedService<BlockchainHostedService>();

            services.AddSingleton<IBlockchainRunner, BlockchainRunner>();
            services.AddSingleton<IHashGenerator, HashGenerator>();
            services.AddSingleton<IAccountService, AccountService>();
            services.AddSingleton<IHashInputProvider, HashInputProvider>();
            services.AddSingleton<ICryptoFactory, CryptoFactory>();
            services.AddSingleton<ISigner, Signer>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("Cors");
            app.UseMvc();
        }
    }
}
