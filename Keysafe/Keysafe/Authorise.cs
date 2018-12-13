﻿using System;
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
    public partial class Authorise : Form
    {
        private string hash;

        public string secret;
        public Authorise()
        {
            InitializeComponent();
        }

        private void bunifuMetroTextbox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void bunifuMetroTextbox1_OnValueChanged(object sender, EventArgs e)
        {
            new Thread(() =>
            {
                string password = bunifuMetroTextbox1.Text;

                if (StringCipher.Decrypt(hash, password) == password)
                {
                    secret = password;

                    bunifuMetroTextbox1.BorderColorIdle = Color.FromArgb(39, 232, 167);
                    bunifuMetroTextbox1.BorderColorFocused = Color.FromArgb(39, 232, 167);
                    bunifuMetroTextbox1.ForeColor = Color.FromArgb(39, 232, 167);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    bunifuMetroTextbox1.BorderColorIdle = Color.FromArgb(223, 69, 119);
                    bunifuMetroTextbox1.BorderColorFocused = Color.FromArgb(223, 69, 119);
                    bunifuMetroTextbox1.ForeColor = Color.FromArgb(223, 69, 119);
                }
            }).Start();
        }

        private void Authorise_Load(object sender, EventArgs e)
        {
            Configuration config = new Configuration();

            config.RunQuery("select master_key from settings");

            while(config.value.Read())
            {
                hash = config.value["master_key"].ToString();
            }
        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }
    }
}
