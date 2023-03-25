using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HurtonToSubiektDesktop
{
    internal class Hurton
    {
        public static string NIP = "6462490527";

        public async static Task<ProductsXML.Catalog> GetXML()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://xml.hurton.pl/");

                using (var response = await client.GetAsync("customer_api/xml/export_xml_long?login=OFFICESMART&authKey=5d37555340569a03fb5c81933b2ee5f50e3e9e04"))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var xmlString = await response.Content.ReadAsStringAsync();

                        //Console.WriteLine(xmlString);
                        string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\full_hurton.xml";

                        File.WriteAllText(path, xmlString);

                        XmlSerializer serializer = new XmlSerializer(typeof(ProductsXML.Catalog));
                        using (StringReader reader = new StringReader(xmlString))
                        {
                            var catalog = (ProductsXML.Catalog) serializer.Deserialize(reader);
                            Console.Write("XML DATE: ");
                            Console.WriteLine(catalog.Date);

                            Console.WriteLine("Ilosc produktow: {0}", catalog.Products.Length);

                            
                            return catalog;

                        }

                    }
                    else
                    {
                        Console.WriteLine("Failed to retrieve data.");
                    }
                }
            }

            return new ProductsXML.Catalog();
        }
    }
}
