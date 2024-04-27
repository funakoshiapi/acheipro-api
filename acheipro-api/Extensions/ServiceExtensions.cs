using System;
using System.Text;
using AspNetCoreRateLimit;
using Contracts;
using Entities.ConfigurationModels;
using Entities.Models;
using LoggerService;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Repository;
using Service;
using Service.Contracts;

namespace acheipro_api.Extensions
{
	public static class ServiceExtensions
	{

		public static void ConfigureCors(this IServiceCollection services) => services.AddCors (options =>
		{
			options.AddPolicy("CorsPolicy", builder =>
			builder.AllowAnyOrigin()
			.AllowAnyMethod()
			.AllowAnyHeader()
			.WithExposedHeaders("X-Pagination"));
		});

		
		public static void ConfigureIISIntegration(this IServiceCollection services) => services.Configure<IISOptions>(options =>
		{
			
		});

		public static void ConfigureLoggerService(this IServiceCollection services) =>
			services.AddSingleton<ILoggerManager, LoggerManager>();

		public static void ConfigureRepositoryManager(this IServiceCollection services) =>
			services.AddScoped<IRepositoryManager, RepositoryManager>();

		public static void ConfigureServiceManager(this IServiceCollection services) =>
			services.AddScoped<IServiceManager, ServiceManager>();

		public static void ConfigurePostgresSqlContext(this IServiceCollection services, IConfiguration configuration)
		{

			var connectionString = new NpgsqlConnectionStringBuilder(configuration.GetConnectionString("DefaultConnection"));
            //connectionString.Password = Environment.GetEnvironmentVariable("DbPassword");

            services.AddDbContext<RepositoryContext>(options => options.UseNpgsql(connectionString.ToString(),
                x => x.MigrationsAssembly("acheipro-api")));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
			

		public static void ConfigureResponseCaching(this IServiceCollection services) => services.AddResponseCaching();

		public static void ConfigureHttpCacheHeaders(this IServiceCollection services) => services.AddHttpCacheHeaders(
			(expirationOpt) =>
			{
				expirationOpt.MaxAge = 5;
				expirationOpt.CacheLocation = CacheLocation.Private;
			},
			(validationOpt) =>
			{
				validationOpt.MustRevalidate = true;
			});

		public static void ConfigureRateLimitingOptions(this IServiceCollection services)
		{
			var rateLimitRules = new List<RateLimitRule>
			{
				new RateLimitRule
				{
					Endpoint = "*",
					Limit = 60,
					Period = "3m"
				}
			};

			services.Configure<IpRateLimitOptions>(opt => { opt.GeneralRules = rateLimitRules; });
			services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
			services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddSingleton<IProcessingStrategy,	AsyncKeyLockProcessingStrategy>();


        }


		public static void ConfigureIdentity(this IServiceCollection services)
		{
			var builder = services.AddIdentity<User, IdentityRole>(o =>
			{
                o.User.RequireUniqueEmail = true;
                o.Password.RequireDigit = true;
				o.Password.RequireLowercase = false;
				o.Password.RequireUppercase = false;
				o.Password.RequireNonAlphanumeric = false;
				o.Password.RequiredLength = 10;
				
			})
			.AddEntityFrameworkStores<RepositoryContext>()
			.AddDefaultTokenProviders();
		}

		public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
		{
			var jwtConfiguration = new JwtConfiguration();
			configuration.Bind(jwtConfiguration.Section, jwtConfiguration);

            var secretKey = Environment.GetEnvironmentVariable("SECRET");

			services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfiguration.ValidIssuer,
                    ValidAudience = jwtConfiguration.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ae68ba8bc4999f150eaf1e520382fadd"))
                };
            });
        }

		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(s =>
			{
				s.SwaggerDoc("v1", new OpenApiInfo
				{
					Title = "Acheipro API",
					Version = "v1",
					Description="Acheipro API by LuandaLabs",
					TermsOfService = new Uri("https://example.com/terms"),
					Contact = new OpenApiContact
					{
						Name = "Funakoshi",
						Email = "luandalabs@gmail.com",
						Url = new Uri("https://www.luandalabs.com"),
					},
					License = new OpenApiLicense
					{
						Name = "Acheipro API LICX",
						Url = new Uri("https://www.luandalabs.com/license")
					}


				});

                var xmlFile = $"{typeof(AcheiProApi.Presentation.AssemblyReference).Assembly.GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);

                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					In = ParameterLocation.Header,
					Description = "Place to add JWT with Bearer",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				s.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},

							Name = "Bearer",
						},
						new List<string>()
					}
				});
			});
		}

		
	}
}

 