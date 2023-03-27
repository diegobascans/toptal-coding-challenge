using Backend.Models.Classes;

namespace Backend.Models.Responses.Users
{
    public class GetUserMealsResponse
    {
        public List<DayMeal> CaloriesPerDay { get; set; }
        public int Count { get; set; }
    }
}
