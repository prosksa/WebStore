using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Implementations;
using WebStore.Infrastructure.Implementations.Cart;
using WebStore.Infrastructure.Implementations.Sql;
using WebStore.Infrastructure.Interfaces;

namespace WebStore
{
	public class Startup
	{
		/// <summary>
		/// Добавляем свойство для доступа к конфигурации
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Добавляем новый конструктор, принимающий интерфейс IConfiguration
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			//Добавляем сервисы, необходимые для mvc
			services.AddMvc();

			//Добавляем разрешение зависимости
			services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
			services.AddScoped<IProductData, SqlProductData>();
			services.AddScoped<IOrdersService, SqlOrdersService>();

			// Добавляем EF Core
			services.AddDbContext<WebStoreContext>(options => options.UseSqlServer(
				Configuration.GetConnectionString("DefaultConnection")));

			// Настройка Identity
			services.AddIdentity<User, IdentityRole>()
				.AddEntityFrameworkStores<WebStoreContext>()
				.AddDefaultTokenProviders();

			services.Configure<IdentityOptions>(options =>
			{
				// Password settings
				options.Password.RequiredLength = 6;

				// Lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.AllowedForNewUsers = true;

				// User settings
				options.User.RequireUniqueEmail = true;
			});

			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.Cookie.Expiration = TimeSpan.FromDays(150);

				options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
				options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout

				options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
				options.SlidingExpiration = true;
			});


			//Настройки для корзины
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<ICartService, CookieCartService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//Добавляем расширение для использования статических файлов, т.к. appsettings.json - это статический файл
			app.UseStaticFiles();

			app.UseWelcomePage("/welcome");

			app.UseAuthentication();

			//Добавляем обработку запросов в mvc-формате
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "areas",
					template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);

				routes.MapRoute(
					name: "default",
					template: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
