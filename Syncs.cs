using InsERT.Moria.ModelDanych;
using ProductsXML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HurtonToSubiektDesktop
{
    internal class Syncs
    {
        public async static Task<List<string>> CategoryList()
        {
            Console.WriteLine("Sync.CategoryList Start.");

            var catalog = await Hurton.GetXML();

            var categorySecondaryListTest = new List<string>();
            foreach (var item in catalog.Products)
            {
                var category = item.CategorySecondary;

                if (!categorySecondaryListTest.Contains(item.CategorySecondary))
                {
                    categorySecondaryListTest.Add(item.CategorySecondary);
                }
            }

            return categorySecondaryListTest;
        }

        public static DateTime updateTextareaDate = DateTime.Now;
        public static string textareaValue = "";

        public static int progressBar = 0;

        public static bool syncInProgres = false;

        public async static void FullUpdate()
        {

            syncInProgres = true;

            Console.WriteLine("Sync.FullUpdate Start.");

            SetTextToTextarea(string.Format($"Rozpoczynam pełną synchronizację"));
            
            var catalog = await Hurton.GetXML();

            SetTextToTextarea(string.Format($"- Wczytano {catalog.Products.Length} produktów z Hurton"), true);

            Subiekt.UpdateOrInsertAssortmentsByCatalog(catalog.Products);

            Console.WriteLine("Sync.FullUpdate End.");

            syncInProgres = false;

        }

        public static void SetTextToTextarea(string text, bool append = false)
        {
            if (append)
            {
                textareaValue += "\n" + text;
            }
            else
            {
                textareaValue = text;
            }

            updateTextareaDate = DateTime.Now;
        }


    }
}
