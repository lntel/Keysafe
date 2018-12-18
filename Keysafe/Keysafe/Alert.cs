using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Keysafe
{
    public partial class Alert : Form
    {
        private bool closable;
        public Alert()
        {
            InitializeComponent();
        }

        public void Display(string title, string message, bool hasOptions = false)
        {
            bunifuCustomLabel1.Text = title;
            bunifuCustomLabel2.Text = message;

            if(!hasOptions)
            {
                bunifuFlatButton2.Hide();

                bunifuFlatButton1.Size = new Size(304, 48);
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }
    }
}
