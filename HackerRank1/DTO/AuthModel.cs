namespace HackerRank1.DTO
{
    public class AuthModel
    {
        public record TokenResponse(string Token);
        public record UserCredential(string Email, string Password);
    }
}
