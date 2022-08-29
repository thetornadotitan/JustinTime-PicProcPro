using ImageMagick;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicProc
{
    /// <summary>
    /// Interaction logic for ImageControls.xaml
    /// </summary>
    public partial class ImageControls : System.Windows.Controls.UserControl
    {
        public ImageControls()
        {
            InitializeComponent();
        }
        private void SelectFolderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.instance == null || MainWindow.instance.holderRef == null) return;

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                String path = dialog.SelectedPath;
                MainWindow.instance.cwd = path;
                MainWindow.instance.holderRef.BuildFromCurrentDirectory();
            }
        }
        private void RotateLBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.instance == null || MainWindow.instance.ImagePreview == null) return;

            MagickImage m = new MagickImage(MainWindow.instance.ImagePreview.GetImage());
            m.Rotate(-90);
            BitmapSource? bitmap = new ImageSourceConverter().ConvertFrom(m.ToByteArray()) as BitmapSource;

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.QualityLevel = 100;
            MemoryStream memorystream = new MemoryStream();
            BitmapImage tmpImage = new BitmapImage();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(memorystream);

            tmpImage.BeginInit();
            tmpImage.StreamSource = new MemoryStream(memorystream.ToArray());
            tmpImage.EndInit();

            memorystream.Close();

            MainWindow.instance.ImagePreview.UpdateImage(tmpImage, "Rotated");
        }
    }
}
