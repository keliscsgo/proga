using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sport_invertar_shop.Forms
{
    public partial class ProductPage : Form
    {
        public static int product_code { get; set; }
        public static int user { get; set; }
        public ProductPage()
        {
            InitializeComponent();
        }

        private void ProductPage_Load(object sender, EventArgs e)
        {
            label1.Text = product_code.ToString();
            DataTable table = DataBase.fromDB(WAF.product(product_code.ToString()));
            DataTable table2 = DataBase.fromDB($"select [id], [name], [description] from [categories] where [id] = '{WAF.text(table.Rows[0]["id"].ToString())}'");
            label5.Text = table2.Rows[0]["name"].ToString();
            label7.Text = table2.Rows[0]["description"].ToString();
            label4.Text = table.Rows[0]["description"].ToString();
            label2.Text = table.Rows[0]["name"].ToString();
            label3.Text = table.Rows[0]["coste"].ToString();
            try
            {
                pictureBox1.Image = Image.FromFile($@".\\.\\{table.Rows[0]["img"]}");
            }
            catch
            {

            }
           
            if ((bool)table.Rows[0]["availability"])
            {
                button2.Enabled = true;
                label6.Text = "Присутсвует";
            }
            else
            {
                button2.Enabled = false;
                label6.Text = "Отсутствует";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataBase.toDB($"exec [pay] '{Home.user_id}', '{label1.Text}', '0','0'");
            MessageBox.Show("Дождитесь списания. Если списалось с баланса - оплата прошла"); 
        }
    }
}
