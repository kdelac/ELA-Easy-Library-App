//using EasyLibraryApplication.WPF.Annotations;
//using System.ComponentModel;
//using System.Runtime.CompilerServices;
//using AForge.Video;
//using AForge.Video.DirectShow;
//using System.Windows.Controls;
//using EasyLibraryApplication.WPF.Commands;
//using System.Drawing;
//using QrCodeGenerator;
//using System.Windows.Media.Imaging;

//namespace EasyLibraryApplication.WPF.ViewModel
//{
//    public class AffirmatingReservationViewModel : INotifyPropertyChanged
//    {
//        private FilterInfoCollection popisWebCamera;
//        private BitmapImage image;

//        public AffirmatingReservationViewModel()
//        {
//            CameraEvent = new CameraCommand(this);
//            popisWebCamera = new FilterInfoCollection(FilterCategory.VideoInputDevice);
//        }

//        #region Command

//        public CameraCommand CameraEvent { get; set; }

//        #endregion

//        public void OpenCamera()
//        {
//            VideoCaptureDevice FinalFrame = new VideoCaptureDevice(popisWebCamera[0].m);
//            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
//            FinalFrame.Start();

//        }

//        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
//        {
//            QrCode code = new QrCode();
//            var video = (Bitmap)eventArgs.Frame.Clone();
//            var video2 = (Bitmap)eventArgs.Frame.Clone();
//            image = video;
//            var result = code.ReadQrCode(video2);
//        }


//        #region PropertyChangedEventHandler

//        public event PropertyChangedEventHandler PropertyChanged;

//        /// <summary>
//        /// Metoda zadužena za implementaciju INotifyPropertyChanged sučelja
//        /// </summary>
//        /// <param name="propertyName"></param>
//        [NotifyPropertyChangedInvocator]
//        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
//        {
//            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
//        }

//        #endregion
//    }
//}
