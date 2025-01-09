using AutoMapper;
using Mango.Services.ProductAPI.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace Mango.Services.ProductAPI.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        //public static void AddApplicationServices(this WebApplicationBuilder builder)
        //{
        //    builder.Services.AddHttpClient<ICouponService, CouponService>();
        //}

        public static WebApplicationBuilder AddDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
            });

            return builder;
        }
        public static WebApplicationBuilder AddMapper(this WebApplicationBuilder builder)
        {
            IMapper mapper = MappingConfigs.RegisterMaps().CreateMapper();
            builder.Services.AddSingleton(mapper);
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return builder;
        }

        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            var secret = builder.Configuration.GetValue<string>("ApiSettings:Secret");
            var issuer = builder.Configuration.GetValue<string>("ApiSettings:Issuer");
            var audience = builder.Configuration.GetValue<string>("ApiSettings:Audience");

            var key = Encoding.ASCII.GetBytes(secret!);

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                };
            });

            return builder;
        }

        public static WebApplicationBuilder AddSwaggerGen(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(option =>
            {
                option.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter the Bearer Authorization string as following: 'Bearer {your token}'",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        }, new string[] {}
                    }
                });
            });

            return builder;
        }
    }
}
