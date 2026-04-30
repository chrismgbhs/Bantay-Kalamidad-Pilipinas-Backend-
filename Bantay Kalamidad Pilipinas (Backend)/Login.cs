using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bantay_Kalamidad_Pilipinas__Backend_
{
    internal class Login
    {
        public static void GoLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            UserModel user = new UserModel
            {
                Username = username,
                Password = password
            };

            Database_Manager.Login(user.Username, user.Password);

        }
    }
}
