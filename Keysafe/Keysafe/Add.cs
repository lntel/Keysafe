using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.Security;

namespace Keysafe
{
    public partial class Add : Form
    {
        private Configuration config;
        private Alert alt;
        private string phrase;
        public Add(string secret)
        {
            InitializeComponent();

            config = new Configuration();
            alt = new Alert();
            phrase = secret;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            string url, email, password;

            url = bunifuMetroTextbox1.Text;
            email = bunifuMetroTextbox2.Text;
            password = bunifuMetroTextbox3.Text;

            if(url != string.Empty && email != string.Empty && password != string.Empty)
            {
                password = StringCipher.Encrypt(password, phrase);

                config.RunQuery(string.Format("insert into accounts (url, email, hash) values ('{0}', '{1}', '{2}')", url, email, password));

                alt.Display("Account added", "Your new account was successfully added.");

                alt.ShowDialog();

                this.DialogResult = DialogResult.OK;
            }
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            bunifuMetroTextbox3.Text = Membership.GeneratePassword(32, 12);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
