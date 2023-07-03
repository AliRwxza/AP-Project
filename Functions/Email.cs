using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp3
{
    public class Email
    {
        static public void SendEmail(string fromAddress, string toAddress, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage(fromAddress, toAddress, subject, body);

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                smtpClient.Credentials = new NetworkCredential(GlobalVariables.SourceEmail, GlobalVariables.SourceEmailPassword);
                smtpClient.EnableSsl = true;

                smtpClient.Send(mail);

                MessageBox.Show("Email sent successfully.");
            }
            catch
            {
                //MessageBox.Show("Failed to send E-mail");
            }
        }
    }
}
