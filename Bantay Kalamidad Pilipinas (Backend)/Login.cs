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
            Console.WriteLine("Please select your role:");
            Console.WriteLine("1. Admin");
            Console.WriteLine("2. Donor");
            Console.WriteLine("3. Volunteer");

            switch (Console.ReadLine())
            {
                case "1":
                    Administrator.AdminLogin();
                    break;
                case "2":
                    Donor.DonorLogin();
                    break;
                case "3":
                    Volunteer.VolunteerLogin();
                    break;
                default:
                    Console.WriteLine("Invalid selection. Please try again.");
                    GoLogin();
                    break;
            }
        }
    }
}
