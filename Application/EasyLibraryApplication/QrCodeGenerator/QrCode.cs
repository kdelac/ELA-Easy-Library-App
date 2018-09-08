using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZXing;

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

        /// <summary>
        /// Citanje podataka iz QR koda
        /// </summary>
        /// <param name="qrCode"></param>
        /// <returns></returns>
        public string ReadQrCode(Bitmap qrCode)
        {
            IBarcodeReader reader = new BarcodeReader();

            var result = reader.Decode(qrCode);
            if (result != null)
            {
                return result.Text;
            }
            return "Neuspjelo";
        }
    }
}
