namespace Backend.Models.Classes
{
    public class CaloriesPerUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Calories { get; set; }
    }
}
