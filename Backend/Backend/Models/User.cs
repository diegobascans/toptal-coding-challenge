using Backend.Models.Enums;

namespace Backend.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public int CaloriesLimit { get; set; }

        public Role Role { get; set; }

        public ICollection<Meal> Meals { get; set; }

        public ICollection<UserCheatingFood> UserCheatingFoods { get; set; }
    }
}
