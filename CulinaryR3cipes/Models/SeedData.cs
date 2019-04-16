using CulinaryR3cipes.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public static class SeedData
    {
        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();

            context.Database.Migrate();

            if(!context.Recipes.Any())
            {
                ProductRepository productRepository = new ProductRepository(context);
                CategoryRepository categoryRepository = new CategoryRepository(context);
                RecipeRepository recipeRepository = new RecipeRepository(context);
                IngredientRepository ingredientRepository = new IngredientRepository(context);
                TypeRepository typeRepository = new TypeRepository(context);

                Type breakfast = new Type { Name = "Śniadanie" };
                Type dinner = new Type { Name = "Obiad" };
                Type supper = new Type { Name = "Kolacja" };

                typeRepository.AddType(breakfast);
                typeRepository.AddType(dinner);
                typeRepository.AddType(supper);

                Category dairyProduct = new Category { Name = "Nabiał" };
                Category fruit = new Category { Name = "Owoc" }; ;
                Category wheatProduct = new Category { Name = "Produkt pszenny" };
                Category meat = new Category { Name = "Mięso" };
                Category vegetable = new Category { Name = "Warzywo" };
                Category spices = new Category { Name = "Przyprawy" };

                categoryRepository.AddCategory(dairyProduct);
                categoryRepository.AddCategory(vegetable);
                categoryRepository.AddCategory(fruit);
                categoryRepository.AddCategory(wheatProduct);
                categoryRepository.AddCategory(meat);
                categoryRepository.AddCategory(spices);

                Product eggs = new Product { Name = "Jajka", Measure = "szt.", Category = dairyProduct };
                Product apple = new Product { Name = "Jabłko", Measure = "szt.", Category = fruit };
                Product pasta = new Product { Name = "Makaron", Measure = "g", Category = wheatProduct };
                Product chicken = new Product { Name = "Pierś z kurczaka", Measure = "g", Category = meat };
                Product mushrooms = new Product { Name = "Pieczarki", Measure = "g", Category = vegetable };
                Product sourCream = new Product { Name = "Śmietana", Measure = "ml", Category = dairyProduct };
                Product carrot = new Product { Name = "Marchew", Measure = "szt.", Category = vegetable };
                Product milk = new Product { Name = "Mleko", Measure = "ml", Category = dairyProduct };
                Product flour = new Product { Name = "Mąka", Measure = "g", Category = wheatProduct };
                Product sugar = new Product { Name = "Cukier", Measure = "g", Category = spices };
                Product bread = new Product { Name = "Chleb", Measure = "szt.", Category = wheatProduct };
                Product ham = new Product { Name = "Szynka", Measure = "szt.", Category = meat };
                Product oatmealFlakes = new Product { Name = "Płatki owsiane", Measure = "g", Category = wheatProduct };
                Product butter = new Product { Name = "Masło", Measure = "g", Category = dairyProduct };

                productRepository.AddProduct(eggs);
                productRepository.AddProduct(apple);
                productRepository.AddProduct(pasta);
                productRepository.AddProduct(chicken);
                productRepository.AddProduct(mushrooms);
                productRepository.AddProduct(sourCream);
                productRepository.AddProduct(carrot);
                productRepository.AddProduct(milk);
                productRepository.AddProduct(flour);
                productRepository.AddProduct(sugar);
                productRepository.AddProduct(bread);
                productRepository.AddProduct(ham);
                productRepository.AddProduct(oatmealFlakes);
                productRepository.AddProduct(butter);

                
                Recipe pastaWithChicken = new Recipe { Name = "Makaron z kurczakiem i pieczarkami", Type = dinner, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 30, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\kurczak_pieczarki.jpg")) };
                Recipe breadWithHam = new Recipe { Name = "Kanapka z szynką", Type = breakfast, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 10, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\kanapka_szynka.jpg")) };
                Recipe pancakes = new Recipe { Name = "Naleśniki", Type = dinner, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 20, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\nalesniki.jpg")) };

                recipeRepository.AddRecipe(pastaWithChicken);
                recipeRepository.AddRecipe(breadWithHam);
                recipeRepository.AddRecipe(pancakes);

                ingredientRepository.AddIngredients(new List<Ingredient> {
                    new Ingredient { Product = chicken, Quantity = 200, Recipe = pastaWithChicken},
                    new Ingredient { Product = pasta, Quantity = 200, Recipe = pastaWithChicken },
                    new Ingredient { Product = mushrooms, Quantity = 100, Recipe = pastaWithChicken},
                    new Ingredient { Product = sourCream, Quantity = 200, Recipe = pastaWithChicken}
                });

                ingredientRepository.AddIngredients(new List<Ingredient> {
                    new Ingredient { Product = bread, Quantity = 3, Recipe = breadWithHam},
                    new Ingredient { Product = butter, Quantity = 10, Recipe = breadWithHam },
                    new Ingredient { Product = ham, Quantity = 3, Recipe = breadWithHam}
                });

                ingredientRepository.AddIngredients(new List<Ingredient> {
                    new Ingredient { Product = milk, Quantity = 250, Recipe = pancakes},
                    new Ingredient { Product = eggs, Quantity = 3, Recipe = pancakes },
                    new Ingredient { Product = flour, Quantity = 200, Recipe = pancakes},
                    new Ingredient { Product = sugar, Quantity = 10, Recipe = pancakes}
                });             
            }
        }

        public static async Task CreateRoles(IApplicationBuilder app, IConfiguration configuration)
        {
            var userManager = app.ApplicationServices.GetService<UserManager<User>>();
            var roleManager = app.ApplicationServices.GetService<RoleManager<IdentityRole>>();

            if (!roleManager.Roles.Any())
            {
                string[] roleNames = { "Admin", "User" };
                IdentityResult roleResult;

                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);

                    if (!roleExist)
                        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }

                var admin = new User
                {
                    UserName = configuration["UserSettings:UserEmail"],
                    Email = configuration["UserSettings:UserEmail"]
                };

                string userPassword = configuration["UserSettings:UserPassword"];

                var user = await userManager.FindByEmailAsync(configuration["UserSettings:UserEmail"]);

                if (user == null)
                {
                    var createAdmin = await userManager.CreateAsync(admin, userPassword);
                    var emailVerifiactionCode = await userManager.GenerateEmailConfirmationTokenAsync(admin);

                    await userManager.ConfirmEmailAsync(admin, emailVerifiactionCode);
                    if (createAdmin.Succeeded)
                        await userManager.AddToRoleAsync(admin, "Admin");
                }
            }
        }
    }
}
