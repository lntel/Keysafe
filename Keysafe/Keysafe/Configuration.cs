﻿using System;
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

        public SQLiteConnection _db;

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

                SqliteExtensions.ExecuteCommand(_db, sql);

                sql = "create table settings (master_key varchar(50), autoUpdate bit, autoBackup bit)";

                SqliteExtensions.ExecuteCommand(_db, sql);

                sql = string.Format("insert into settings (autoUpdate, autoBackup) VALUES ('{0}', '{1}')", true, true);

                SqliteExtensions.ExecuteCommand(_db, sql);
            } else
            {
                switch (_db.State)
                {
                    case System.Data.ConnectionState.Broken:
                        //_db.Open();
                        break;
                    case System.Data.ConnectionState.Closed:
                        _db.Open();
                        break;
                }
            }
        }

        public void Dispose()
        {
            _db.Close();

            GC.Collect();
            GC.WaitForPendingFinalizers();

            File.Delete(full_path);
        }
    }

    static class SqliteExtensions
    {
        public static SQLiteDataReader ExecuteReader(SQLiteConnection connection, string query)
        {
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                return command.ExecuteReader();
            }
        }

        public static void ExecuteCommand(SQLiteConnection _db, string query)
        {
            using (SQLiteCommand command = _db.CreateCommand())
            {
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
        }
    }
}
