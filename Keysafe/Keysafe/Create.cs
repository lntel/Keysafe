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
    public partial class Create : Form
    {
        private Alert alt;
        public Create()
        {
            InitializeComponent();

            alt = new Alert();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string password = bunifuMetroTextbox1.Text;

            if(password != string.Empty)
            {
                Configuration config = new Configuration();

                password = StringCipher.Encrypt(password, password);

                config.RunQuery(string.Format("insert into settings (master_key) VALUES ('{0}')", password));

                alt.Display("Password set", "Congratulations, your password is now set and Keysafe is ready to use.");
                alt.ShowDialog();

                if (alt.DialogResult == DialogResult.OK) this.DialogResult = DialogResult.OK;

                alt.Dispose();
            }
        }
    }
}
