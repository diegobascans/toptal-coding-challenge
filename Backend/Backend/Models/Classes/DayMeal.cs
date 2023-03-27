namespace Backend.Models.Classes
{
    public class DayMeal
    {
        public int Calories { get; set; }

        public DateTime Date { get; set; }

        public bool LimitExceeded { get; set; }
    }
}
