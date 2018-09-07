using EasyLibraryApplication.WPF.Annotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Windows.Controls;
using EasyLibraryApplication.WPF.Commands;

namespace EasyLibraryApplication.WPF.ViewModel
{
    public class AffirmatingReservationViewModel : INotifyPropertyChanged
    {

        public AffirmatingReservationViewModel()
        {
            CameraEvent = new CameraCommand(this);
        }

        #region Command

        public CameraCommand CameraEvent { get; set; }

        #endregion

        public void OpenCamera()
        {
            VideoCaptureDevice FinalFrame = new VideoCaptureDevice();
            FinalFrame.NewFrame += new NewFrameEventHandler(FinalFrame_NewFrame);
            FinalFrame.Start();

        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            System.Drawing.Image img;
            img = (Bitmap)eventArgs.Frame.Clone();
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
    }
}
