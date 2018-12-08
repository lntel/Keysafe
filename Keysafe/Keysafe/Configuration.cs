using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Data.SQLite;

namespace Keysafe
{
    class Configuration
    {
        public string dir_path;
        public string full_path;
        public bool first_time = false;

        public SQLiteDataReader value;

        private SQLiteConnection _db;
        public Configuration()
        {
            dir_path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Keysafe";
            full_path = dir_path + @"\local.sqlite";

            _db = new SQLiteConnection(string.Format("Data Source={0};Version=3;", full_path));

            bool dir_exists = Directory.Exists(dir_path);

            if (!dir_exists) Directory.CreateDirectory(dir_path);

            if (!File.Exists(full_path) || !dir_exists)
            {
                first_time = true;

                SQLiteConnection.CreateFile(full_path);

                _db.Open();

                string sql = "create table accounts (url varchar(50), email varchar(40), username varchar(20), hash varchar (100))";

                SQLiteCommand command = new SQLiteCommand(sql, _db);

                command.ExecuteNonQuery();

                sql = "create table settings (master_key)";

                command = new SQLiteCommand(sql, _db);

                command.ExecuteNonQuery();
            }
        }

        public void RunQuery(string query)
        {
            _db.Open();

            SQLiteCommand command = new SQLiteCommand(query, _db);

            value = command.ExecuteReader();
        }
    }
}
