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

        public void Display(string title, string message, bool canClose = true)
        {
            bunifuCustomLabel1.Text = title;
            bunifuCustomLabel2.Text = message;

            closable = canClose;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            if(closable)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
