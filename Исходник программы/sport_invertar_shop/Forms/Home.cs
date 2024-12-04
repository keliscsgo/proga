using sport_invertar_shop.UserControll;
using sport_invertar_shop.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sport_invertar_shop.Forms
{
    public partial class Home : Form
    {
        public static int user_id { get; set; }
        public Home()
        {
           
            InitializeComponent();
            
            DataTable table = DataBase.fromDB(WAF.allcategories());
            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox1.Items.Add(table.Rows[i]["name"]);
                comboBox1.SelectedIndex = 0;
            }
        }

        void loadmain()
        {
            DataTable table = DataBase.fromDB(WAF.allproduct());
            if (table != null)
            {
                flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Products item = new Products();
                    item.product_id = (int)table.Rows[i]["id"];
                    flowLayoutPanel1.Controls.Add(item);
                }
            }
        }

        private void Home_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable user = DataBase.fromDB(WAF.get_user(user_id.ToString()));
                label1.Text = user.Rows[0]["login"].ToString();
                switch ((int)user.Rows[0]["role"])
                {
                    case 1:
                        label2.Text = "Заказчик"; break;
                    case 2:
                        label2.Text = "Бригадир"; linkLabel3.Visible = true; break;
                    case 3:
                        label2.Text = "Администратор"; linkLabel4.Visible = true; break;
                    default:
                        label2.Text = "Не определено"; break;
                }
                label3.Text = $"Баланс: {Convert.ToInt32(user.Rows[0]["money"])}";
            }
            catch
            {

            }
            loadmain();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult asd = MessageBox.Show("Хотите выйти?", "Выйти?", MessageBoxButtons.OKCancel);
            if(asd == DialogResult.OK)
            {
                Application.Restart();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DataTable table = DataBase.fromDB(WAF.get_search(textBox1.Text, comboBox1.SelectedItem.ToString()));
            if (table != null)
            {
                flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Products item = new Products();
                    item.product_id = (int)table.Rows[i]["id"];
                    flowLayoutPanel1.Controls.Add(item);
                }
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PostavPanel open = new PostavPanel();
            PostavPanel.user = user_id;
            open.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AdminPanel open = new AdminPanel();
            open.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            Profile item = new Profile();
            Profile.user = user_id;
            flowLayoutPanel1.Controls.Add(item);
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            loadmain();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DataTable table = DataBase.fromDB(WAF.get_my_basket(user_id));
            if (table != null)
            {
                flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    Baskets item = new Baskets();
                    Baskets.code = (int)table.Rows[i]["id"];
                    flowLayoutPanel1.Controls.Add(item);
                }
            }
        }

        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                DataTable user = DataBase.fromDB(WAF.get_user(user_id.ToString()));
                label1.Text = user.Rows[0]["login"].ToString();
                switch ((int)user.Rows[0]["role"])
                {
                    case 1:
                        label2.Text = "Заказчик"; break;
                    case 2:
                        label2.Text = "Бригадир"; linkLabel3.Visible = true; break;
                    case 3:
                        label2.Text = "Администратор"; linkLabel4.Visible = true; break;
                    default:
                        label2.Text = "Не определено"; break;
                }
                label3.Text = $"Баланс: {Convert.ToInt32(user.Rows[0]["money"])}";
            }
            catch
            {

            }
        }
    }
}
