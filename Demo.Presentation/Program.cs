using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services.Classes;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Contexts;
using Demo.DataAccess.Repositories;
using Demo.DataAccess.Repositories.Classes;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container

            builder.Services.AddControllersWithViews(options => {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            builder.Services.AddDbContext<AppDBContext>(options => {
                options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                options.UseLazyLoadingProxies();
                //options.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"));

            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();

            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            //builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            builder.Services.AddAutoMapper(p => p.AddProfile(new MappingProfiles()));

            #endregion

            var app = builder.Build();

            #region Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            } 
            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}