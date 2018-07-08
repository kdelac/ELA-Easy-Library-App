using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace QrCodeGenerator
{
    public class QrCode
    {
        private Bitmap qrCodeBitmap;

        /// <summary>
        /// Stvara Bitpam objekt QR koda od prosljeđenih podataka, koristi se u slučaju stvaranja slike QR koda
        /// </summary>
        /// <param name="data"></param>
        public QrCode(string data)
        {
            string uri = "http://api.qrserver.com/v1/create-qr-code/?data=" + data + "&size=500x500";
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(new Uri(uri));
            qrCodeBitmap = new Bitmap(stream);
            stream.Flush();
            stream.Close();
            client.Dispose();
        }
        /// <summary>
        /// Stvara prazan objek QR koda, stvara se u slučaju čitanja QR koda
        /// </summary>
        public QrCode()
        {
            
        }

        /// <summary>
        /// Vraća objekt slike QR koda
        /// </summary>
        /// <returns>Bitpmap objekt</returns>

        public Bitmap GetQrCodeBitmap()
        {
            return qrCodeBitmap;
        }
    }
}
