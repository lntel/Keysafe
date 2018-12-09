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
        private Configuration config;
        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            config = new Configuration();

            config.RunQuery("select * from settings");

            while(config.value.Read())
            {
                bunifuSwitch1.Value = Convert.ToBoolean(0);

                MessageBox.Show(config.value["autoUpdate"].ToString());
            }
        }
    }
}
