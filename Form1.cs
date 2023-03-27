using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using Newtonsoft.Json;
using System.IO;

using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Threading;

namespace HurtonToSubiektDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            nexoUserPasswordInput.UseSystemPasswordChar = true;

            catalogCategoriesTreeView.CheckBoxes = true;


            /** TODO ! */
            var test = Properties.Settings.Default;
            Console.WriteLine(test);

            //tabControl1.TabPages[0].Enabled = false;


        }

        private void select_server_combo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void get_servers_Click(object sender, EventArgs e)
        {
            /**
             * Poszukaj serwerów
             */

            string suffix = " (Czekaj)";

            get_servers.Enabled = false;

            get_servers.Text += suffix;

            infoBox.Text = "Wyszukiwanie nazw serwerów...";

            //select_server_combo.Items.AddRange(DatabaseUtils.GetServers().ToArray());
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (s, args) =>
            {
                // Pobierz dane z bazy danych
                List<string> servers = DatabaseUtils.GetServers();

                // Przekaż dane jako argument do zdarzenia Completed
                args.Result = servers;
            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                var list = ((List<string>)args.Result).ToArray();

                // Uzupełnij kontrolkę ComboBox danymi z bazy danych
                select_server_combo.Items.Clear();
                select_server_combo.Items.AddRange(list);

                // Ustaw wartość domyślną na pierwszą opcję
                if (list.Length > 0)
                {
                    select_server_combo.SelectedIndex = 0;
                }

                infoBox.Text = string.Format("Znalezione serwery: {0}", list.Length);

                get_servers.Enabled = true;
                get_servers.Text = get_servers.Text.Replace(suffix, "");
            };

            worker.RunWorkerAsync();
        }

        private void connect_server_button_Click(object sender, EventArgs e)
        {

            /**
             * Kliknij przycisk POŁĄCZ w celu pobrania listy baz danych
             * 
             */

            string suffix = " (Czekaj)";

            connect_server_button.Text += suffix;
            connect_server_button.Enabled = false;

            var baseName = select_server_combo.Text;

            infoBox.Text = string.Format("Łączenie z serwerem: {0}", baseName);

            string[] databases = new string[] { };

            try
            {
                databases = DatabaseUtils.GetServerDatabases(
                    baseName, 
                    windowsAuthBox.Checked,
                    windowsAuthLogin.Text,
                    windowsAuthPass.Text
                    ).ToArray();

                select_db_box.Items.Clear();
                select_db_box.Items.AddRange(databases);
                if (databases.Length > 0)
                {
                    select_db_box.SelectedIndex = 0;
                }

                infoBox.Text = string.Format("Połączono z serwerem: {0}", baseName);

                Subiekt.SetDatabaseLoginData(baseName,
                    windowsAuthBox.Checked,
                    windowsAuthLogin.Text,
                    windowsAuthPass.Text);
                
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                infoBox.Text = string.Format("Nie udało się zalogować do bazy danych '{0}' przy użyciu tych poświadczeń", baseName);
            }
            catch (Exception ex)
            {
                infoBox.Text = string.Format("Wystąpił nieznany błąd podczas łączenia z bazą danych '{0}'", baseName);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                connect_server_button.Text = connect_server_button.Text.Replace(suffix, "");
                connect_server_button.Enabled = true;
            }

        }

        private void select_db_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            Subiekt.SetDatabaseName(select_db_box.Text);
        }

        private void windowsAuthBox_CheckedChanged(object sender, EventArgs e)
        {
            bool windowsAuthEnabled = windowsAuthBox.Checked;

            windowsAuthLogin.ReadOnly = windowsAuthEnabled;
            windowsAuthPass.ReadOnly = windowsAuthEnabled;

            if (windowsAuthEnabled)
            {
                windowsAuthLogin.Text = "";
                windowsAuthPass.Text = "";
            }
        }

        private void nexoConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                nexoUsersComboBox.DataSource = Subiekt.GetNexoUsers();
                nexoUsersComboBox.DisplayMember = "Text"; 
            }
            catch(System.InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Nie udało się połączyć", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine(ex.Message);
            }
        }

        async private void getHurtonJsonButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Pobieranie danych json...");

            // https://hurton.pl/customer_api/documentation



            return;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://xml.hurton.pl/");

                using (var response = await client.GetAsync("customer_api/xml/export_json_short?login=asdasdasads&authKey=5d37555340569a03fb5c81933b2ee5f50e3e9e04"))
                {

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        //Console.WriteLine(json);
                        ProductsJSON.ProductData data = JsonConvert.DeserializeObject<ProductsJSON.ProductData>(json);
                        //Console.WriteLine(data);

                        hurtonJsonItemsCountLabel.Text = "Pobranych przedmiotów z json: " + data.DataList.Count;

                        /*foreach (ProductsJSON.Product item in data.DataList)
                        {
                            Console.WriteLine(item.Model);
                        }*/
                    }
                    else
                    {
                        Console.WriteLine("Failed to retrieve data.");
                    }
                }

            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            var target = (NexoUser)nexoUsersComboBox.SelectedItem;

            infoBox.Text = string.Format("Logowanie do Subiekta jako {0} ...", target.Text);
            var status = Subiekt.Login(target.Value, nexoUserPasswordInput.Text);
            if(status)
            {
                infoBox.Text = "Zalogowano do programu Subiekt Nexo.";
            }
            else
            {
                infoBox.Text = string.Format("Logowanie do Subiekta jako {0} -> NIE POWIODŁO SIĘ", target.Text);
            }

        }

        private void fullSyncXmlButton_Click(object sender, EventArgs e)
        {

            string suffix = " (Czekaj)";

            fullSyncXmlButton.Enabled = false;
            checkBoxOnlyUpdateExistProducts.Enabled = false;

            fullSyncXmlButton.Text += suffix;

            infoBox.Text = "Trwa wykonywanie pełnej synchronizacji elementów";


            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (s, args) =>
            {
                Console.WriteLine("Worker Syncs.FullUpdate Start");

                var textareaLastTime = DateTime.Now;

                Syncs.FullUpdate();

                do
                {

                    if (textareaLastTime.CompareTo(Syncs.updateTextareaDate) < 0)
                    {
                        UpdateTextarea(Syncs.textareaValue);
                    }

                    Thread.Sleep(1000);
                } while (Syncs.syncInProgres);


                Console.WriteLine("Worker Syncs.FullUpdate End");

            };

            worker.RunWorkerCompleted += (s, args) =>
            {
                fullSyncXmlButton.Enabled = true;
                fullSyncXmlButton.Text = fullSyncXmlButton.Text.Replace(suffix, "");

                checkBoxOnlyUpdateExistProducts.Enabled = true;
            };

            worker.RunWorkerAsync();

        }


        private void UpdateTextarea(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<string>(UpdateTextarea), text);
                return;
            }

            richTextBox1.Text = text;

            progressBar1.Value = Syncs.progressBar;
        }
        private async void getCategoriesButton_Click(object sender, EventArgs e)
        {
            var categories = await Syncs.CategoryList();

            categories.Sort();

            var nodes = catalogCategoriesTreeView.Nodes;

            foreach (var full_category in categories)
            {
                var sub_categories = full_category.Split('-');

                TreeNode node = new TreeNode();
                for (int index = 0; index < sub_categories.Length; index++)
                {
                    var sub = sub_categories[index];

                    if (node.Text.Length == 0)
                    {
                        for (int index2 = 0; index2 < nodes.Count; index2++)
                        {
                            if (nodes[index2].Text == sub)
                            {
                                node = nodes[index2];

                                break;
                            }
                        }

                        // if still not found
                        if (node.Text.Length == 0)
                        {
                            node.Text = sub;
                            node.Checked = true;

                            nodes.Add(node);

                        }

                        continue;
                    }

                    for (int index2 = 0; index2 < node.Nodes.Count; index2++)
                    {
                        if (node.Nodes[index2].Text == sub)
                        {
                            node = node.Nodes[index2];
                            break;
                        }
                    }

                    if (node.Text != sub)
                    {
                        TreeNode subNode = new TreeNode();
                        subNode.Text = sub;
                        subNode.Checked = true;

                        node.Nodes.Add(subNode);
                        node = subNode;
                    }
                }
            }
        }

        private void catalogCategoriesTreeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            // The code only executes if the user caused the checked state to change.
            if (e.Action != TreeViewAction.Unknown)
            {
                if (e.Node.Nodes.Count > 0)
                {

                    Console.WriteLine("{0} -> {1}", e.Node.Text, e.Node.Checked);

                    /* Calls the CheckAllChildNodes method, passing in the current 
                    Checked value of the TreeNode whose checked state changed. */
                    this.CheckAllChildNodes(e.Node, e.Node.Checked);
                }
            }
        }
        private void CheckAllChildNodes(TreeNode treeNode, bool nodeChecked)
        {
            foreach (TreeNode node in treeNode.Nodes)
            {
                node.Checked = nodeChecked;
                if (node.Nodes.Count > 0)
                {
                    // If the current node has child nodes, call the CheckAllChildsNodes method recursively.
                    this.CheckAllChildNodes(node, nodeChecked);
                }
            }
        }


        private void catalogCategoriesTreeView_AfterClick(object sender, EventArgs e)
        {
            Console.WriteLine("click");
        }

        private void checkBoxOnlyUpdateExistProducts_CheckedChanged(object sender, EventArgs e)
        {
            Syncs.OnlyUpdateExists = checkBoxOnlyUpdateExistProducts.Checked;
        }
    }
}
