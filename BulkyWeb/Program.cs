using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository;
using Bulky.DataAccess.Repository.IRepository;
//using BulkyWeb.Data;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);         /////    This method initializes a new instance of the WebApplicationBuilder class, which is used to configure the application.

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<ApplicationDbContext>(options=> 
			options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();


			var app = builder.Build();     /// After configuring the services, the app variable is built by calling builder.Build(). This creates the application's host.  

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();     ///   enable HTTPS redirection and serving static files, respectively.
			app.UseStaticFiles();          /// enables HTTPS redirection and serving static files , respectively

			app.UseRouting();    ///  adds routing middleware to the pipeline, enabling the application to match incoming requests to appropriate controller actions

			app.UseAuthorization();   //// app.UseAuthorization() adds authorization middleware to the pipeline, enabling authentication and authorization checks for incoming requests.

			app.MapControllerRoute(
				name: "default",
				pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}
