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
    /// <summary>
    /// Interaction logic for Pic.xaml
    /// </summary>
    public partial class PicItem : UserControl
    {
        private Image? picImg;
        private string path;
        public PicItem(string pathToImageFile, ListBox parent)
        {
            //Height="{Binding Height, ElementName=PicList, Mode=OneWay}"

            InitializeComponent();
            picImg = FindName("PicImg") as Image;
            Binding b = new Binding();
            b.Mode = BindingMode.OneWay;
            b.Source = parent.Height;
            if (picImg != null)
            {
                picImg.SetBinding(HeightProperty, b);
                picImg.Source = new BitmapImage(new Uri(pathToImageFile));
            }
            path = pathToImageFile;
        }

        public PicItem()
        {
            InitializeComponent();
            picImg = FindName("PicImg") as Image;
            path = "";
        }
    }
}
