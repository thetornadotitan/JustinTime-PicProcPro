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
            if (result == DialogResult.OK)
            {
                String path = dialog.SelectedPath;
                MainWindow.instance.cwd = path;
                MainWindow.instance.holderRef.BuildFromCurrentDirectory();
            }
        }

        private BitmapImage GetBitMapFromMagickImage(MagickImage m)
        {
            BitmapSource? bitmap = new ImageSourceConverter().ConvertFrom(m.ToByteArray()) as BitmapSource;

            BitmapEncoder encoder = new PngBitmapEncoder();
            MemoryStream memorystream = new MemoryStream();
            BitmapImage tmpImage = new BitmapImage();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(memorystream);

            tmpImage.BeginInit();
            tmpImage.StreamSource = new MemoryStream(memorystream.ToArray());
            tmpImage.EndInit();

            memorystream.Close();

            return tmpImage;
        }

        private void RotateLBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.instance == null || MainWindow.instance.ImagePreview == null) return;

            MagickImage m = new MagickImage(MainWindow.instance.ImagePreview.GetImage());
            m.Rotate(-90);

            MainWindow.instance.ImagePreview.UpdateImage(GetBitMapFromMagickImage(m), MainWindow.instance.ImagePreview.GetCurrentImameName());
        }

        private void RotateRBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.instance == null || MainWindow.instance.ImagePreview == null) return;

            MagickImage m = new MagickImage(MainWindow.instance.ImagePreview.GetImage());
            m.Rotate(90);

            MainWindow.instance.ImagePreview.UpdateImage(GetBitMapFromMagickImage(m), MainWindow.instance.ImagePreview.GetCurrentImameName());
        }

        private void FlipHorizontalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.instance == null || MainWindow.instance.ImagePreview == null) return;

            MagickImage m = new MagickImage(MainWindow.instance.ImagePreview.GetImage());
            m.Flop();

            MainWindow.instance.ImagePreview.UpdateImage(GetBitMapFromMagickImage(m), MainWindow.instance.ImagePreview.GetCurrentImameName());
        }

        private void FlipVerticalBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.instance == null || MainWindow.instance.ImagePreview == null) return;

            MagickImage m = new MagickImage(MainWindow.instance.ImagePreview.GetImage());
            m.Flip();

            MainWindow.instance.ImagePreview.UpdateImage(GetBitMapFromMagickImage(m), MainWindow.instance.ImagePreview.GetCurrentImameName());
        }
    }
}
