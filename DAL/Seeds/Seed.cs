using J6.DAL.Database;
using J6.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace J6.DAL.Seeds
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, DbContainer _context)
        {
            if (await userManager.Users.AnyAsync()) return;
            var userData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/UserseedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            var SellersData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/SellersData.json");
            var sellers = JsonSerializer.Deserialize<List<AppUser>>(SellersData);

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Customer"},
                new AppRole{Name = "Seller"},
                new AppRole{Name = "Moderator"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "P@$$w0rd");
                await userManager.AddToRoleAsync(user, "Customer");
            }

            foreach (var seller in sellers)
            {
                seller.UserName = seller.UserName.ToLower();
                await userManager.CreateAsync(seller, "P@$$w0rd");
                await userManager.AddToRoleAsync(seller, "Seller");
            }

            AppUser admin = new AppUser
            {
                UserName = "admin",
                Email = "Admain@J6.com",
                FirstName = "Abu",
                LastName = "AlAdamin"
            };

            await userManager.CreateAsync(admin, "P@$$w0rd");
            await userManager.AddToRoleAsync(admin, "Admin");
            //############################################################################

            var CategoriesData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/CategoriesData.json");
            var Categories = JsonSerializer.Deserialize<List<Category>>(CategoriesData);

            await _context.AddRangeAsync(Categories);
            await _context.SaveChangesAsync();

            var SubCategoriesData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/SubCategoriesData.json");
            var SubCategories = JsonSerializer.Deserialize<List<SubCategory>>(SubCategoriesData);

            await _context.AddRangeAsync(SubCategories);
            await _context.SaveChangesAsync();

            var BrandsData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/BrandData.json");
            var Brands = JsonSerializer.Deserialize<List<Brand>>(BrandsData);

            await _context.AddRangeAsync(Brands);
            await _context.SaveChangesAsync();

            var ProductssData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/ProductsData.json");
            var Products = JsonSerializer.Deserialize<List<Product>>(ProductssData);

            await _context.AddRangeAsync(Products);
            await _context.SaveChangesAsync();

            var BagsData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/BagData.json");
            var Bags = JsonSerializer.Deserialize<List<SavedBag>>(BagsData);

            await _context.AddRangeAsync(Bags);
            await _context.SaveChangesAsync();

            var BagsAssistantData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/BagAssistantData.json");
            var BagsAssistant = JsonSerializer.Deserialize<List<MiddleSavedProduct>>(BagsAssistantData);

            await _context.AddRangeAsync(BagsAssistant);
            await _context.SaveChangesAsync();

            var ViewsData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/ViewsData.json");
            var Views = JsonSerializer.Deserialize<List<View>>(ViewsData);

            await _context.AddRangeAsync(Views);
            await _context.SaveChangesAsync();

            var CartData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/CartsData.Json");
            var Catrts = JsonSerializer.Deserialize<List<Cart>>(CartData);

            await _context.AddRangeAsync(Catrts);
            await _context.SaveChangesAsync(); 

            var CartProdsData = await System.IO.File.ReadAllTextAsync("DAL/Seeds/CartProductsData.json");
            var ProdCarts = JsonSerializer.Deserialize<List<ProdCart>>(CartProdsData);

            await _context.AddRangeAsync(ProdCarts);
            await _context.SaveChangesAsync();
        } 
        
    }
}
