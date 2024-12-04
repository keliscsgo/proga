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
    public partial class UserItem : UserControl
    {
        public static int usercode { get; set; }
        public UserItem()
        {
            InitializeComponent();
        }

        private void UserItem_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable user = DataBase.fromDB(WAF.get_user(usercode.ToString()));
                label1.Text = user.Rows[0]["login"].ToString();
                switch ((int)user.Rows[0]["role"])
                {
                    case 1:
                        label2.Text = "Заказчик"; break;
                    case 2:
                        label2.Text = "Бригадир"; break;
                    case 3:
                        label2.Text = "Администратор"; break;
                    default:
                        label2.Text = "Не определено"; break;
                }
                label3.Text = $"Баланс: {Convert.ToInt32(user.Rows[0]["money"])}";
                label4.Text = user.Rows[0]["surname"].ToString();
                label5.Text = user.Rows[0]["name"].ToString();
                label6.Text = user.Rows[0]["lastname"].ToString();
                label7.Text = user.Rows[0]["id"].ToString();
            }
            catch
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DataBase.toDB($"delete from [users] where [id] = '{label7.Text}'");
            this.Visible = false;
        }
    }
}
