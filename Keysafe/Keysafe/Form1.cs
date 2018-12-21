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
using System.Data.SQLite;

namespace Keysafe
{
    public partial class Form1 : Form
    {
        public Alert alt;
        public Settings settings;

        private Add add;
        private Thread SettingsEvents;
        private string secret;

        private int rowIndex;

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
                using (Create create = new Create())
                {
                    create.ShowDialog();

                    if (create.DialogResult == DialogResult.OK)
                    {
                        secret = create.secret;
                        this.WindowState = FormWindowState.Normal;
                        this.ShowInTaskbar = true;
                    }
                }
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

                    //SettingsEvents = new Thread(LoadSettings);
                }

                if(auth.DialogResult == DialogResult.No)
                {
                    Environment.Exit(-1);
                }

                alt.Dispose();

                auth.Dispose();
            }
        }

        private void ShowSites()
        {
            config = new Configuration();

            using (SQLiteDataReader reader = SqliteExtensions.ExecuteReader(config._db, "select * from accounts"))
            {
                this.Invoke((MethodInvoker)delegate
                {
                    dataGridView1.Rows.Clear();
                });

                while (reader.Read())
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        dataGridView1.Rows.Add(reader["url"], reader["email"], reader["hash"]);
                    });
                }
            }

            //SettingsEvents.Start();
        }

        private void LoadSettings()
        {
            //int index = 0;

            config = new Configuration();

            while(true)
            {
                using (SQLiteDataReader reader = SqliteExtensions.ExecuteReader(config._db, "selec * from settings"))
                {
                    while (reader.Read())
                    {
                        if (reader["autoBackup"].ToString() == "True")
                        {
                            //Backup backup = new Backup();

                            //MessageBox.Show(backup.BackupDate().ToString());
                        }
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
            if (add == null)
            {
                add = new Add(secret);
                add.ShowDialog();

                if(add.DialogResult == DialogResult.OK)
                {
                    ShowSites();
                }
            }

            add.Dispose();

            add = null;
        }

        private void bunifuImageButton3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void bunifuImageButton1_Click(object sender, EventArgs e)
        {
            if(settings == null)
            {
                settings = new Settings(this);
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*
            if(bunifuFlatButton1.Visible)
            {
                bunifuFlatButton1.Visible = false;
            } else
            {
                bunifuFlatButton1.Visible = true;
            }
            */
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!bunifuImageButton3.Visible) bunifuImageButton3.Visible = true;

            rowIndex = e.RowIndex;
        }

        private void bunifuImageButton3_Click_1(object sender, EventArgs e)
        {
            SqliteExtensions.ExecuteCommand(config._db, string.Format("delete from accounts where url = '{0}'", dataGridView1.Rows[rowIndex].Cells[0].Value.ToString()));

            bunifuCustomLabel2.Text = "Record for " + dataGridView1.Rows[rowIndex].Cells[0].Value.ToString() + " has been deleted";

            dataGridView1.Rows.RemoveAt(rowIndex);

            rowIndex = 0;

            GC.Collect();
        }
    }
}
