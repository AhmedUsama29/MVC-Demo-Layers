using System.Net;
using System.Net.Mail;

namespace Demo.Presentation.Helper
{
    public static class EmailSettings
    {

        public static bool SendEmail(Email email)
        {
            try
            {
                var client = new SmtpClient("smtp.gmail.com", 587);
                client.EnableSsl = true;

                client.Credentials = new NetworkCredential("ahmedabobasha23@gmail.com", "iwltuqiybffxggon");

                client.Send(from: "ahmedabobasha23@gmail.com", recipients: email.To, subject: email.Subject, body: email.Body);

                return true;
            }
            catch (Exception) 
            { 
            
                return false;
            }

        }

    }
}
