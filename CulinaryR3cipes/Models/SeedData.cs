﻿using CulinaryR3cipes.Models.Repositories;
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

                typeRepository.Add(supper);
                typeRepository.Add(dinner);
                typeRepository.Add(breakfast);

                Category dairyProduct = new Category { Name = "Nabiał" };
                Category fruit = new Category { Name = "Owoc" }; ;
                Category wheatProduct = new Category { Name = "Produkt pszenny" };
                Category meat = new Category { Name = "Mięso" };
                Category vegetable = new Category { Name = "Warzywo" };
                Category spices = new Category { Name = "Przyprawy" };

                categoryRepository.Add(dairyProduct);
                categoryRepository.Add(vegetable);
                categoryRepository.Add(fruit);
                categoryRepository.Add(wheatProduct);
                categoryRepository.Add(meat);
                categoryRepository.Add(spices);

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
                Product banana = new Product { Name = "Banan", Measure = "szt.", Category = fruit };
                Product rice = new Product { Name = "Ryż", Measure = "g", Category = wheatProduct };
                Product spinach = new Product { Name = "Szpinak", Measure = "g", Category = vegetable };
                Product broccoli = new Product { Name = "Brokuł", Measure = "g", Category = vegetable };

                productRepository.Add(eggs);
                productRepository.Add(apple);
                productRepository.Add(pasta);
                productRepository.Add(chicken);
                productRepository.Add(mushrooms);
                productRepository.Add(sourCream);
                productRepository.Add(carrot);
                productRepository.Add(milk);
                productRepository.Add(flour);
                productRepository.Add(sugar);
                productRepository.Add(bread);
                productRepository.Add(ham);
                productRepository.Add(oatmealFlakes);
                productRepository.Add(butter);
                productRepository.Add(banana);

                Recipe pastaWithChicken = new Recipe { Name = "Makaron z kurczakiem i pieczarkami", Type = dinner, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 30, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\kurczak_pieczarki.jpg")) };
                Recipe breadWithHam = new Recipe { Name = "Kanapka z szynką", Type = breakfast, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 10, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\kanapka_szynka.jpg")) };
                Recipe pancakes = new Recipe { Name = "Naleśniki", Type = dinner, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 20, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\nalesniki.jpg")) };
                Recipe porridge = new Recipe { Name = "Owsianka", Type = breakfast, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 15, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\owsianka.jpg")) };
                Recipe omelette = new Recipe { Name = "Omlet", Type = breakfast, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 15, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\omlet.jpg")) };
                Recipe scrambledEggs = new Recipe { Name = "Jajecznica", Type = breakfast, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 15, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\jajecznica.jpg")) };
                Recipe riceWithChickenAndSpinach = new Recipe { Name = "Kurczak z ryżem i szpinakiem", Type = breakfast, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 45, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\kurczakRyżSzpinak.jpg")) };
                Recipe riceWithChickenCarrotBroccoli = new Recipe { Name = "Kurczak z ryżem, marchewką i brokułem", Type = breakfast, Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed at mattis dui. Duis vitae rhoncus lacus. Duis luctus eleifend dolor, ut facilisis magna vehicula ac. Vestibulum nec suscipit magna, et gravida sem. Quisque quis malesuada felis, in luctus tortor. Aenean pulvinar id turpis vel volutpat. Nullam libero urna, ultrices a mollis sit amet, ultricies at erat. Mauris nec pharetra neque. Praesent ipsum turpis, dictum vitae massa pulvinar, eleifend rhoncus tellus. Sed interdum elementum rhoncus. Sed ac nisl varius, faucibus sem eget, tempus risus. Maecenas eu leo eu massa consectetur venenatis nec sed lorem.", Time = 45, Img = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, @"wwwroot\seedImages\kurczakRyżMarchewkaBrokuł.jpg")) };

                recipeRepository.Add(pastaWithChicken);
                recipeRepository.Add(breadWithHam);
                recipeRepository.Add(pancakes);
                recipeRepository.Add(porridge);
                recipeRepository.Add(omelette);
                recipeRepository.Add(scrambledEggs);
                recipeRepository.Add(riceWithChickenAndSpinach);

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

                ingredientRepository.AddIngredients(new List<Ingredient> {
                    new Ingredient { Product = milk, Quantity = 250, Recipe = porridge},
                    new Ingredient { Product = oatmealFlakes, Quantity = 150, Recipe = porridge },
                    new Ingredient { Product = apple, Quantity = 1, Recipe = porridge},
                });

                ingredientRepository.AddIngredients(new List<Ingredient> {
                    new Ingredient { Product = milk, Quantity = 50, Recipe = omelette},
                    new Ingredient { Product = oatmealFlakes, Quantity = 150, Recipe = omelette },
                    new Ingredient { Product = banana, Quantity = 1, Recipe = omelette},
                    new Ingredient { Product = eggs, Quantity = 3, Recipe = omelette}
                });

                ingredientRepository.AddIngredients(new List<Ingredient> {
                    new Ingredient { Product = milk, Quantity = 30, Recipe = scrambledEggs},
                    new Ingredient { Product = butter, Quantity = 5, Recipe = scrambledEggs },
                    new Ingredient { Product = ham, Quantity = 4, Recipe = scrambledEggs},
                    new Ingredient { Product = eggs, Quantity = 3, Recipe = scrambledEggs}
                });

                ingredientRepository.AddIngredients(new List<Ingredient> {
                    new Ingredient { Product = rice, Quantity = 100, Recipe = riceWithChickenAndSpinach},
                    new Ingredient { Product = butter, Quantity = 5, Recipe = riceWithChickenAndSpinach },
                    new Ingredient { Product = chicken, Quantity = 250, Recipe = riceWithChickenAndSpinach},
                    new Ingredient { Product = spinach, Quantity = 100, Recipe = riceWithChickenAndSpinach}
                });

                ingredientRepository.AddIngredients(new List<Ingredient> {
                    new Ingredient { Product = rice, Quantity = 100, Recipe = riceWithChickenCarrotBroccoli},
                    new Ingredient { Product = chicken, Quantity = 250, Recipe = riceWithChickenCarrotBroccoli},
                    new Ingredient { Product = broccoli, Quantity = 100, Recipe = riceWithChickenCarrotBroccoli},
                    new Ingredient { Product = carrot, Quantity = 3, Recipe = riceWithChickenCarrotBroccoli}
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
