using DutchTreat.Data;
using DutchTreat.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DutchTreat
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);			
            builder.Host.ConfigureAppConfiguration(AddConfiguration);
            

            var services = builder.Services;			
			
			services.AddDbContext<DutchContext>(/*cfg=> { 
				cfg.UseSqlServer(); 
			}*/);
			services.AddTransient<DutchSeeder>();
			services.AddTransient<IMailService, NullMailService>();
			services.AddScoped<IDutchRepository, DutchRepository>();
			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddControllersWithViews();
			services.AddRazorPages()
				.AddRazorRuntimeCompilation()
				.AddNewtonsoftJson(cfg =>
				cfg.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
				);

			var app = builder.Build();

			if (app.Environment.IsEnvironment("Development"))
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/error");
			}
			//app.MapGet("/", () => "Hello World!");
			//app.UseDefaultFiles();			
			app.UseStaticFiles();
			app.UseRouting();
			app.UseEndpoints(cfg =>
			{
				cfg.MapRazorPages();
				cfg.MapControllerRoute("Default",
					"/{controller}/{action}/{id?}",
					new { controller = "App", action = "Index"}
				);
			});



			if (args.Length == 1 && args[0].ToLower() == "/seed")
			{
				RunSeeding(app);
			}
			else
			{
				app.Run();
			}


		}

        private static void RunSeeding(IHost app)
        {
			var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
			using(var scope = scopeFactory.CreateScope())
			{
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.Seed();
            }
            
        }

        private static void AddConfiguration(HostBuilderContext ctx, IConfigurationBuilder bldr)
        {
            bldr.Sources.Clear();
			bldr.SetBasePath(Directory.GetCurrentDirectory()).
				AddJsonFile("config.json").
				AddEnvironmentVariables();
        }
    }
}