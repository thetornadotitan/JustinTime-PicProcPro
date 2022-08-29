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
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace PicProc
{
    public partial class PicHolder : UserControl
    {
        ListBox? listBox = null;
        List<PicItem> listBoxItems = new List<PicItem>();
        public PicHolder()
        {
            InitializeComponent();
            listBox = FindName("PicList") as ListBox;
            if(listBox != null)
                listBox.ItemsSource = listBoxItems;
        }

        public void BuildImageListFromSku(string sku)
        {
            ClearItems();
            string[] foundImages = FindImagesFromSku(sku);
            PopulateListWithPics(foundImages);
        }

        private string[] FindImagesFromSku(string sku)
        {
            if (MainWindow.instance == null) return new string[0];

            List<string> images = new List<string>();
            string pattern = MainWindow.instance.cwd.Replace(@"\", @"\\")+@"\\"+sku;
            Trace.WriteLine("Pattern: " + pattern);

            foreach (string path in Directory.GetFiles(MainWindow.instance.cwd))
            {
                Match m = Regex.Match(path, pattern);
                if (m.Success)
                {
                    images.Add(path);
                }
            }

            return images.ToArray();
        }

        private void PopulateListWithPics(string[] picPaths)
        {
            if (listBox == null) return;

            foreach (string picPath in picPaths)
            {
                listBoxItems.Add(new PicItem(picPath, listBox));
            }
            
            listBox.Items.Refresh();
        }

        private void ClearItems()
        {
            listBoxItems.Clear();
            if (listBox != null)
                listBox.Items.Refresh();
        }
    }
}
