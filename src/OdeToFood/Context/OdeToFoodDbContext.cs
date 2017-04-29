using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OdeToFood.Entities;

namespace OdeToFood.Context
{
    public class OdeToFoodDbContext : IdentityDbContext<User>
    {
        public OdeToFoodDbContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<Restaurant> Restaurants { get; set; }
    }
}
