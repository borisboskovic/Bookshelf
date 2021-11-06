namespace BookshelfAPI.Services.RequestModels.User
{
    public class ConfirmEmail_RequestModel
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
