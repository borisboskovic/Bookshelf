using BookshelfAPI.Helpers;

namespace BookshelfAPI.Services.DTOs.User
{
    public class UserInfoDto : ActionResult
    {
        public string Id { get; set; }
        public string Avatar { get; set; }
    }
}
