using sport_invertar_shop.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace sport_invertar_shop.UserControll
{
    public partial class Products : UserControl
    {
        public int product_id { get; set; }
        public Products()
        {
            InitializeComponent();
        }

        private void Product_Load(object sender, EventArgs e)
        {
            label4.Text = product_id.ToString();
            DataTable table = DataBase.fromDB(WAF.product(label4.Text));
            label1.Text = table.Rows[0]["name"].ToString();
            label2.Text = table.Rows[0]["description"].ToString();
            label3.Text = Convert.ToInt32(table.Rows[0]["coste"]).ToString();
            try
            {
                pictureBox1.Image = Image.FromFile($@".\\.\\{table.Rows[0]["img"]}");
            }
            catch
            {
                pictureBox1.Image = Image.FromFile($@".\\.\\img\none.png");
            }
           

            if ((bool)table.Rows[0]["availability"])
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ProductPage open = new ProductPage();
            ProductPage.product_code = Convert.ToInt32(label4.Text);
            open.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataBase.toDB($"insert into [baskets]([user],[product]) values('{Home.user_id}', '{label4.Text}')");
            MessageBox.Show("Забронированно!");
        }
    }
}
