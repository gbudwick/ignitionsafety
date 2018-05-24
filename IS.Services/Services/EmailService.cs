using IS.Web.Components.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace IS.Services.Services
{
    public class EmailService
    {
        private IOptions<EmailSettingsModel> _configOptions;


        public EmailService(IOptions<EmailSettingsModel> configOptions)
        {
            _configOptions = configOptions;
        }


        public void SendInviteEmail( string link, string emailAddress )
        {
            var message = new MimeMessage( );
            message.From.Add( new MailboxAddress( "georgebudwick@gmail.com" ) );
            message.To.Add( new MailboxAddress( emailAddress ) );
            message.Subject = "Welcome to Ignition Safety";

            var bodyBuilder = new BodyBuilder( );
            bodyBuilder.HtmlBody = "<a href='" + _configOptions.Value.FromDomain + link +
                                   "'>Click here to set your password</a>";

            message.Body = bodyBuilder.ToMessageBody( );

            using ( var client = new SmtpClient( ) )
            {
                client.Connect(
                    _configOptions.Value.SmtpServer
                   , _configOptions.Value.Port
                   , MailKit.Security.SecureSocketOptions.StartTls
                );

                ////Note: only needed if the SMTP server requires authentication
                client.Authenticate( "georgebudwick@gmail.com", "Yqw2gmbgl4$" );
                client.Send( message );
                client.Disconnect( true );
            }
        }
    }
}
