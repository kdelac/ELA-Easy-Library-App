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

namespace EasyLibraryApplication.WPF.View
{
    /// <summary>
    /// Interaction logic for AffirmatingReservationView.xaml
    /// </summary>
    public partial class AffirmatingReservationView : Page , INotifyPropertyChanged
    {
        private FilterInfoCollection popisWebCamera;
       // private BitmapImage image;
        private VideoCaptureDevice FinalFrame;

        public AffirmatingReservationView()
        {
            InitializeComponent();
            this.DataContext = this;
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
            var video2 = (Bitmap)eventArgs.Frame.Clone();
            //img.Source = video.ToBitmapImage();
            //ispis.Text = code.ReadQrCode(video2);

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

           // ispis.Text = code.ReadQrCode(video2);
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
    }
}
