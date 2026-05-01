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

                Console.WriteLine("Would you like to make a donation? (yes/no)");
                string makeDonation = Console.ReadLine().Trim().ToLower();

                if (makeDonation == "yes")
                {
                    // Call the method to handle donation process
                    MakeDonation();
                }
                else
                {
                    Console.WriteLine("Thank you for logging in. Have a great day!");
                }
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
            Database_Manager.ShowTableWithCondition("Pledge", $"\'{CurrentUser.ID}\' = Donor_ID");
        }

        public static void MakeDonation() 
        {
            Console.WriteLine("Do you want to make a pledge? (yes/no)");
            string response = Console.ReadLine().Trim().ToLower();

            if (response == "yes") 
            {
                DateTime selectedDate = DatePicker();

                Database_Manager
            }
        }

        public static DateTime DatePicker()
        {
            DateTime selectedDate;

            Console.WriteLine("=== Console Date Picker ===");
            Console.WriteLine("Enter your pledge date (format: yyyy-MM-dd): ");

            while (true)
            {
                Console.Write("Date: ");
                string input = Console.ReadLine()?.Trim();

                // Default to today's date if empty
                if (string.IsNullOrEmpty(input))
                {
                    selectedDate = DateTime.Today;
                    break;
                }

                // Try parsing with strict format

                if (DateTime.TryParseExact(input, "yyyy-MM-dd",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out selectedDate))
                {
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid date format. Please use yyyy-MM-dd (e.g., 2026-05-01).");
                }
            }

            Console.WriteLine($"✅ You selected: {selectedDate:D}");
            return selectedDate;
        }
    }
}
