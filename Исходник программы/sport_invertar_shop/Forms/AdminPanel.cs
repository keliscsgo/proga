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
    public partial class AdminPanel : Form
    {
        public AdminPanel()
        {
            InitializeComponent();
        }

        private void AdminPanel_Load(object sender, EventArgs e)
        {
            reload();
        }

        void reload()
        {
            DataTable table = DataBase.fromDB("select * from [users]");
            if (table != null)
            {
                flowLayoutPanel1.Controls.Clear();
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    UserItem item = new UserItem();
                    UserItem.usercode = (int)table.Rows[i]["id"];
                    flowLayoutPanel1.Controls.Add(item);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            reload();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
