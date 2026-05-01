using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bantay_Kalamidad_Pilipinas__Backend_
{
    internal class Volunteer
    {
        public static void VolunteerLogin()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            bool loginSuccess = Database_Manager.Login(username, password, "Volunteer");
            if (loginSuccess)
            {
                Console.WriteLine("Login successful!");
                // Proceed to volunteer dashboard or functionality
            }
            else
            {
                Console.WriteLine("Login failed. Please check your credentials.");
                // Optionally, you can call VolunteerLogin() again for retry
            }
        }
    }
}
