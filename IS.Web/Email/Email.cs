using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IS.Web.Components.ViewModels;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace IS.Web.Email
{
    public class Email
    {
        private IOptions<EmailSettingsModel> _configOptions;


        public Email(IOptions<EmailSettingsModel> configOptions)
        {
            _configOptions = configOptions;
        }


        public void SendPasswordResetEmail( string link, string emailAddress )
        {



            var message = new MimeMessage( );
            message.From.Add( new MailboxAddress( "georgebudwick@gmail.com" ) );
            message.To.Add( new MailboxAddress( emailAddress ) );
            message.Subject = "Ignition Safety - Reset";

            var bodyBuilder = new BodyBuilder( );
            bodyBuilder.HtmlBody = "<a href='" + _configOptions.Value.FromDomain + link +
                                   "'>Click here to reset you password</a>";
            

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
