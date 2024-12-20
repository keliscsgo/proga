﻿using sport_invertar_shop.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sport_invertar_shop
{
    public partial class Auth : Form
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = DataBase.fromDB(WAF.login(textBox1.Text, textBox2.Text));
            if (dataTable.Rows.Count != 0)
            {
                label4.Visible = false;
                Home open = new Home();
                Home.user_id = (int)dataTable.Rows[0]["id"];
                open.Show();
                this.Hide();

            }
            else
            {
                label4.Visible = true;
                textBox2.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SignUp open = new SignUp();
            open.Show();
            this.Hide();
        }
    }
}
