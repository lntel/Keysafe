using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keysafe
{
    public partial class Settings : Form
    {
        private Configuration config = new Configuration();
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
    }
}
