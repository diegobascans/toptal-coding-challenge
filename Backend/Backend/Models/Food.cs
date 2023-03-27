namespace Backend.Models
{
    public class Food
    {
        public int Id { get; set; }
        
        public string Description { get; set; }

        public ICollection<UserCheatingFood> UserCheatingFoods { get; set; }
    }
}
