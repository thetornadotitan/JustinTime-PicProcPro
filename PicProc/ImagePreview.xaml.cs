using System;
using System.Collections.Generic;
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
            imageName.Text = name;
        }
    }
}
