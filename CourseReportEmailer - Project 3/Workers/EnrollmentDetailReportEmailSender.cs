using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace CourseReportEmailer.Workers
{
    internal class EnrollmentDetailReportEmailSender
    {

        public void Send(string fileName)
        {
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            
            NetworkCredential creds = new NetworkCredential("your-email@gmail.com","123YourPassword");
            client.EnableSsl = true;
            client.Credentials = creds;

            MailMessage message = new MailMessage();

            message.From = new MailAddress("your-email@gmail.com");     
            message.Subject = "Enrollment Details Report";
            message.IsBodyHtml = true;
            message.Body = "Hi,<br><br>Attached please find the enrollment details report.<br><br>Thanks. . .";

            Attachment attachment = new Attachment(fileName);
            message.Attachments.Add(attachment);

            message.To.Add("final-email@gmail.com");

            client.Send(message);
        }
        
    }
}
