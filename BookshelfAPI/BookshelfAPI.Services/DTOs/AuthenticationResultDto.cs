namespace BookshelfAPI.Services.DTOs
{
    public class AuthenticationResultDto
    {
        public int StatusCode { get; set; }
        public string TokenJson { get; set; }
    }
}
