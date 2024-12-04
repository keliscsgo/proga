using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sport_invertar_shop.Forms
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dataTable = DataBase.fromDB(WAF.check_login(textBox1.Text));
            if (dataTable.Rows.Count == 0)
            {
                label4.Visible = false;
                if (WAF.text(textBox2.Text) == WAF.text(textBox3.Text))
                {
                    DataBase.toDB(WAF.signup(textBox1.Text, textBox2.Text));
                    MessageBox.Show("Вы можете войти");
                    Application.Restart();
                }
                else
                {
                    label6.Visible = true;
                }
            }
            else
            {
                label4.Visible = true;
            }


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Application.Restart();
        }
    }
}
