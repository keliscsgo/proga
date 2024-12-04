using sport_invertar_shop.UserControll;
using sport_invertar_shop.UserControls;
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
    public partial class PostavPanel : Form
    {
        public static int user { get; set; }
        public static int provider { get; set; }
        public PostavPanel()
        {
            InitializeComponent();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PostavPanel_Load(object sender, EventArgs e)
        {
            reload();
        }

        void reload()
        {
            DataTable table = DataBase.fromDB(WAF.me_providers(user));
            DataTable tables = DataBase.fromDB(WAF.providers_prodo(Convert.ToInt32(table.Rows[0]["id"])));
            provider = Convert.ToInt32(table.Rows[0]["id"]);
            if (tables != null)
            {
                flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < tables.Rows.Count; i++)
                {
                    ProductPostav item = new ProductPostav();
                    ProductPostav.code = (int)tables.Rows[i]["id"];
                    ProductPostav.provider = provider;
                    flowLayoutPanel1.Controls.Add(item);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DataBase.toDB($"insert into [products]([name],[provider]) values('{WAF.text(textBox1.Text)}', '{provider}');");
            reload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
