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
        private string senderPassword;

        /// <summary>
        /// Konstruktor kojim se stvara objekt e-maila
        /// </summary>
        /// <param name="senderMail">Mail pošiljatelja (radi samo s gmail adresama)</param>
        /// <param name="senderPassword">Mail lozinka pošiljatelja</param>
        /// <param name="recipientMail">Mail primatelja</param>
        public Email(string senderMail,string senderPassword, string recipientMail)
        {
            this.mailMessage = new MailMessage(senderMail,recipientMail);
            this.mailMessage.IsBodyHtml = true;
            this.sender = senderMail;
            this.senderPassword = senderPassword;
        }

        /// <summary>
        /// Metoda koja šalje mail s tekstom i slikom
        /// </summary>
        /// <param name="image">Slika</param>
        /// <param name="text">Tekst</param>
        /// <returns></returns>
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
                Password = senderPassword
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
