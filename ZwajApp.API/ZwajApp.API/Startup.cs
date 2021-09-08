using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ZwajApp.API.Data;
using ZwajApp.API.Helpers;

namespace ZwajApp.API
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
            services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddJsonOptions(option => {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddCors();
            services.AddAutoMapper(); // تستخدم لربط DTos مع controller
            services.AddTransient<TrailData>();
            //اضافة خدمة جديدة وتبني نسخة جديدة في كل مرة يتم استدعاء على المستودع
            // المشكلة في حالة الطلبات المتزامنة بحصل فيها مشكلة 
            // لو زاد عدد الركوست مابنفضلها
            //services.AddSingleton
            //مناسب للخدمات الخفيفة 
            // لانه كل مرة بتم طلب من المستودع وبعمل طلب جديد للمستعمل لكل ركوست
            // services.AddTransient
            //AddSingleton  نفس 
            //تدعم الطلبات المتزامنة بصيرش مشكلة  لانها بتعمل انستنس لكل عملية طلب وبكون في سرعة
            services.AddScoped<IAuthRepository, AuthRepository>();
            //services.AddScoped<IZwajRepository, ZwajRepository>();

            //اضافة middelware authontication
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(Options =>
             {
                 Options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                     ValidateIssuer = false,
                     ValidateAudience = false

                 };
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, TrailData TrailData)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // app.UseHsts();


                //production mode  هذا الكود بالاسفل حتى يتم معالجة الاخطاء في حال كان في  
                // try cashe وهذا يستخدم حتى لا يتم عمل معالجة للاخطاء  عن طريق
                app.UseExceptionHandler(BuilderExtensions =>
                {
                BuilderExtensions.Run(async context=>
                 {
                     context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                     var error = context.Features.Get<IExceptionHandlerFeature>();
                     if (error != null)
                     {
                         context.Response.AddApplicationError(error.Error.Message);
                         await context.Response.WriteAsync(error.Error.Message);
                     }
                  }); 
               });
                
            }
           // TrailData.TrialUsers();
            app.UseHttpsRedirection();
            //بناء الصلاحيات بينه وبين SPA
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
