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
        public static string connectionString = "Data Source=CCL2-10\\MSSQLSERVER01;Initial Catalog=\"Bantay Kalamidad Pilipinas\";Persist Security Info=True;User ID=sa;Password=ccl2;Pooling=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Application Name=\"SQL Server Management Studio\";";

        // LOGIN FUNCTION
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
    }
}
