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
            Console.WriteLine("6. Rescue Operation");
            Console.WriteLine("7. Waste Management");
            Console.WriteLine("8. Rescue Operations");
            Console.WriteLine("9. Volunteers");
            Console.Write("Response: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddDonation();
                    break;

                case "2":
                    AddBenificiary();
                    break;

                case "3":
                    AddCenter();
                    break;

                case "4":
                    AddLocation();
                    break;

                case "5":
                    AddDistribution();
                    break;

                case "6":
                    AddDelivery();
                    break;

                case "7":
                    WasteManagement();
                    break;

                case "8":
                    AddRescue();
                    break;
            }
        }

        public static void WasteManagement() 
        {
            Console.WriteLine("Waste management functionality is currently under development. Please check back later for updates.");
        }

        //ADDING FUNCTIONALITY

        public static void AddRescue() 
        {
            Console.WriteLine("Rescue operation functionality is currently under development. Please check back later for updates.");
        }

        /// <summary>
        /// Adds a new delivery schedule to the database by prompting the user for the distribution ID, delivery date, and delivery status. The method validates the input to ensure that the distribution ID exists in the database and that the delivery status is one of the accepted values ("delivered", "pending", or "in transit"). It then calls the Database_Manager.AddDeliverySchedule method to save the delivery information to the database. This method is intended for use by administrators to manage and track the delivery of donated items to beneficiaries or evacuation centers as part of disaster response efforts.
        /// </summary>
        public static void AddDelivery()
        {
            string distributionID;
            string status;
            DateTime deliveryDate;
            Console.WriteLine("Please select the distribution from the list below:");
            Database_Manager.ShowTable("Distribution");

            while (true)
            {
                bool result;
                Console.Write("Distribution ID: ");
                distributionID = Console.ReadLine().Trim();

                Database_Manager.ShowTableWithCondition("Distribution", $"Distribution_ID = '{distributionID}'", out result);
                
                if (result)
                {
                    break;
                }
            }

            deliveryDate = DatePicker();

            while (true)
            {
                Console.Write("Delivery status: ");
                status = Console.ReadLine().Trim().ToLower();

                if (status == "delivered" || status == "pending" || status == "in transit")
                {
                    break;
                }

                else
                {
                    Console.WriteLine("❌ Invalid status. Please enter 'delivered', 'pending', or 'in transit'.");
                }
            }

            Database_Manager.AddDeliverySchedule(distributionID, deliveryDate, status);

        }

        /// <summary>
        /// Adds a new distribution record to the database. This method is currently a placeholder and does not contain any implementation. It is intended to be developed in the future to allow administrators to record the distribution of donated items to beneficiaries or evacuation centers. The method will likely involve prompting the user for relevant information such as the item being distributed, the quantity, the recipient, and the date of distribution, and then calling a corresponding method in the Database_Manager class to save this information to the database.
        /// </summary>
        public static void AddDistribution() 
        {
            string benificiaryID;
            string eventID;
            string centerID;
            DateTime dateDistributed;
            Console.WriteLine("Please select the benificiary from the list below:");
            Database_Manager.ShowTable("Benificiary");

            while (true)
            {
                bool result;
                Console.Write("Benificiary ID: ");
                benificiaryID = Console.ReadLine().Trim();
                Database_Manager.ShowTableWithCondition("Benificiary", $"Benificiary_ID = '{benificiaryID}'", out result);

                if (result)
                {
                    break;
                }

                else
                {
                    Console.WriteLine($"❌ Benificiary ID {benificiaryID} is invalid. Please try again.");
                }
            }

            Console.WriteLine("Please select the event from the list below:");
            Database_Manager.ShowTable("Event");

            while (true)
            {
                bool result;
                Console.Write("Event ID: ");
                eventID = Console.ReadLine().Trim();
                Database_Manager.ShowTableWithCondition("[Disaster Event]", $"Event_ID = '{eventID}'", out result);

                if (result)
                {
                    break;
                }

                else
                {
                    Console.WriteLine($"❌ Event ID {eventID} is invalid. Please try again.");
                }
            }

            Console.WriteLine("Please select the center from the list below:");
            Database_Manager.ShowTable("Center");

            while (true)
            {
                bool result;
                Console.Write("Center ID: ");
                centerID = Console.ReadLine().Trim();
                Database_Manager.ShowTableWithCondition("[Evacuation Center]", $"Center_ID = '{centerID}'", out result);

                if (result)
                {
                    break;
                }

                else
                {
                    Console.WriteLine($"❌ Center ID {centerID} is invalid. Please try again.");
                }
            }

            dateDistributed = DatePicker();

            Database_Manager.AddDistribution(benificiaryID, eventID, centerID, dateDistributed);
        }

        /// <summary>
        /// Adds a new location to the database by prompting the user for the barangay, city, and province. The method validates the input to ensure that none of the fields are left empty or contain only whitespace. If the input is valid, it calls the Database_Manager.AddLocation method to add the location to the database. The method provides feedback to the user throughout the process, indicating any errors in the input and confirming successful addition of the location.
        /// </summary>
        public static void AddLocation()
        {
            string barangay;
            string city;
            string province;

            while (true)
            {
                Console.Write("Barangay: ");
                barangay = Console.ReadLine().Trim();

                if (barangay == "" || barangay == " ")
                {
                    Console.WriteLine("❌ Barangay cannot be empty. Please enter a valid barangay.");
                }

                else
                {
                    break;
                }
            }

            while (true)
            {
                Console.Write("City: ");
                city = Console.ReadLine().Trim();

                if (city == "" || city == " ")
                {
                    Console.WriteLine("❌ City cannot be empty. Please enter a valid city.");
                }

                else
                {
                    break;
                }
            }

            while (true)
            {
                Console.Write("Province: ");
                province = Console.ReadLine().Trim();

                if (province == "" || province == " ")
                {
                    Console.WriteLine("❌ Province cannot be empty. Please enter a valid province.");
                }

                else
                {
                    break;
                }
            }

            Database_Manager.AddLocation(barangay, city, province);
        }

        /// <summary>
        /// Adds a new evacuation center to the database by prompting the user for the center's name, location ID, and capacity. The method validates the input and ensures that the location ID exists in the database before adding the center. It provides feedback to the user throughout the process and interacts with the Database_Manager class to perform the necessary database operations.
        /// </summary>
        public static void AddCenter()
        {
            string centerName;
            string locationID;
            int capacity;

            while (true)
            {
                Console.Write("Center Name: ");
                centerName = Console.ReadLine().Trim();

                if (centerName == "" || centerName == " ")
                {
                    Console.WriteLine("❌ Center name cannot be empty. Please enter a valid center name.");
                }

                else
                {
                    break;
                }

            }

            Console.WriteLine("Please select the location from the list below:");
            Database_Manager.ShowTable("Location");

            while (true)
            {
                bool result;
                Console.Write("Location ID: ");
                locationID = Console.ReadLine().Trim();
                Database_Manager.ShowTableWithCondition("Location", $"Location_ID = '{locationID}'", out result);

                if (result)
                {
                    break;
                }

                else
                {
                    Console.WriteLine($"❌ Location ID {locationID} is invalid. Please try again.");
                }
            }

            while (true)
            {
                Console.Write("Capacity: ");
                capacity = int.Parse(Console.ReadLine());

                if (capacity > 0) 
                {
                    break;
                }

                else
                {
                    Console.WriteLine("❌ Capacity must be a positive integer. Please enter a valid capacity.");  
                }
            }

            Database_Manager.AddCenter(centerName, locationID, capacity);
        }

        /// <summary>
        /// Adds a new beneficiary to the database by prompting the user for the beneficiary's name, category, and associated evacuation center ID. The method validates the input and ensures that the center ID exists in the database before adding the beneficiary. It provides feedback to the user throughout the process and interacts with the Database_Manager class to perform the necessary database operations.
        /// </summary>
        public static void AddBenificiary()
        {
            string name;
            string category;
            string centerID;

            while (true)
            {
                Console.Write("Name: ");
                name = Console.ReadLine().Trim();

                if (name == "" || name == " ")
                {
                    Console.WriteLine("❌ Name cannot be empty. Please enter a valid name.");
                }

                else
                {
                    break;
                }
            }


            while (true)
            {
                Console.Write("Category: ");
                category = Console.ReadLine().Trim();

                if (category == "" || category == " ")
                {
                    Console.WriteLine("❌ Category cannot be empty. Please enter a valid category.");
                }

                else
                {
                    break;
                }
            }


            Console.WriteLine("Please select the center from the list below:");
            Database_Manager.ShowTable("[Evacuation Center]");

            while (true)
            {
                Console.Write("Center ID: ");
                centerID = Console.ReadLine().Trim();

                if (centerID == "" || centerID == " ")
                {
                    Console.WriteLine("❌ Center ID cannot be empty. Please enter a valid center ID.");
                }

                else
                {
                    bool result;
                    Database_Manager.ShowTableWithCondition("[Evacuation Center]", $"Center_ID = '{centerID}'", out result);

                    if (result)
                    {
                        break;
                    }

                    else
                    {
                        Console.WriteLine($"❌ Center ID {centerID} is invalid. Please try again.");
                    }

                }
            }

            Database_Manager.AddBenificiary(name, category, centerID);

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
            string unit;
            string category;
            string location;

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
                    Console.Write("Quantity: ");
                    if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                    {
                        quantity = int.Parse(Console.ReadLine());
                        break;
                    }

                    else
                    {
                        Console.WriteLine("❌ Quantity must be a positive integer. Please enter a valid quantity.");
                    }
                }

                Console.Write("Unit: ");
                unit = Console.ReadLine().Trim();

                Console.Write("Category: ");
                category = Console.ReadLine().Trim();

                Console.Write("Location: ");
                location = Console.ReadLine().Trim();

                Console.WriteLine("Please choose expiration date.");
                DateTime expDate = DatePicker();

                Database_Manager.AddDonatedItem(item, quantity, unit, category, expDate, location);
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
