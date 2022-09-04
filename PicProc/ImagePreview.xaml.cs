using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PicProc
{
    public partial class ImagePreview : UserControl
    {
        Image? previewImage;
        TextBlock? imageName;
        public ImagePreview()
        {
            InitializeComponent();
            previewImage = FindName("PreviewImage") as Image;
            imageName = FindName("PreviewImageName") as TextBlock;
        }

        public void UpdateImage(BitmapImage img, string name)
        {
            if (previewImage == null || imageName == null) return;

            previewImage.Source = img;

            if(name !=null)
                imageName.Text = name;
        }

        public string GetCurrentImameName()
        {
            if(imageName != null)
                return imageName.Text;

            return "";
        }

        public byte[] GetImage()
        {
            if (previewImage == null || previewImage.Source == null) return new byte[0];

            MemoryStream memStream = new MemoryStream();
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(previewImage.Source as BitmapImage));
            encoder.Save(memStream);
            return memStream.ToArray();
        }
    }
}
