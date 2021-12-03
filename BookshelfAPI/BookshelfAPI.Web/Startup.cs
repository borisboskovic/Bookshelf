using Azure.Storage.Blobs;
using BookshelfAPI.Data;
using BookshelfAPI.Data.Models;
using BookshelfAPI.Services;
using BookshelfAPI.Services.Helpers;
using BookshelfAPI.Services.Interfaces;
using BookshelfAPI.Services.Interfaces.Admin;
using BookshelfAPI.Services.Services;
using BookshelfAPI.Services.Services.Admin;
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
                options.SignIn.RequireConfirmedEmail = false;
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
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IAzureStorageService, AzureStorageService>();
            services.AddTransient<IBookDetailsService, BookDetailsService>();
            services.AddTransient<IAuthorDetailsService, AuthorDetailsService>();
            services.AddTransient<IBookReviewService, BookReviewService>();
            services.AddTransient<IBookListService, BookListService>();
            services.AddTransient<IHomePageService, HomePageService>();

            // Email configuration
            services.AddSingleton(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            
            // Azure BLOB client
            services.AddSingleton(x => new BlobServiceClient(Configuration.GetConnectionString("AzureBlobStorage")));

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                    builder.WithOrigins(new string[] { Configuration["TokenConstants:Audience"] });
                });
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddFluentValidation(fv =>
                    fv.RegisterValidatorsFromAssemblyContaining<Startup>()
            );

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1.1", new OpenApiInfo
                {
                    Title = "Bookshelf API",
                    Version = "v1.1",
                    Contact = new OpenApiContact
                    {
                        Name = "Boris Boskovic",
                        Email = "boris.boskovic92@gmail.com",
                    }
                });
                c.IgnoreObsoleteActions();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.1/swagger.json", "Bookshelf API v1.1");
                c.DocumentTitle = "Bookshelf API";
            });
        }
    }
}
