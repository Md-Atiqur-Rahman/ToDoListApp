using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using TodoListApi.Handeler;
using TodoListApi.Helper;
using TodoListApi.InMemoryDbContext;
using TodoListApi.InMemoryDbContext.Entities;
using TodoListApi.Interfaces;
using TodoListApi.Requirements;
using TodoListApi.Services;

namespace TodoListApi
{
    public class Startup
    {
        public static Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        //readonly string _AllowSpecificOrigins = "_AllowSpecificOrigins";
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services )
        {


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                   .AddJwtBearer(options =>
                   {
                       options.TokenValidationParameters = new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateIssuerSigningKey = true,
                           ValidIssuer = TokenHelper.Issuer,
                           ValidAudience = TokenHelper.Audience,
                           IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(TokenHelper.Secret))
                       };

                   });


            services.AddAuthorization(options =>
            {
                options.AddPolicy("OnlyNonBlockedUser", policy =>
                {
                    policy.Requirements.Add(new UserBlockedStatusRequirement(false));

                });
            });
            services.AddSingleton<IAuthorizationHandler, UserBlockedStatusHandler>();
            services.AddScoped<IUserInfo, UserInfoService>();
            services.AddScoped<ITodoItemService, TodoItemService>();

            services.AddDbContext<MemoryDbContext>(context =>
            {
                context.UseInMemoryDatabase("InMemoryDatabase");

            });
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(15);
            });
            services.AddControllers();
            services.AddControllersWithViews();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TodoListApi", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
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
                        Array.Empty<string>()
                    }
                });
            });
            //end

            services.AddCors();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(_AllowSpecificOrigins, builder => builder
            //     .SetIsOriginAllowed(_ => true)
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .AllowCredentials()
            //    );
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoListApi v1"));
            }
            app.UseSession();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseRouting();
            app.UseCors(options =>
            options.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
            //app.UseCors(_AllowSpecificOrigins);
            

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers().RequireCors(_AllowSpecificOrigins);
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });


            var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<MemoryDbContext>();
            //context.Database.EnsureDeleted();

            SeedData(context);
            SeedDataForTodoType(context);
            SeedDataForToDO(context);
        }
        public static void SeedData (MemoryDbContext context)
        {
            var filePath = _hostingEnvironment.ContentRootPath + "\\jsonFile\\User.json";
            var jsonData = System.IO.File.ReadAllText(filePath);
            var users = JsonConvert.DeserializeObject<List<User>>(jsonData) ?? new List<User>();
            foreach (var item in users)
            {
                context.Users.Add(item);
            }
            context.SaveChanges();
        }
        public static void SeedDataForToDO(MemoryDbContext context)
        {
            var filePath = _hostingEnvironment.ContentRootPath + "\\jsonFile\\TodoItem.json";
            var jsonData = System.IO.File.ReadAllText(filePath);
            var todoItems = JsonConvert.DeserializeObject<List<TodoItem>>(jsonData) ?? new List<TodoItem>();
            foreach (var item in todoItems)
            {
                context.TodoItems.Add(item);
            }
            context.SaveChanges();
        }

        public static void SeedDataForTodoType(MemoryDbContext context)
        {

            var filePath_TodoType = _hostingEnvironment.ContentRootPath + "\\jsonFile\\TodoType.json";
            var jsonData = System.IO.File.ReadAllText(filePath_TodoType);
            var todoTypes = JsonConvert.DeserializeObject<List<TodoType>>(jsonData) ?? new List<TodoType>();
            foreach (var item in todoTypes)
            {
                context.TodoTypes.Add(item);
            }
            context.SaveChanges();
        }
    }
}
