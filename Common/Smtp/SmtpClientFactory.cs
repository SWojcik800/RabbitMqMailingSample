using System.Net;
using System.Net.Mail;

namespace Common.Smtp
{
    /// <summary>
    /// Factory for SmtpClient
    /// </summary>
    public static class SmtpClientFactory 
    {
        public static SmtpClient Create(SmtpSettings settings)
            => new SmtpClient(settings.Server, settings.Port)
            {
                Credentials = new NetworkCredential(settings.Username, settings.Password),
                EnableSsl = settings.EnableSsl
            };
    }
}
