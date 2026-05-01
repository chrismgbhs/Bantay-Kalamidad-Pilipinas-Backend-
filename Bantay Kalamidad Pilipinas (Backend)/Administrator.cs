using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bantay_Kalamidad_Pilipinas__Backend_
{
    internal class Administrator
    {
        public static void AdminLogin()
        {
            Console.Write("Please enter your username: ");
            string username = Console.ReadLine();
            Console.Write("Please enter your password:  ");
            string password = Console.ReadLine();
            bool loginStatus = Database_Manager.Login(username, password, "admin");
            if (loginStatus)
            {
                if (CurrentUser.Role == "Admin")
                {
                    Console.WriteLine("Login successful! Welcome, Admin.");
                    // Proceed to admin menu or functionality
                }
                else
                {
                    Console.WriteLine("Access denied. You are not an admin.");
                    // Optionally, redirect to the main menu or login again
                }
            }
            else
            {
                Console.WriteLine("Login failed. Please check your credentials and try again.");
                // Optionally, redirect to the main menu or login again
            }
        }
    }
}
