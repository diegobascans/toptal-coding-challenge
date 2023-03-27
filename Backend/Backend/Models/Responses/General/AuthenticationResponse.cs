namespace Backend.Models.Responses.General
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }
        public User User { get; set; }
    }
}
