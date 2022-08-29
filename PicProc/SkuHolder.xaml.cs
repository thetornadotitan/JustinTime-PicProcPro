using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
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
using System.Diagnostics;

namespace PicProc
{
    public class ProductInfo
    {
        public string productName = "";
        public string productSku = "";

        public ProductInfo(string sku, string name)
        {
            productName = name;
            productSku = sku;
        }
    }

    public partial class SkuHolder : UserControl
    {
        private Dictionary<string, int> validExtensions = new Dictionary<string, int>()
        {
            {"jpg", 0},
            {"jpeg", 0},
            {"png", 0},
            {"jfif", 0},
            {"webp", 0}
        };

        private List<SkuItem> skuItems = new List<SkuItem>();
        private ListBox? listBox;

        public SkuHolder()
        {
            InitializeComponent();
            listBox = FindName("ListBox") as ListBox;

            if (listBox != null)
            {
                listBox.Items.Clear();
                listBox.ItemsSource = skuItems;
            }

        }

        public void BuildFromCurrentDirectory()
        {
            string[] validFileNames = GetValidFileNames();
            Dictionary<string, int> uniqueFileName = GetUniqueFileNames(validFileNames);
            ProductInfo[] currentProducts = GetProductInfoFromFileNames(uniqueFileName);
            UpdateList(currentProducts);
        }

        private string[] GetValidFileNames()
        {
            List<string> validFiles = new List<string>();

            if (MainWindow.instance == null) return validFiles.ToArray();

            foreach(string file in Directory.GetFiles(MainWindow.instance.cwd))
            {
                string result = "";

                //Remove lead path
                string[] tokens = file.Split('\\');
                result = tokens[tokens.Length-1];

                //Remove Extension and Test Extension
                tokens = result.Split('.');
                string extension = tokens[1].ToLower();

                if (validExtensions.ContainsKey(extension))
                {
                    //Remove letter
                    result = tokens[0];
                    result = result.Substring(0, result.Length - 1);
                    validFiles.Add(result);
                }
            }

            return validFiles.ToArray();
        }

        private Dictionary<string, int> GetUniqueFileNames(string[] validFileNames) 
        {
            Dictionary<string, int> uniqueNames = new Dictionary<string, int>();

            foreach (string vfn in validFileNames)
            {
                if (uniqueNames.ContainsKey(vfn)) continue;
                uniqueNames.Add(vfn, 0);
            }

            return uniqueNames;
        }
    
        private ProductInfo[] GetProductInfoFromFileNames(Dictionary<string, int> uniqueFileNames)
        {
            List<ProductInfo> result = new List<ProductInfo>();

            //This is where we would connect to the databse, for now connecting to JSON
            byte[] jsonBytes = File.ReadAllBytes("C:\\Users\\Isaac\\Desktop\\Test Case Folder\\database.json");
            JsonDocument doc = JsonDocument.Parse(jsonBytes);
            JsonElement root = doc.RootElement;
            //----

            foreach(JsonProperty elm in root.EnumerateObject())
            {
                if(uniqueFileNames.ContainsKey(elm.Name))
                {
                    result.Add(new ProductInfo(elm.Name, elm.Value.GetProperty("ProductName").GetString()));
                }
            }

            return result.ToArray();
        }
    
        private void UpdateList(ProductInfo[] products)
        {
            skuItems.Clear();
            foreach(ProductInfo p in products)
            {
                SkuItem si = new SkuItem(p.productSku, p.productName);
                skuItems.Add(si);
            }

            if(listBox != null)
                listBox.Items.Refresh();
        }
    }
}
