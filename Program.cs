using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace DutchTreat
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);			
            builder.Host.ConfigureAppConfiguration(AddConfiguration);
			IConfiguration config = builder.Configuration;
			Console.WriteLine();

            var services = builder.Services;			
			
			
			//Configure identity
            services.AddIdentity<StoreUser, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<DutchContext>()
            .AddDefaultTokenProviders();

			services.AddAuthentication()
				.AddCookie()
				.AddJwtBearer(cfg=>cfg.TokenValidationParameters = new TokenValidationParameters()
				{
                    ValidIssuer = config["Tokens:Issuer"],
                    ValidAudience = config["Tokens:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]))
				});

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

			if (app.Environment.IsDevelopment()/*.IsEnvironment("Development")*/)
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
			app.UseAuthentication();
			app.UseAuthorization();

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

        private static void RunSeeding(WebApplication app)
        {
			var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
			using(var scope = scopeFactory.CreateScope())
			{
                var seeder = scope.ServiceProvider.GetService<DutchSeeder>();
                seeder.SeedAsync().Wait();
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