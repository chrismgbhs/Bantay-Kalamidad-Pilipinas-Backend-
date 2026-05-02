using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
                    AdminMenu();
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

        public static void AdminMenu()
        {
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. Donation");
            Console.WriteLine("2. Benificiary");
            Console.WriteLine("3. Centers");
            Console.WriteLine("4. Locations");
            Console.WriteLine("5. Distribution");
            Console.WriteLine("6. Waste Management");
            Console.WriteLine("7. Rescue Operations");
            Console.WriteLine("8. Volunteers");
            Console.Write("Response: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddDonation();
                    break;
            }
        }

        /// <summary>
        /// Guides the user through the process of adding a donation or pledge by prompting for donor, event, and item
        /// details via the console.
        /// </summary>
        /// <remarks>This method interacts with the user through the console to collect and validate donor
        /// and event information, and to record either a pledge or a direct donation. It displays relevant tables and
        /// prompts for input as needed. The method is intended for interactive, console-based applications and is not
        /// thread-safe.</remarks>
        public static void AddDonation()
        {
            Console.WriteLine("Please select the donor from the list below:");
            Database_Manager.ShowTable("Donor");
            string donorID;
            string eventID;
            string item;
            int quantity;

            while (true) 
            {
                Console.Write("Donor ID: ");
                donorID = Console.ReadLine().Trim();
                Database_Manager.ShowTableWithCondition("Donor", $"Donor_ID = '{donorID}'", out bool result);
                if (result)
                {
                    Console.WriteLine($"✅ Donor ID {donorID} is valid.");
                    break;
                }
                else
                {
                    Console.WriteLine($"❌ Donor ID {donorID} is invalid. Please try again.");
                }
            }

            Console.WriteLine("Do you want to add a pledge? (yes/no)");
            string response = Console.ReadLine().Trim().ToLower();

            if (response == "yes")
            {
                AddPledge(donorID);
            }

            else
            {
                Database_Manager.ShowTable("Disaster Event");

                while (true)
                {
                    Console.Write("Please select the event ID from the list above: ");
                    eventID = Console.ReadLine().Trim();

                    if (eventID == "" || eventID == " ")
                    {
                        Console.WriteLine("❌ Event ID cannot be empty. Please enter a valid event ID.");
                        continue;
                    }

                    else
                    {
                        break;
                    }
                }

                Database_Manager.ShowTableWithCondition("Disaster Event", $"Event_ID = '{eventID}'", out bool eventResult);
                
                if (eventResult)
                {
                    Console.WriteLine($"✅ Event ID {eventID} is valid.");
                    DateTime dateReceived = DatePicker();
                    Database_Manager.AddDonation(donorID, eventID, dateReceived);
                    Console.WriteLine("Donation added successfully!");

                }

                else
                {
                    Console.WriteLine($"❌ Event ID {eventID} is invalid. Please try again.");
                }

                while (true)
                {
                    Console.WriteLine("Please enter the item that has been donated.");
                    Console.Write("Item: ");
                    item = Console.ReadLine().Trim();

                    if (string.IsNullOrWhiteSpace(item))
                    {
                        Console.WriteLine("❌ Item cannot be empty. Please enter a valid item.");
                    }

                    else
                    {
                        break;
                    }
                }

                while (true)
                {
                    Console.Write
                }

                while (true)
                {
                    Console.Write("Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                    {
                        Database_Manager.AddDonatedItem(item, quantity);
                    }
                }
            }
        }

        /// <summary>
        /// Prompts the user to enter a date in the console and returns the selected date.
        /// </summary>
        /// <remarks>The user must enter the date in the format yyyy-MM-dd. If the input is invalid, the
        /// prompt repeats until a valid date is entered. The method defaults to today's date if the input is left
        /// blank.</remarks>
        /// <returns>A DateTime value representing the date entered by the user. If the user provides no input, the current date
        /// is returned.</returns>
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

        /// <summary>
        /// Initiates the process for a donor to add a pledge and associated items to the database.
        /// </summary>
        /// <remarks>This method prompts the user to enter pledge item details interactively via the
        /// console. Each item and its quantity are added to the pledge for the specified donor. The process continues
        /// until the user indicates completion.</remarks>
        /// <param name="donorID">The unique identifier of the donor making the pledge. Cannot be null or empty.</param>
        public static void AddPledge(string donorID) 
        {
            int quantity;
            DateTime selectedDate = DatePicker();
            Database_Manager.AddPledge(donorID, selectedDate);
            while (true)
            {
                string item = string.Empty;
                Console.WriteLine("Enter the item you want to pledge.");

                while (true)
                {
                    Console.Write("Item: ");
                    item = Console.ReadLine().Trim();

                    if (item == "" || item == " ")
                    {
                        Console.WriteLine("❌ Item cannot be empty. Please enter a valid item.");
                    }

                    else
                    {
                        break;
                    }
                }

                while (true)
                {
                    Console.Write("Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                    {
                        break;
                    }
                }

                Database_Manager.AddPledgeItem(item, quantity, selectedDate);
            }
        }

    }
}
