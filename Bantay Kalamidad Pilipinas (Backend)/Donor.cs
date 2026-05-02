using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bantay_Kalamidad_Pilipinas__Backend_
{
    internal class Donor
    {
        public static void DonorLogin()
        {
            Console.Write("Enter Username: ");
            string username = Console.ReadLine();
            Console.Write("Enter Password: ");
            string password = Console.ReadLine();
            bool loginSuccess = Database_Manager.Login(username, password, "Donor");
            if (loginSuccess)
            {
                Console.WriteLine("Login successful!");
                ShowDonationAndPledgeTable();
            }
            else
            {
                Console.WriteLine("Login failed. Please check your credentials.");
                // Optionally, you can call DonorLogin() again for retry
            }
        }

        public static void ShowDonationAndPledgeTable()
        {
            Database_Manager.ShowTable("Donation");
            Database_Manager.ShowTableWithCondition("Pledge", $"\'{CurrentUser.ID}\' = Donor_ID", out bool result);
        }
    }
}
