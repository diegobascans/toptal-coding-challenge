namespace Backend.Models.Responses.Admin
{
    public class GetGeneralInformationResponse
    {
        public List<Meal> Meals { get; set; }
        public int TotalMeals { get; set; }
    }
}
