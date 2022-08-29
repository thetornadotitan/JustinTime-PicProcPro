using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class SkuItem : UserControl
    {
        private TextBlock? skuTB;
        private string productName = "";
        public SkuItem(string sku, string name)
        {
            InitializeComponent();
            skuTB = FindName("SkuTB") as TextBlock;

            if(skuTB != null)
                skuTB.Text = sku;

            productName = name;
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(skuTB != null)
                Trace.WriteLine("SKU: " + skuTB.Text + " & Name: " + productName);

            if(MainWindow.instance != null && skuTB != null)
                MainWindow.instance.SkuItemClicked(skuTB.Text, productName);
        }
    }
}
