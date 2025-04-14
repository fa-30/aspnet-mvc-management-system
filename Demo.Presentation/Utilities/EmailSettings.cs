using System.Net.Mail;
using System.Net;

namespace Demo.Presentation.Utilities
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587);
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("farahkhaled648@gmail.com", password: "qchjvcmkogtykvsz");
            Client.Send("farahkhaled648@gmail.com", email.To, email.Subject,email.Body);

        }

    }
}
