using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OngProject.Core.Helper;
using OngProject.Core.Interfaces;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System.Text;
using OngProject.DataAccess;
using Microsoft.EntityFrameworkCore;
using OngProject.Core.Business;
using Amazon.S3;
using OngProject.Core.Mapper;
using System.Collections.Generic;

namespace OngProject
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
            services.AddAWSService<IAmazonS3>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OngProject", Version = "v1" });
                c.AddSecurityDefinition("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "Ingrese Bearer [Token] para autentificarse en la aplicacion"
                    });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new List<string>()
                    }
                });
            });

            services.AddDbContext<AppDbContext>((services, options) => {
                options.UseInternalServiceProvider(services);
                options.UseSqlServer(this.Configuration["SqlConnectionString"]);
                options.UseLazyLoadingProxies();
            });

            services.AddEntityFrameworkProxies();
            services.AddEntityFrameworkSqlServer();            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IActivitiesService, ActivitiesService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentsService, CommentsService>();
            services.AddScoped<IMemberService, MemberService>();
            services.AddScoped<INewsService, NewsService>();
            services.AddScoped<IOrganizationsService, OrganizationService>();
            services.AddScoped<IRolesService, RolesService>();
            services.AddScoped<ITestimonialsService, TestimonialsService>();
            services.AddScoped<IUserService, UsersService>();
            services.AddScoped<ISlideSerivice, SlideService>();
            services.AddScoped<IS3AwsHelper, S3AwsHelper>();
            services.AddScoped<IContactService, ContactService>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            services.AddScoped<IJwtHelper, JwtHelper>();
            services.AddScoped<IEntityMapper, EntityMapper>();


            // JWT Token Generator
            var key = Encoding.ASCII.GetBytes(Configuration["JWT:Secret"]);
            services
            .AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OngProject v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();


            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}