using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Keysafe
{
    class Utilities
    {
        public Thread backupThread;

        private Directories _route;
        private Int32 unixTimestamp;

        private struct Directories
        {
            public string backup;
            public string main;
        }

        public Utilities()
        {

            backupThread = new Thread(BackupLoop);

            StringBuilder builder = new StringBuilder();

            builder.Append(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));

            builder.Append(@"\Keysafe");

            _route.main = builder.ToString();

            builder.Append(@"\backups");

            _route.backup = builder.ToString();
        }

        private void BackupLoop()
        {
            string[] backups;

            if(!Directory.Exists(_route.backup))
            {
                Directory.CreateDirectory(_route.backup);
            }

            while(true)
            {
                unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

                backups = Directory.GetFiles(_route.backup);

                if(backups.Length < 4)
                {
                    File.Copy(string.Format(@"{0}\local.sqlite", _route.main), string.Format(@"{0}\{1}", _route.backup, unixTimestamp));
                }

                Thread.Sleep(900000); // 15 min interval
            }
        }
    }
}
