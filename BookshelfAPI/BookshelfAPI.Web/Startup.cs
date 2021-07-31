using BookshelfAPI.Data;
using BookshelfAPI.Data.Models;
using BookshelfAPI.Services;
using BookshelfAPI.Services.Helpers;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BookshelfAPI.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            services.AddDbContext<BookshelfDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Bookshelf"));
            });

            services.AddIdentityCore<BookshelfUser>(options =>
            {
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 8;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.Lockout.AllowedForNewUsers = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<BookshelfDbContext>()
                .AddSignInManager<SignInManager<BookshelfUser>>()
                .AddDefaultTokenProviders();

            //Authentication
            var secret = Configuration["TokenConstants:Secret"]; //TODO: Use user secrets for this
            var secretBytes = Encoding.UTF8.GetBytes(secret);
            var key = new SymmetricSecurityKey(secretBytes);
            services.AddAuthentication("OAuth")
                .AddJwtBearer("OAuth", options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configuration["TokenConstants:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = Configuration["TokenConstants:Audience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                    };
                });

            //Authorization
            services.AddAuthorizationCore(options =>
            {
                var defaultAuthorizationBuilder = new AuthorizationPolicyBuilder();
                var defaultAuthorizationPolicy = defaultAuthorizationBuilder
                    .AddAuthenticationSchemes(new string[] { "OAuth" })
                    .RequireAuthenticatedUser()
                    .RequireClaim(JwtRegisteredClaimNames.Sub)
                    .RequireClaim(JwtRegisteredClaimNames.Email)
                    .RequireClaim("role")
                    .RequireClaim(JwtRegisteredClaimNames.Iat)
                    .Build();

                options.DefaultPolicy = defaultAuthorizationPolicy;
            });

            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources/Localization";
            });

            //Application services
            services.AddTransient<IUserService, UserService>();
            services.AddSingleton(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

            services.AddControllers().AddNewtonsoftJson();

            services.AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>()
            );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bookshelf Web API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "BookshelfAPI");
            });
        }
    }
}
