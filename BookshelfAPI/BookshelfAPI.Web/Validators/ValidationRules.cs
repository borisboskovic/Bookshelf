using System.Linq;

namespace BookshelfAPI.Web.Validators
{
    public static class ValidationRules
    {
        public static bool ContainOnlyLetters(string @string)
        {
            return @string.All(c => char.IsLetter(c));
        }
    }
}
