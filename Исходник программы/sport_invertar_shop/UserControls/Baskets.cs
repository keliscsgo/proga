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

namespace sport_invertar_shop.UserControls
{
    public partial class Baskets : UserControl
    {
        public static int code { get; set; }
        public Baskets()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Baskets_Load(object sender, EventArgs e)
        {
            DataTable table = DataBase.fromDB(WAF.get_info_basket(code));
            label4.Text = code.ToString();
            label1.Text = table.Rows[0]["product"].ToString();
            label2.Text = table.Rows[0]["description"].ToString();
            label3.Text = Convert.ToInt32(table.Rows[0]["coste"]).ToString();
            try
            {
                pictureBox1.Image = Image.FromFile($@".\\.\\{table.Rows[0]["img"]}");
            }
            catch
            {
                pictureBox1.Image = Image.FromFile($@".\\.\\img\\none.png");
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataBase.toDB($"exec [pay] '{Home.user_id}', '{label4.Text}', '0','0'");
            MessageBox.Show("Дождитесь списания и обновите корзину. При успешной оплате, товар пропадёт из корзины");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataBase.toDB($"delete from [baskets] where [id] = '{label4.Text}'");
            this.Visible = false;
            MessageBox.Show("Удалено");
        }
    }
}
