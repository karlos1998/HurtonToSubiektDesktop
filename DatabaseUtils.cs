using System;
using System.Collections.Generic;
using System.Data.Sql;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace HurtonToSubiektDesktop
{
    internal class DatabaseUtils
    {
        public static List<string> GetServers()
        {
            var servers = SqlDataSourceEnumerator.Instance.GetDataSources();

            /*var list = new List<string>();

            foreach (DataRow row in servers.Rows)
            {
                list.Add(row["ServerName"].ToString());
            }*/
            return servers.AsEnumerable()
            .Select(row => row.Field<string>("ServerName"))
            .ToList();

            //return list;
        }

        public static List<string> GetServerDatabases(
            string server_name, 
            bool windows_auth_enabled, 
            string windows_login = "sa", 
            string windows_pass = ""
        )
        {
            var list = new List<string>();

            Console.WriteLine("Target server name: {0}", server_name);

            string string_data = windows_auth_enabled ?
                string.Format("Server={0};Integrated Security=True", server_name) :
                string.Format("Server={0};User ID={1};Password={2}", server_name, windows_login, windows_pass);

            using (var connection = new SqlConnection(string_data))
            {
                // Otwiera połączenie
                connection.Open();

                // Pobiera listę baz danych
                var command = new SqlCommand("SELECT name FROM sys.databases", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var text = reader.GetString(0);
                        if (text.StartsWith("Nexo_"))
                        {
                            //list.Add(text.Substring(5));
                            list.Add(text);
                        }
                        else
                        {
                            Console.WriteLine("Pominieto DB: {0}", text);
                        }
                    }
                }
            }

            return list;
        }
    }
}
