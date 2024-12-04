using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sport_invertar_shop.UserControls
{
    public partial class Profile : UserControl
    {
        public static int user { get; set; }
        public Profile()
        {
            InitializeComponent();
        }

        private void Profile_Load(object sender, EventArgs e)
        {
            userdata();
        }

        void userdata()
        {
            DataTable info = DataBase.fromDB(WAF.get_user(user.ToString()));
            label4.Text = info.Rows[0]["login"].ToString();
            switch ((int)info.Rows[0]["role"])
            {
                case 1:
                    label7.Text = "Пользователь"; break;
                case 2:
                    label7.Text = "Поставщик"; break;
                case 3:
                    label7.Text = "Администратор"; break;
                default:
                    label7.Text = "Не определено"; break;
            }
            textBox1.Text = info.Rows[0]["surname"].ToString();
            textBox2.Text = info.Rows[0]["password"].ToString();
            textBox3.Text = info.Rows[0]["password"].ToString();
            textBox4.Text = info.Rows[0]["name"].ToString();
            textBox5.Text = info.Rows[0]["lastname"].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            userdata();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataBase.toDB(WAF.update_account(user.ToString(), textBox3.Text, textBox1.Text, textBox4.Text, textBox5.Text));
        }
    }
}
