using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models
{
    public class Meal
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Calories { get; set; }

        [Required]
        public int FoodId { get; set; }
        
        public Food Food { get; set; }

        [Required]
        public int UserId { get; set; }

        public User User { get; set; }
    }
}
