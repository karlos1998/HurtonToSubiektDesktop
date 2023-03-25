using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


using InsERT.Moria.Sfera;
using InsERT.Mox.Product;

using System.Data.SqlClient;

using System.Data;
using System.Data.Sql;
using InsERT.Moria.Asortymenty;
using InsERT.Moria.ModelDanych;
using System.Xml.Linq;
using InsERT.Moria.Dokumenty.Logistyka;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using InsERT.Moria.Zdjecia;
using System.Data.Entity.Core.Common.CommandTrees;
using ProductsXML;

namespace HurtonToSubiektDesktop
{

    /*class NexoUser
    {
        public string Nazwa { get; set; }
        public string Login { get; set; }
        public Guid Id { get; set; }
    }*/

    public class NexoUser
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public NexoUser(string text, string value)
        {
            Text = text;
            Value = value;
        }
    }

    public class FilteredAssortment
    {
        public Asortyment element { get; set; }
        public string hurton_symbol { get; set; }

        public FilteredAssortment(Asortyment _element, string _symbol)
        {
            element = _element;
            hurton_symbol = _symbol;
        }
    }

    internal class Subiekt
    {

        static string server_name;
        static bool windows_auth_enabled;
        static string windows_login;
        static string windows_pass;

        static string databaseName;

        static Uchwyt sfera;

        public static bool Login(string login, string password)
        {
            Console.WriteLine("Login Data: {0}, {1}, {2}, {3}, {4}, {5}, {6}", server_name, databaseName, windows_auth_enabled, windows_login, windows_pass, login, password);
            DanePolaczenia danePolaczenia = DanePolaczenia.Jawne(server_name, databaseName, windows_auth_enabled, windows_login, windows_pass);
            MenedzerPolaczen mp = new MenedzerPolaczen();

            sfera = mp.Polacz(danePolaczenia, ProductId.Subiekt);

            var status = sfera.ZalogujOperatora(login, password);

            Console.WriteLine("Subiekt.Login Done.");

            return status;

        }

        public static List<NexoUser> GetNexoUsers()
        {

            DanePolaczenia danePolaczenia = DanePolaczenia.Jawne(
                server_name,
                databaseName,
                windows_auth_enabled,
                windows_login,
                windows_pass
                );
            MenedzerPolaczen mp = new MenedzerPolaczen();

            Uchwyt sfera = mp.Polacz(danePolaczenia, ProductId.Subiekt);

            var uzytkownicy = new List<NexoUser>();
            using (var connection = sfera.PodajPolaczenie())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id, Nazwa, Login FROM ModelDanychContainer.Uzytkownicy WHERE Ukryty=0 AND NOT Osoba_Id  IS NULL;";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            /*var u = new NexoUser
                            {
                                Id = reader.GetGuid(0),
                                Nazwa = reader.GetString(1),
                                Login = reader.GetString(2)
                            };
                            uzytkownicy.Add(u);*/

                            uzytkownicy.Add(
                                new NexoUser(
                                    reader.GetString(1),
                                    reader.GetString(2)
                                )
                            );

                            Console.WriteLine(reader.GetString(1));
                        }
                    }
                }
            }

            return uzytkownicy;
        }

        public static void SetDatabaseLoginData(
            string _server_name,
            bool _windows_auth_enabled,
            string _windows_login,
            string _windows_pass
        )
        {
            server_name = _server_name;
            windows_auth_enabled = _windows_auth_enabled;
            windows_login = _windows_login;
            windows_pass = _windows_pass;
        }

        public static void SetDatabaseName(string _databaseName)
        {
            databaseName = _databaseName;
            Console.WriteLine("SET DATABASE NAME:{0}", databaseName);
        }

        public static List<FilteredAssortment> GetAssortment()
        {
            IAsortymenty menedzerAsortymentow = sfera.PodajObiektTypu<IAsortymenty>();

            var elements = menedzerAsortymentow.Dane.Wszystkie().ToList().Select(a => new FilteredAssortment(
                a,
                a.Dostawcy().FirstOrDefault(d => d.Podmiot.NIP == Hurton.NIP)?.Symbol ?? ""
            )).Where(e => e.hurton_symbol != "").ToList();

            foreach (var element in elements)
            {
                Console.WriteLine("{0} - {1}", element.element.Symbol, element.element.Nazwa);
            }

            return elements;
        }

        public static void UpdateOrInsertAssortmentsByCatalog(ProductsXML.Product[] products)
        {
            var elements = GetAssortment();

            Syncs.SetTextToTextarea(string.Format($"- Znaleziono {elements.Count} produktów w Nexo"), true);

            var areaText = Syncs.textareaValue;

            IAsortymenty menedzerAsortymentow = sfera.PodajObiektTypu<IAsortymenty>();

            int updated_count = 0,
                skipped_count = 0,
                inserted_count = 0,
                error_count = 0;

            int products_done = 0;

            int mb_sum_images = 0;
            int count_sum_images = 0;
            foreach (var product in products)
            {

                var found = elements.Find(item => item.hurton_symbol == product.Sku);
                if (found != null)
                {
                    Console.WriteLine("Znaleziono w subiekcie [{3}] '{0}': {1}    ----->   {2}", product.Sku, product.Name, found.element.Nazwa, found.element.Symbol);
                    var asortyment = menedzerAsortymentow.Znajdz(found.element);

                    try
                    {

                        var group_and_feature = product.CategorySecondary.Split(new char[] { '-' }, 2);
                        string group_name = product.CategorySecondary;
                        string feauture_name = "";
                        if (group_and_feature.Length > 1)
                        {
                            group_name = group_and_feature[0];
                            feauture_name = group_and_feature[1];
                        }

                        group_name = group_name.Substring(0, Math.Min(50, group_name.Length));
                        feauture_name = feauture_name.Substring(0, Math.Min(50, feauture_name.Length));



                        /* 
                         * Nazwa towaru 
                         */
                        if (asortyment.Dane.Nazwa != product.Name)
                        {
                            asortyment.Dane.Opis = string.Format("Poprzednia nazwa towaru: {0} \n{1}", asortyment.Dane.Nazwa, asortyment.Dane.Opis);
                        }
                        else
                        {
                            asortyment.Dane.Opis = "";
                        }
                        asortyment.Dane.Nazwa = product.Name;


                        /*
                         * Pełen opis z HTML
                         */
                        asortyment.Dane.PelnaCharakterystyka = product.Description;



                        /*
                         * Dodaj zdjecia do galerii i usun stare
                         */
                        var galeria = asortyment.PobierzGalerieZdjec();
                        var zdjecia = galeria.PobierzZdjecia();
                        while (zdjecia.Count() > 0)
                        {
                            IZdjecie zdjecie = zdjecia.First();
                            galeria.Usun(zdjecie);
                            Console.WriteLine("- Usuwam zdjecie");
                        }

                        foreach (var image_url in product.Images.Image)
                        {
                            WebClient client = new WebClient();
                            byte[] imageBytes = client.DownloadData(image_url);

                            MemoryStream ms = new MemoryStream(imageBytes);
                            Image image = Image.FromStream(ms);
                            string format = image.RawFormat.ToString();

                            string formatName = image.RawFormat.Guid == ImageFormat.Jpeg.Guid ? "jpeg" :
                            image.RawFormat.Guid == ImageFormat.Png.Guid ? "png" :
                            image.RawFormat.Guid == ImageFormat.Gif.Guid ? "gif" :
                            image.RawFormat.Guid == ImageFormat.Bmp.Guid ? "bmp" :
                            image.RawFormat.Guid == ImageFormat.Icon.Guid ? "ico" : "unknown";

                            Console.WriteLine("New image from url: {0} ({1}) [size:{2}]", image_url, formatName, imageBytes.Length);

                            mb_sum_images += imageBytes.Length / 1024;
                            count_sum_images++;
                            galeria.DodajZdjecie(image_url, formatName, imageBytes);
                        }


                        /**
                         * Model
                         */
                        /*var model = new Model();
                        model.Nazwa = product.Model;
                        asortyment.Dane.Model = model;*/



                        /**
                         *  Atrybuty przedmiotu w podstawowym opisie
                         */
                        if(product.Attributes.Attribute.Count() > 0)
                        {
                            asortyment.Dane.Opis += "\n\nAtrybuty przedmiotu:\n";
                            foreach (var attr in product.Attributes.Attribute)
                            {
                                asortyment.Dane.Opis += string.Format("- {0}: {1}\n", attr.Name, attr.Value);
                            }
                        }


                        asortyment.Dane.CenaEwidencyjna = product.Prices.NetPriceCustomer;
                        Console.WriteLine("Cena ewindencyjna: {0}", asortyment.Dane.CenaEwidencyjna);

                        //netPriceCatalog - netto internetowa
                        /*var internetowa = asortyment.Dane.PozycjeCennika.FirstOrDefault(o => o.Cennik.Tytul == "Internetowa");
                        if(internetowa != null)
                        {
                            InsERT.Moria.CennikiICeny.IPozycjeCennika mgr = sfera.PodajObiektTypu<InsERT.Moria.CennikiICeny.IPozycjeCennika>();
                            var c = mgr.Znajdz(internetowa);
                            c.Dane.CenaNetto = product.Prices.NetPriceCatalog;
                            Console.WriteLine("Cena Internetowa Netto -> {0}", c.Dane.CenaNetto);
                            c.Zapisz();

                        }*/
                        

                        var dostawca = asortyment.Dane.Dostawcy().FirstOrDefault(d => d.Podmiot.NIP == Hurton.NIP);
                        
                        dostawca.CenaDeklarowana = product.Prices.NetPriceCatalog;

                        dostawca.RabatDeklarowany = (product.Prices.NetPriceCatalog - product.Prices.NetPriceCustomer) / product.Prices.NetPriceCatalog;
                        // netPriceCatalog / RABAT = netPriceCustomer -> cena netto po rabacie




                        /* Przydziel grupę */
                        if (group_name.Length > 1)
                        {
                            InsERT.Moria.Asortymenty.IGrupyAsortymentu igrupy = sfera.PodajObiektTypu<InsERT.Moria.Asortymenty.IGrupyAsortymentu>();
                            InsERT.Moria.ModelDanych.GrupaAsortymentu grupa = igrupy.Dane.Wszystkie().FirstOrDefault(a => a.Nazwa == group_name);
                            bool group_not_exist = grupa == null;
                            bool group_saved = false;
                            if ( group_not_exist )
                            {
                                var igrupa = igrupy.Utworz();
                                igrupa.Dane.Nazwa = group_name;
                                group_saved = igrupa.Zapisz();
                                Console.WriteLine("Tworzenie nowej grupy '{0}' -> {1}", group_name, group_saved);

                                grupa = igrupa.Dane;
                            }
                            if(group_saved || !group_not_exist)
                            {
                                asortyment.Dane.Grupa = grupa;
                            }
                        }


                        /* Przydziel ceche */
                        if (feauture_name.Length > 0)
                        {


                            InsERT.Moria.Asortymenty.ICechyAsortymentu icechy = sfera.PodajObiektTypu<InsERT.Moria.Asortymenty.ICechyAsortymentu>();
                            InsERT.Moria.ModelDanych.CechaAsortymentu cecha = icechy.Dane.Wszystkie().FirstOrDefault(a => a.Nazwa == feauture_name);
                            bool feauture_not_exist = cecha == null;
                            bool feauture_saved = false;
                            if (feauture_not_exist)
                            {
                                var icecha = icechy.Utworz();
                                icecha.Dane.Nazwa = feauture_name;
                                feauture_saved = icecha.Zapisz();
                                Console.WriteLine("Tworzenie nowej cechy '{0}' -> {1}", feauture_name, feauture_saved);

                                cecha = icecha.Dane;
                            }
                            if (
                                (feauture_not_exist && feauture_saved) ||
                                (!feauture_not_exist && asortyment.Dane.Cechy.FirstOrDefault(a => a.Nazwa == feauture_name) == null)
                            )
                            {
                                asortyment.Dane.Cechy.Add(cecha);
                            }
                            Console.WriteLine($"Rezultat cechy: ({feauture_not_exist}), ({feauture_saved}), ({feauture_name})");
                        }
                        else
                        {
                            Console.WriteLine("! Brak nazwy która mogłaby być cechą.");
                        }


                        /*
                         * Zapisz zmiany
                         */
                        var saveSuccess = asortyment.Zapisz();
                        if(saveSuccess)
                        {
                            updated_count++;
                        }
                        else
                        {
                            error_count++;
                        }
                        Console.WriteLine("Status zapisu zmian: {0}", saveSuccess);

                    } 
                    catch (InsERT.Mox.Locking.AppLockNotAcquiredException e)
                    {
                        Console.WriteLine("Nie udało się edytować elementu '{0}' {1} - jest zablokowany!", asortyment.Dane.Nazwa, asortyment.Dane.Symbol);
                        error_count++;
                    }

                    

                }
                else
                {
                    //Console.WriteLine("NIE Znaleziono w subiekcie '{0}': {1}", product.Sku, product.Name);
                }

                products_done++;

                Syncs.progressBar = 100 * products_done / products.Length;

                Syncs.SetTextToTextarea(string.Format($"{areaText}\nPostęp: {products_done}/{products.Length}\n-> Zaktualizowano: {updated_count}\n-> Dodano: {inserted_count}\n-> Pominięto: {skipped_count}\n-> Błędy: {error_count}"));
            }

            Console.WriteLine("Waga wszystykich zdjec ({0}) lacznie: {1}", count_sum_images, mb_sum_images);
        }
    }
}
