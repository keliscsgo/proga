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
    public partial class ProductPostav : UserControl
    {
        public static int code { get; set; }
        public static int provider { get; set; }
        public ProductPostav()
        {
            InitializeComponent();
            DataTable table = DataBase.fromDB(WAF.allcategories());
            for (int i = 0; i < table.Rows.Count; i++)
            {
                comboBox1.Items.Add(table.Rows[i]["name"]);
                comboBox1.SelectedIndex = 0;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable table = DataBase.fromDB($"select [id], [name], [description] from [categories] where [name] = '{WAF.text(comboBox1.SelectedItem.ToString())}'");
            label7.Text = table.Rows[0]["description"].ToString();
        }

        private void ProductPostav_Load(object sender, EventArgs e)
        {
            label1.Text = code.ToString();
            DataTable table = DataBase.fromDB(WAF.product(label1.Text));
            textBox1.Text = table.Rows[0]["name"].ToString();
            textBox2.Text = table.Rows[0]["description"].ToString();
            numericUpDown1.Value = Convert.ToInt32(table.Rows[0]["coste"]);
            try
            {
                pictureBox1.Image = Image.FromFile($@".\\.\\{table.Rows[0]["img"]}");
                textBox3.Text = table.Rows[0]["img"].ToString();
            }
            catch
            {
                pictureBox1.Image = Image.FromFile($@".\\.\\img\none.png");
            }
            if ((bool)table.Rows[0]["availability"])
            {
               comboBox2.SelectedIndex = 1;
            }
            else
            {
                comboBox2.SelectedIndex = 0;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = Image.FromFile($@".\\.\\{textBox3.Text}");
            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int go;
            if (comboBox2.SelectedIndex == 1)
            {
                go = 0;
            }
            else
            {
                go = 1;
            }
            DataTable table = DataBase.fromDB($"select [id], [name], [description] from [categories] where [name] = '{WAF.text(comboBox1.SelectedItem.ToString())}'");
            label7.Text = table.Rows[0]["description"].ToString();
            DataBase.toDB(WAF.update_product(label1.Text, textBox1.Text, textBox2.Text, provider, Convert.ToInt32(table.Rows[0]["id"]), textBox3.Text, Convert.ToInt32(numericUpDown1.Value), go));
            MessageBox.Show("Обновлено");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataBase.toDB($"delete from [products] where [id] = '{label1.Text}'");
            this.Visible = false;
        }
    }
}
