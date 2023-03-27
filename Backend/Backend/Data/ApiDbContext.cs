using Backend.Common;
using Backend.Models;
using Backend.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(): base() {}

        public ApiDbContext(DbContextOptions<ApiDbContext> options): base(options) {}

        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Meal> Meals { get; set; }
        public virtual DbSet<UserCheatingFood> UserCheatingFoods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCheatingFood>().HasKey(uf => new { uf.UserId, uf.FoodId });

            modelBuilder.Entity<Food>().HasData(
                new Food { Id = 1, Description = "Meat" },
                new Food { Id = 2, Description = "Chicken" },
                new Food { Id = 3, Description = "Apple" },
                new Food { Id = 4, Description = "Fish" },
                new Food { Id = 5, Description = "Banana" },
                new Food { Id = 6, Description = "Chocolate" },
                new Food { Id = 7, Description = "Candy" },
                new Food { Id = 8, Description = "Potato" },
                new Food { Id = 9, Description = "Pizza" },
                new Food { Id = 10, Description = "Egg" },
                new Food { Id = 11, Description = "Hot dogs" },
                new Food { Id = 12, Description = "Sushi" },
                new Food { Id = 13, Description = "Ice cream" });

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "Jose", Role = Role.User, CaloriesLimit = Constants.Common.DEFAULT_CALORIES_LIMIT },
                new User { Id = 2, Username = "Jorge", Role = Role.User, CaloriesLimit = Constants.Common.DEFAULT_CALORIES_LIMIT },
                new User { Id = 3, Username = "Maxi", Role = Role.User, CaloriesLimit = Constants.Common.DEFAULT_CALORIES_LIMIT },
                new User { Id = 4, Username = "Diego", Role = Role.User, CaloriesLimit = Constants.Common.DEFAULT_CALORIES_LIMIT },
                new User { Id = 5, Username = "Martin", Role = Role.Admin, CaloriesLimit = Constants.Common.DEFAULT_CALORIES_LIMIT }
            );

            List<Meal> meals = new List<Meal>();

            DateTime startDate = DateTime.Today.AddDays(-60);
            DateTime endDate = DateTime.Today;
            TimeSpan timeSpan = endDate - startDate;

            Random rnd = new Random();
            for(int i = 1; i < 500; i++)
            {
                TimeSpan rndSpan = new TimeSpan(0, rnd.Next(0, (int)timeSpan.TotalMinutes), 0);

                meals.Add(new Meal
                {
                    Id = i,
                    Calories = rnd.Next(400, 1200),
                    FoodId = rnd.Next(1, 14),
                    UserId = rnd.Next(1, 6),
                    Date = startDate + rndSpan
                });
            }


            modelBuilder.Entity<Meal>().HasData(meals);
        }

    }
}
