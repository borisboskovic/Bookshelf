using BookshelfAPI.Helpers;
using System.Collections.Generic;

namespace BookshelfAPI.Services
{
    public class ServiceResponse
    {
        public ActionResult Body { get; set; } = null;
        public bool Succeeded { get; set; } = false;
        public Dictionary<string, string[]> Errors;

        public ServiceResponse()
        {
            Errors = new();
        }
    }
}
