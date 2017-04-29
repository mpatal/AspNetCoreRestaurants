using System.Collections.Generic;
using System.Linq;
using OdeToFood.Context;
using OdeToFood.Entities;

namespace OdeToFood.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant restaurant);
        void Commit();
    }

    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _context;
        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.SingleOrDefault(x => x.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            _context.Add(restaurant);
            return restaurant;
        }

        public void Commit()
        {
            _context.SaveChanges(); //TODO:: implement unit of work with dependency injection
        }
    }

    public class InMemoryRestaurantData :IRestaurantData
    {
        private static readonly List<Restaurant> Restaurants;
        static InMemoryRestaurantData()
        {
            Restaurants = new List<Restaurant>()
            {
                new Restaurant
                {
                    Id = 1,
                    Name = "The House of Kobe"
                },
                new Restaurant
                {
                    Id = 2,
                    Name = "Max"
                },
                new Restaurant
                {
                    Id = 3,
                    Name = "King's Contrivance"
                }
            };
        }
        public IEnumerable<Restaurant> GetAll()
        {
            return Restaurants;
        }

        public Restaurant Get(int id)
        {
            var restaurant = Restaurants.FirstOrDefault(x=>x.Id == id);
            return restaurant;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = Restaurants.Max(r => r.Id) + 1;
            Restaurants.Add(restaurant);
            return restaurant;
        }

        public void Commit()
        {
            //no op
        }
    }
}
