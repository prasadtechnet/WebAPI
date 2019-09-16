using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SampleAPI.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace SampleAPI.Installers
{
    public class MVCInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {
            //JWT
            var jwtSettings = new JwtSettings();
            Configuration.Bind(nameof(jwtSettings), jwtSettings);

            services.AddSingleton(jwtSettings);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(cog => {
                cog.SaveToken = true;
                cog.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    RequireExpirationTime = false,
                    ValidateLifetime = true

                };
            });


            //MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Swagger
            services.AddSwaggerGen(config => {
                                
                config.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Order Api",
                    Description = "Test API",
                    TermsOfService = "https://example.com/terms",
                    Contact = new Contact { Name = "prasad", Email = string.Empty, Url = "https://twitter.com/spboyer" },
                    License = new License { Name = "lic1", Url = "https://example.com/licence" }
                });

                var security = new Dictionary<string, IEnumerable<string>> {
                    {"Bearer",new string[0] }
                };

                config.AddSecurityDefinition("Bearer", new ApiKeyScheme {
                    Description="Jwt Authorization for Order Api",
                    Name="Authorization",
                    In="header",
                    Type="apiKey"

                });
                config.AddSecurityRequirement(security);
            });
        }
    }
}
