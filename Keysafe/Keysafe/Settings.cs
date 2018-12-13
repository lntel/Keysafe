using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Keysafe
{
    public partial class Settings : Form
    {
        private Configuration config = new Configuration();
        private Alert alt = new Alert();
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            config.RunQuery("select * from settings");

            while(config.value.Read())
            {
                string autoupdate = config.value["autoUpdate"].ToString();
                string autobackup = config.value["autoBackup"].ToString();

                bunifuSwitch1.Value = Convert.ToBoolean(autoupdate);
                bunifuSwitch2.Value = Convert.ToBoolean(autobackup);
            }
        }

        private void bunifuSwitch1_Click(object sender, EventArgs e)
        {
            config.RunQuery(string.Format("update settings set autoUpdate = {0}", bunifuSwitch1.Value));
        }

        private void bunifuSwitch2_Click(object sender, EventArgs e)
        {
            config.RunQuery(string.Format("update settings set autoBackup = {0}", bunifuSwitch2.Value));
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void bunifuCustomLabel4_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog sv = new SaveFileDialog();

            sv.FileName = Guid.NewGuid().ToString();
            sv.DefaultExt = "sqlite";
            sv.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            sv.Title = "Choose a location to export your person data to";

            if(sv.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Copy(config.full_path, sv.FileName);

                    alt.Display("File Exported", "Successfully exported Sqlite database, these hashes will still be encrypted.");
                    alt.ShowDialog();
                    
                }
                catch(Exception ex)
                {
                    // Handle
                }
            }
        }
    }

    public class Backup
    {
        private string dir_path;
        public Backup()
        {
            dir_path = string.Format(@"{0}\Keysafe\", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }

        public DateTime BackupDate()
        {
            FileInfo fi = new FileInfo(dir_path + "local.sqlite");

            return fi.CreationTime;
        }
    }
}
