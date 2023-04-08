using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext ctx;
        private readonly IWebHostEnvironment env;
        private readonly UserManager<StoreUser> userManager;

        public DutchSeeder(DutchContext ctx, IWebHostEnvironment env,UserManager<StoreUser> userManager)
        {
            this.ctx = ctx;
            this.env = env;
            this.userManager = userManager;
        }

        public async Task SeedAsync()
        {
            ctx.Database.EnsureCreated();

            StoreUser user = await userManager.FindByEmailAsync(/*"shawn@dutchtreat.com"*/"admin@email");
            if(user == null)            {

                user = new StoreUser()
                {
                    /*FirstName="Shawn",
                    LastName="Wildermuth",
                    Email="shawn@dutchtreat.com",
                    UserName= "shawn@dutchtreat.com"*/
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@email",
                    UserName = "admin@email"
                };

                var result = await userManager.CreateAsync(user,"P@ssw0rd!");
                if(result != IdentityResult.Success) 
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }



            if (!ctx.Products.Any())
            {
                //need to create sample data
                var filePath = Path.Combine(env.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonSerializer.Deserialize<IEnumerable<Product>>(json);

                ctx.Products.AddRange(products);

                var order = ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();

                if (order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                ctx.SaveChanges();
            }
        }
    }
}
