namespace BookshelfAPI.Data.Helpers
{
    public class LocalizationCodes
    {
        public static readonly int Success = 1;

        public static readonly int RegisterFail_Default = 1000;
        public static readonly int RegisterFail_EmailInUse = 1001;
        public static readonly int RegisterFail_PasswordTooShort = 1002;
        public static readonly int LoginFail_UserNotFound = 1003;
        public static readonly int LoginFail_WrongPassword = 1004;
    }
}
