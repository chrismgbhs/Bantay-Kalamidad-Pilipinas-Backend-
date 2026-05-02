using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Bantay_Kalamidad_Pilipinas__Backend_
{
    internal class Database_Manager
    {
        public static string connectionString = "Data Source=CHREGION\\SQLEXPRESS;Initial Catalog=\"Bantay Kalamidad Pilipinas\";Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=\"SQL Server Management Studio\";";

        // LOGIN FUNCTION

        /// <summary>
        /// Attempts to authenticate a user with the specified username, password, and role.
        /// </summary>
        /// <remarks>The method updates the current user context upon successful authentication. If the
        /// credentials or role do not match an existing user, the method returns false. The method does not throw
        /// exceptions for authentication failures but may write error messages to the console.</remarks>
        /// <param name="username">The username of the user attempting to log in. Cannot be null or empty.</param>
        /// <param name="password">The password associated with the specified username. Cannot be null or empty.</param>
        /// <param name="role">The role of the user to authenticate. Must be "Admin", "Donor", or "Volunteer".</param>
        /// <returns>true if the user is successfully authenticated with the specified role; otherwise, false.</returns>
        public static bool Login(string username, string password, string role)
        {
            bool status = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {

                    string query = "";

                    switch (role)
                    {
                        case "Admin":
                            query = $"SELECT * FROM Users WHERE Username = @username AND Password = @password AND Role = 'Admin'";
                            break;
                        case "Donor":
                            query = $"SELECT * FROM Users WHERE Username = @username AND Password = @password AND Role = 'Donor'";
                            break;
                        case "Volunteer":
                            query = $"SELECT * FROM Users WHERE Username = @username AND Password = @password AND Role = 'Volunteer'";
                            break;
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // INITIALIZATION OF DATA

                                status = true;
                                CurrentUser.Username = username;
                                CurrentUser.Password = password;
                                CurrentUser.ID = reader["ID"].ToString();

                            }

                            else
                            {
                                Console.WriteLine("User not found.");
                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }

            return status;
        }

        // GENERAL SHOW TABLE FUNCTIONS

        /// <summary>
        /// Displays all rows and columns from the specified database table in the console output.
        /// </summary>
        /// <remarks>If the specified table contains no rows, a message indicating that no data was found
        /// is displayed. The method writes output directly to the console and is intended for interactive or diagnostic
        /// use.</remarks>
        /// <param name="tableName">The name of the database table to display. Must correspond to an existing table in the connected database.</param>
        public static void ShowTable(string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"SELECT * FROM {tableName}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Display column headers
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.Write($"{reader.GetName(i)}\t");
                                }
                                Console.WriteLine();
                                // Display rows
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write($"{reader[i]}\t");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("No data found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Displays all rows from the specified table that satisfy the given condition and indicates whether any
        /// matching rows were found.
        /// </summary>
        /// <remarks>The method writes the column headers and matching rows to the console. If no rows
        /// match the condition, a message is displayed instead. The method does not throw exceptions for query or
        /// connection errors; instead, error messages are written to the console.</remarks>
        /// <param name="tableName">The name of the database table to query. Cannot be null or empty.</param>
        /// <param name="condition">The SQL condition to apply to the query's WHERE clause. Should be a valid SQL expression.</param>
        /// <param name="result">When this method returns, contains <see langword="true"/> if at least one row matches the condition;
        /// otherwise, <see langword="false"/>. This parameter is passed uninitialized.</param>
        public static void ShowTableWithCondition(string tableName, string condition, out bool result)
        {
            result = false;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = $"SELECT * FROM {tableName} WHERE {condition}";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                result |= true;
                                // Display column headers
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    Console.Write($"{reader.GetName(i)}\t");
                                }
                                Console.WriteLine();
                                // Display rows
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        Console.Write($"{reader[i]}\t");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else
                            {
                                Console.WriteLine("No data found.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // ADDING PLEDGE

        /// <summary>
        /// Adds a new pledge record for the specified donor on the given date.
        /// </summary>
        /// <remarks>This method creates a new pledge entry in the database for the specified donor.
        /// Ensure that the donor ID corresponds to an existing donor record before calling this method.</remarks>
        /// <param name="donorID">The unique identifier of the donor for whom the pledge is being added. Cannot be null or empty.</param>
        /// <param name="pledgeDate">The date on which the pledge is made.</param>
        public static void AddPledge(string donorID, DateTime pledgeDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "usp_AddPledge (@donorID, @pledgeDate);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@donorID", donorID);
                        command.Parameters.AddWithValue("@pledgeDate", pledgeDate);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Pledge added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add pledge.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a pledge item with the specified name, quantity, and expected delivery date to the data store.
        /// </summary>
        /// <remarks>This method records a new pledge item in the underlying data store. Ensure that the
        /// provided parameters meet the required constraints to avoid errors during the operation.</remarks>
        /// <param name="itemName">The name of the item being pledged. Cannot be null or empty.</param>
        /// <param name="quantity">The number of items being pledged. Must be greater than zero.</param>
        /// <param name="deliveryDate">The expected date when the pledged item will be delivered.</param>
        public static void AddPledgeItem (string itemName, int quantity, DateTime deliveryDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "usp_AddPledgeItem (@itemName, @quantity, @expectedDeliveryDate);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@itemName", itemName);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        command.Parameters.AddWithValue("@expectedDeliveryDate", deliveryDate);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Pledge added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add pledge.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // ADDING DONATION

        /// <summary>
        /// Adds a new donation record for the specified donor and event with the given date received.
        /// </summary>
        /// <remarks>This method inserts a donation into the database using the provided donor and event
        /// identifiers. Ensure that the donor and event exist before calling this method.</remarks>
        /// <param name="donorID">The unique identifier of the donor making the donation. Cannot be null or empty.</param>
        /// <param name="eventID">The unique identifier of the event associated with the donation. Cannot be null or empty.</param>
        /// <param name="dateReceived">The date and time when the donation was received.</param>
        public static void AddDonation(string donorID, string eventID, DateTime dateReceived)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "usp_AddDonation (@donorID, @eventID, @dateReceived);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@donorID", donorID);
                        command.Parameters.AddWithValue("@eventID", eventID);
                        command.Parameters.AddWithValue("@dateReceived", dateReceived);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Donation added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add donation.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Adds a donated item with the specified name and quantity to the database.
        /// </summary>
        /// <remarks>This method inserts a new donated item record using a stored procedure. If the
        /// operation fails, an error message is written to the console.</remarks>
        /// <param name="itemName">The name of the donated item to add. Cannot be null or empty.</param>
        /// <param name="quantity">The quantity of the donated item to add. Must be greater than zero.</param>
        public static void AddDonatedItem(string itemName, int quantity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "usp_AddDonatedItem (@itemName, @quantity);";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@itemName", itemName);
                        command.Parameters.AddWithValue("@quantity", quantity);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Item added successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Failed to add pledge.");
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

    }
}
