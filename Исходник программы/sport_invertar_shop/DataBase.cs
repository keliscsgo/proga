using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sport_invertar_shop
{
    internal class DataBase
    {
        #region[Работа с базой данных]
        public static SqlConnection SqlConnection()
        {
            return new SqlConnection(@"Data Source=FROL\SQLEXPRESS; Initial Catalog =sport_invertar_shop; Integrated Security=True");
        }

        public static void toDB(string stroke)
        {
            try
            {
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(stroke, SqlConnection());
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);
            }
            catch
            {
                MessageBox.Show("У вас неполадки с MS SQL Server. Проверьте его работоспособность и наличие базы");
            }

        }
        public static DataTable fromDB(string stroke)
        {
            try{
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(stroke, SqlConnection());
                DataSet dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet);
                DataTable dataTable = dataSet.Tables[0];
                return dataTable;
            }
            catch
            {
                DataTable dataTable = new DataTable();
                return dataTable;
            }

        }
        #endregion
    }
}
