using MimeKit;
using System.Text;

namespace BookshelfAPI.Services.Helpers
{
    public static class EmailMessageHelper
    {
        public static MimeMessage CreateEmailConfirmationMessage(string from, string to, string token)
        {
            var message = new MimeMessage();
            message.To.Add(new MailboxAddress(to));
            message.Subject = "Bookshelf - Account confirmation link";
            message.From.Add(new MailboxAddress(from));
            message.Body = new TextPart(CreateConfirmationMessageBody(token));
            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = CreateConfirmationMessageBody(token),
            };

            return message;
        }

        private static string CreateConfirmationMessageBody(string token)
        {
            var sb = new StringBuilder();
            sb.Append(@"
            <html>
                <head>
                    <title>Account confirmation email</title>
                </head>
                <body>
            ");

            sb.Append(token);

            sb.Append(@"
                </body>
            </html>
            ");

            return sb.ToString();
        }
    }
}
