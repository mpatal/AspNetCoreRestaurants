using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OdeToFood.Entities;

namespace OdeToFood.Context
{
    public static class OdeToFoodContextExtensions
    {
        public static void EnsureSeedData(this OdeToFoodDbContext context)
        {
            if (!context.Restaurants.Any(x=>x.Name == "Max"))
            {
                var restaurant = new Restaurant
                {
                    Cuisine = CuisineType.Filipino,
                    Name = "Max"
                };
                context.Restaurants.Add(restaurant);
            }

            context.SaveChanges();
        }
    }
}
