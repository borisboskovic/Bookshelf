using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using System.Text;
using System.Threading;

namespace BookshelfAPI.Services.Helpers
{
    public static class EmailMessageHelper
    {
        public static MimeMessage CreateEmailConfirmationMessage(
            string from,
            string to,
            string token,
            string baseURL,
            IStringLocalizer<UserService> localizer)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(to));
            message.Subject = localizer["account_confirmation_email_subject"];
            message.From.Add(new MailboxAddress(from));

            var emailBase64UrlEncoded = Base64UrlEncoder.Encode(to);
            var tokenBase64UrlEncoded = Base64UrlEncoder.Encode(token);
            var link = $"{baseURL}confirm-email?m={emailBase64UrlEncoded}&t={tokenBase64UrlEncoded}";
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = CreateConfirmationMessageBody(link, baseURL, localizer),
            };

            return message;
        }

        private static string CreateConfirmationMessageBody(string link, string baseURL, IStringLocalizer<UserService> localizer)
        {
            var sb = new StringBuilder();
            sb.Append($@"
            <html>
                <head>
                    <title>Account confirmation email</title>
                </head>
                <body>
                    <div>
                        <p>{localizer["account_confirmation_email_message"]}</p>
                        <p>
                            <a href='{link}'>{link}</a>
                        </p>
                    </div>
                </body>
            </html>
            ");

            return sb.ToString();
        }
    }
}
