﻿using BookshelfAPI.Helpers;

namespace BookshelfAPI.Services.DTOs
{
    public class AuthenticationResultDto : ActionResult
    {
        public string Token { get; set; }
    }
}
