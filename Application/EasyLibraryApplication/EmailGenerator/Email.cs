using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace EmailGenerator
{
    public class Email
    {
        private MailMessage mailMessage;
        private string sender;
        public Email(string senderMail, string recipientMail)
        {
            mailMessage = new MailMessage(senderMail,recipientMail);
            mailMessage.IsBodyHtml = true;
            sender = senderMail;
        }

        public Task SendEmail(Bitmap image, string text)
        {
            SmtpClient client = new SmtpClient();
            client.Port = 25;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential()
            {
                UserName = sender,
                Password = "km58986702976"
            };
            ImageConverter imageConverter = new ImageConverter();
            Byte[] imageBytes = (Byte[]) imageConverter.ConvertTo(image, typeof(Byte[]));
            MemoryStream imaMemoryStream = new MemoryStream(imageBytes);
            mailMessage.Subject = "Podaci o rezervaciji";
            mailMessage.Body = text;                                                                                            
            mailMessage.Attachments.Add(new Attachment(imaMemoryStream, contentType:new ContentType()
            {
                MediaType = MediaTypeNames.Image.Jpeg,
                Name = "ReservationQrCode.jpeg"
            }));
            return Task.Run(() => client.Send(mailMessage));
        }

        
    }
}
