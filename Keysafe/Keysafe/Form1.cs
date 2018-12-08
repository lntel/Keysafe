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
    public partial class Form1 : Form
    {
        public Alert alt;
        public Form1()
        {
            InitializeComponent();

            alt = new Alert();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Configuration config = new Configuration();

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            if (config.first_time)
            {
                Create create = new Create();

                create.ShowDialog();

                if(create.DialogResult == DialogResult.OK)
                {
                    this.WindowState = FormWindowState.Normal;
                    this.ShowInTaskbar = true;
                }

                create.Dispose();
            } else
            {
                Authorise auth = new Authorise();

                auth.ShowDialog();

                if (auth.DialogResult == DialogResult.OK)
                {
                    alt.Display("Authorised", "You have been successfully authorised.");
                    alt.ShowDialog();

                    this.WindowState = FormWindowState.Normal;
                    this.ShowInTaskbar = true;
                }

                alt.Dispose();

                auth.Dispose();
            }
        }
    }
}
