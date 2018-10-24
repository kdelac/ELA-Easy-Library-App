using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AForge.Video;
using AForge.Video.DirectShow;
using EasyLibraryApplication.WPF.Annotations;
using EasyLibraryApplication.WPF.Helper;
using QrCodeGenerator;
using Newtonsoft;
using Newtonsoft.Json;
using EasyLibraryApplication.WPF.Model;
using System.Data.Entity;

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for AffirmatingReservationView.xaml
    /// </summary>
    public partial class AffirmatingReservationView : Page
    {
        public LibraryEntities ctx;
        private FilterInfoCollection popisWebCamera;
        private VideoCaptureDevice FinalFrame;
        private Bitmap qrCode;
        private Reservation rezervacija;

     

        public AffirmatingReservationView()
        {
            InitializeComponent();
            this.DataContext = this;
            ctx = new LibraryEntities();
            popisWebCamera = new FilterInfoCollection(FilterCategory.VideoInputDevice);
        }

        public void OpenCamera()
        {
            FinalFrame = new VideoCaptureDevice(popisWebCamera[0].MonikerString);
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();

        }

        private void FinalFrame_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            QrCode code = new QrCode();
            //var video = (Bitmap)eventArgs.Frame.Clone();
            qrCode = (Bitmap)eventArgs.Frame.Clone();
            //img.Source = video.ToBitmapImage();
            // ispis.Text = code.ReadQrCode(video2);

            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    bi = bitmap.ToBitmapImage();
                    
                }
                bi.Freeze(); // avoid cross thread operations and prevents leaks
                Dispatcher.BeginInvoke(new ThreadStart(delegate { img.Source = bi; }));
                
            }
            catch (Exception exc)
            {
                MessageBox.Show("Neuspjelo");
            }

            
        }

        private void StopCamera()
        {
            if (FinalFrame != null && FinalFrame.IsRunning)
            {
                FinalFrame.SignalToStop();
                FinalFrame.NewFrame -= new NewFrameEventHandler(FinalFrame_NewFrame);
            }
        }

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Metoda zadužena za implementaciju INotifyPropertyChanged sučelja
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenCamera();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StopCamera();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            QrCode citac = new QrCode();
            try
            {
                string qrCodeString = citac.ReadQrCode(qrCode);
                if (qrCodeString == "Neuspjelo" || qrCodeString == "Ne valja")
                {
                    ispis.Text = qrCodeString;
                }
                else
                {
                    ispis.Text = "";
                    ctx.Books.Load();
                    ctx.Users.Load();
                    rezervacija = JsonConvert.DeserializeObject<Reservation>(qrCodeString);
                    Book knjiga = ctx.Books.Where(x => x.Id == rezervacija.BookId).FirstOrDefault();
                    User user = ctx.Users.Where(x => x.Id == rezervacija.UserId).FirstOrDefault();
                    bookName.Text = "Naziv knjige: " + knjiga.Title;
                    userName.Text = "Ime korisnika: " + user.Name;
                   
                }
            }
            catch (Exception ex)
            {

                ispis.Text = ex.Message;
            }
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ctx.Reservations.Load();
            
            Copy odabranaKopija;
            Loan loan = new Loan();
            if (inventoryNumber.Text != "" || inventoryNumber.Text != null)
            {
                ctx.Reservations.SqlQuery($"DELETE * FROM Reservations WHERE UserId ={rezervacija.UserId} AND BookId ={rezervacija.BookId} ");
                odabranaKopija = ctx.Copies.Where(x => x.InventoryNumber == inventoryNumber.Text).FirstOrDefault();
                 loan.CopyId = odabranaKopija.Id;
                loan.LoanDate = DateTime.Now;
                loan.EndLoanDate = DateTime.Now.AddDays(30);
                loan.UserId = rezervacija.UserId;
                ctx.SaveChanges();
            }
            else
            {
                MessageBox.Show("Niste unijeli inventarski broj!!!");
            }

            ctx.Loans.Add(loan);
            ctx.SaveChanges();
            MessageBox.Show("Uspjesno izdana knjiga");
            rezervacija = null;
            inventoryNumber.Clear();
            userName.Clear();
            bookName.Clear();
        }
    }
}
