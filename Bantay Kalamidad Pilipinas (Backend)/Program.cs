using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bantay_Kalamidad_Pilipinas__Backend_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bantay Kalamidad Pilipinas!");
                Console.WriteLine("Please select an option:");
                Console.WriteLine("1. Donation");
                Console.WriteLine("2. Benificiary");
                Console.WriteLine("3. Evacuation Center");
                Console.WriteLine("4. Location");
                Console.WriteLine("5. Distribution");
                Console.WriteLine("6. Delivery");
                Console.WriteLine("3. Waste");
                Console.WriteLine("3. Rescue");
                Console.WriteLine("3. Volunteers");

            int choice = Convert.ToInt32(Console.ReadLine());
    
                switch (choice)
                {
                    case 1:
                        ReportDisaster();
                        break;
                    case 2:
                        ViewDisasterReports();
                        break;
                    case 3:
                        Console.WriteLine("Thank you for using Bantay Kalamidad Pilipinas. Stay safe!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
    
            static void ReportDisaster()
            {
                Console.WriteLine("Please enter the type of disaster:");
                string disasterType = Console.ReadLine();
    
                Console.WriteLine("Please enter the location of the disaster:");
                string location = Console.ReadLine();
    
                Console.WriteLine("Please enter the date and time of the disaster (MM/DD/YYYY HH:MM):");
                DateTime dateTime = DateTime.Parse(Console.ReadLine());
    
                // Here you can add code to save the disaster report to a database or file
                Console.WriteLine("Thank you for reporting the disaster. Your report has been saved.");
            }
    
            static void ViewDisasterReports()
            {
                // Here you can add code to retrieve and display disaster reports from a database or file
                Console.WriteLine("Displaying all disaster reports...");
        }
    }
}
