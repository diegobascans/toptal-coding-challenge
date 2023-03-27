namespace Backend.Models
{
    public class UserCheatingFood
    {
        public int FoodId { get; set; }
        
        public int UserId { get; set; }

        public Food Food { get; set; }

        public User User { get; set; }
    }
}
