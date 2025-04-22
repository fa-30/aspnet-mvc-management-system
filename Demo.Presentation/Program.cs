using Demo.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Demo.DAL.Repositories.classes;
using Demo.DAL.Repositories.Interfaces;
using Demo.BLL.Services.classes;
using Demo.BLL.Services.Interfaces;
using Demo.BLL.Profiles;
using Microsoft.AspNetCore.Mvc;
using Demo.BLL.Services.AttachementService;
using Microsoft.AspNetCore.Identity;
using Demo.DAL.Models.IdentityModels;
using Demo.Presentation.Setting;
using Demo.Presentation.Helpers;
using Microsoft.AspNetCore.Authentication.Google;
namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region Add services to the container.
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }
                );
            //builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(Options =>
            { 
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                Options.UseLazyLoadingProxies();
            },ServiceLifetime.Scoped);
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.User.RequireUniqueEmail = true;
            } ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.Services.Configure<MailSettings>(
                builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddTransient<IMailServices, MailServices>();
            builder.Services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = GoogleDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
            }).AddGoogle(o => { IConfiguration GoogleAuthSection = builder.Configuration.GetSection("Authentication:Google");
                o.ClientId = GoogleAuthSection["ClientId"];
                o.ClientSecret = GoogleAuthSection["ClientSecret"];
            }
            );
            #endregion
            var app = builder.Build();

            #region Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");
            #endregion
            app.Run();
        }
    }
}
