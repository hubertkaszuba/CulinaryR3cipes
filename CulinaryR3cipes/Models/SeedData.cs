using CulinaryR3cipes.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CulinaryR3cipes.Models
{
    public static class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder app)
        {
            ApplicationDbContext context = app.ApplicationServices
                .GetRequiredService<ApplicationDbContext>();

            ProductRepository productRepository = new ProductRepository(context);
            CategoryRepository categoryRepository = new CategoryRepository(context);
            RecipeRepository recipeRepository = new RecipeRepository(context);
            IngredientRepository ingredientRepository = new IngredientRepository(context);
            TypeRepository typeRepository = new TypeRepository(context);

            context.Database.Migrate();

            if(!context.Recipes.Any())
            {
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

                categoryRepository.AddCategory(dairyProduct);
                categoryRepository.AddCategory(vegetable);
                categoryRepository.AddCategory(fruit);
                categoryRepository.AddCategory(wheatProduct);
                categoryRepository.AddCategory(meat);

                Product eggs = new Product { Name = "Jajka", Measure = "szt.", Category = dairyProduct };
                Product apple = new Product { Name = "Jabłko", Measure = "szt.", Category = fruit };
                Product pasta = new Product { Name = "Makaron", Measure = "g", Category = wheatProduct };
                Product chicken = new Product { Name = "Pierś z kurczaka", Measure = "g", Category = meat };
                Product mushrooms = new Product { Name = "Pieczarki", Measure = "g", Category = vegetable };
                Product sourCream = new Product { Name = "Śmietana", Measure = "ml", Category = dairyProduct };
                Product carrot = new Product { Name = "Marchew", Measure = "szt.", Category = vegetable };

                productRepository.AddProduct(eggs);
                productRepository.AddProduct(apple);
                productRepository.AddProduct(pasta);
                productRepository.AddProduct(chicken);
                productRepository.AddProduct(mushrooms);
                productRepository.AddProduct(sourCream);
                productRepository.AddProduct(carrot);

                Recipe pastaWithChicken = new Recipe { Name = "Makaron z kurczakiem i pieczarkami", Type = dinner, Description = "Zrób makaron", Time = 30 };

                recipeRepository.AddRecipe(pastaWithChicken);

                ingredientRepository.AddIngredients(new List<Ingredient> {
                    new Ingredient { Product = chicken, Quantity = 200, Recipe = pastaWithChicken},
                    new Ingredient { Product = pasta, Quantity = 200, Recipe = pastaWithChicken },
                    new Ingredient { Product = mushrooms, Quantity = 100, Recipe = pastaWithChicken},
                    new Ingredient { Product = sourCream, Quantity = 200, Recipe = pastaWithChicken}
                });
            }


        }
    }
}
