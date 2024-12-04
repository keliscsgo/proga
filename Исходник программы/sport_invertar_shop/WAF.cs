using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sport_invertar_shop
{
    internal class WAF
    {

        public static string login(string login, string password)
        {
            return $"execute [login] '{text(login)}', '{text(password)}';";
        }

        public static string signup(string login, string password)
        {
            return $"execute [signup] '{text(login)}', '{text(password)}';";
        }

        public static string allproduct()
        {
            return $"execute [allproduct];";
        }

        public static string me_providers(int code)
        {
            return $"exec [me_providers] '{code}';";
        }

        public static string providers_prodo(int code)
        {
            return $"exec [providers_prodo] '{code}'";
        }

        public static string get_my_basket(int code)
        {
            return $"execute [get_my_basket] '{code}'";
        }

        public static string get_basket()
        {
            return $"execute [get_basket]";
        }

        public static string get_info_basket(int code)
        {
            return $"exec [info_basket] '{code}'";
        }

        public static string get_search(string search, string categoites)
        {
            if (categoites == "Все категории")
            {
                return $"execute [get_search] '%{text(search)}%', '1';";
            }
            else
            {
                DataTable table = DataBase.fromDB($"select [id], [name] from [categories] where [name] = '{text(categoites)}'");
                return $"execute [get_search] '%{text(search)}%', '{table.Rows[0]["id"]}'";
            }
            
        }

        public static string allcategories()
        {
            return $"execute [allcategories];";
        }

        public static string product(string code)
        {
            return $"execute [product] '{code}'";
        }

        public static string get_user(string code)
        {
            return $"execute [get_user] '{code}'";
        }

        public static string update_account(string code, string password, string surname, string name, string firstname)
        {
            return $"exec [update_account] '{text(code)}', '{text(password)}', '{text(surname)}','{text(name)}','{text(firstname)}'";
        }

        public static string check_login(string login)
        {
            return $"execute [check_login] '{text(login)}';";
        }

        public static string text(string text)
        {
            return text.Replace('\'', ' ').Trim();
        }

        public static string update_product(string code, string name, string description, int provider, int categoria, string img, int money, int availability)
        {
            return $"exec [update_product] '{text(code)}', '{text(name)}', '{text(description)}', '{provider}', '{categoria}', '{text(img)}', '{money}', '{availability}';";
        }
    }
}
