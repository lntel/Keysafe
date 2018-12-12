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
    public partial class Form1 : Form
    {
        public Alert alt;

        private Thread SettingsEvents;
        private string secret;

        private Configuration config;
        public Form1()
        {
            InitializeComponent();

            alt = new Alert();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            config = new Configuration();

            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;

            if (config.first_time)
            {
                Create create = new Create();

                create.ShowDialog();

                if(create.DialogResult == DialogResult.OK)
                {
                    secret = create.secret;
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
                    secret = auth.secret;

                    alt.Display("Authorised", "You have been successfully authorised.");
                    alt.ShowDialog();

                    this.WindowState = FormWindowState.Normal;
                    this.ShowInTaskbar = true;

                    new Thread(() =>
                    {
                        ShowSites();
                    }).Start();

                    SettingsEvents = new Thread(LoadSettings);
                }

                alt.Dispose();

                auth.Dispose();
            }
        }

        private void ShowSites()
        {
            config = new Configuration();

            config.RunQuery("select * from accounts");

            this.Invoke((MethodInvoker)delegate
            {
                dataGridView1.Rows.Clear();
            });

                while (config.value.Read())
            {
                this.Invoke((MethodInvoker)delegate
                {
                    dataGridView1.Rows.Add(config.value["url"], config.value["email"], config.value["hash"]);
                });
            }

            //SettingsEvents.Start();
        }

        private void LoadSettings()
        {
            //int index = 0;

            config = new Configuration();

            while(true)
            {
                config.RunQuery("select * from settings");

                while (config.value.Read())
                {
                    if(config.value["autoBackup"].ToString() == "True")
                    {
                        //Backup backup = new Backup();

                        //MessageBox.Show(backup.BackupDate().ToString());
                    }
                }

                Thread.Sleep(2000); // 10 mins
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            if(e.ColumnIndex == 2)
            {
                value = StringCipher.Decrypt(value, secret);

                bunifuCustomLabel2.Text = "Copied password to clipboard";
            } else
            {
                bunifuCustomLabel2.Text = string.Format("Copied {0} to clipboard!", value);
            }

            Clipboard.SetText(value);
        }

        private void bunifuImageButton2_Click(object sender, EventArgs e)
        {
            Add add = new Add(secret);

            add.ShowDialog();

            if (add.DialogResult == DialogResult.OK)
            {
                ShowSites();
            }

            add.Dispose();
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            Settings settings = null;

            if(settings == null)
            {
                settings = new Settings();
                settings.Show();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
