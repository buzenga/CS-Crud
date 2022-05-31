using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagementApp
{
    internal class DataBase
    {
        public static string GetConnectionString()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Kuba\source\repos\CarAppDB\CarDb.mdf;Integrated Security=True;Connect Timeout=30";
            return connectionString;
        }
        public static void AppendToDb()
        {
            Console.Clear();

            try
            {
                SqlConnection sqlConnection;
                /*string connectionString = @"Data Source=DESKTOP-F3KC1IH\TEW_SQLEXPRESS;Initial Catalog=CarManagementDb;Integrated Security=True";*/
                string connectionString = GetConnectionString();

                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    Console.WriteLine("Connection to DataBase established successfully");

                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();

                    Console.WriteLine("Insert a plate number: ");
                    string plateNumber = Console.ReadLine();

                    Console.WriteLine("Insert car's producer name: ");
                    string producerName = Console.ReadLine();

                    Console.WriteLine("Insert car's model: ");
                    string model = Console.ReadLine();

                    Console.WriteLine("Insert Owner's name: ");
                    string owner = Console.ReadLine();

                    var car = new Car(plateNumber, producerName, model, owner);

                    string insertQuery = $@"INSERT INTO Cars(plate_number,car_producer,car_model,car_owner) VALUES('{car.PlateNumber}','{car.ProducerName}','{car.Model}','{car.Owner}')";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                    insertCommand.ExecuteNonQuery();

                    Console.WriteLine($"The {model} of {owner} has been added successfully to the DataBase");
                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    sqlConnection.Close();

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal static void FindCar()
        {
            Console.Clear();

            try
            {
                string searchPhrase = "";

                SqlConnection sqlConnection;
                string connectionString = GetConnectionString();

                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    Console.WriteLine("Insert the signs on a plate number, that you are searching for : ");
                    searchPhrase = Console.ReadLine();

                    string getQuery = "Select * FROM Cars";
                    SqlCommand getCommand = new SqlCommand(getQuery, sqlConnection);    
                    SqlDataReader dataReader = getCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        if (dataReader.GetValue(0).ToString().Contains(searchPhrase))  
                        {
                            var car = new Car(dataReader.GetValue(0).ToString(),
                                dataReader.GetValue(1).ToString(),
                                dataReader.GetValue(2).ToString(),
                                dataReader.GetValue(3).ToString());

                            CarsCollection.AddCar(car);
                        }
                    }
                    dataReader.Close();

                    Console.Clear();
                    Console.WriteLine($"List of the cars with plate number including '{searchPhrase}'\n");
                    CarsCollection.ShowCollection();
                    CarsCollection.ClearCollection();

                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    sqlConnection.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal static void ChangePlateNumber()
        {
            Console.Clear();

            try
            {
                string oldPlateNumber;
                string newPlateNumber;
                
                //Jak nic tu nie przypisałem, to program się sypał po pętli while - te zmienne były unassigned
                string temporaryProducer = "";
                string temporaryModel = "";
                string temporaryOwner = "";
                

                SqlConnection sqlConnection;
                string connectionString = GetConnectionString();

                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    Console.WriteLine("Connection to DataBase established successfully");

                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();

                    Console.WriteLine("Insert the plate number you want to change: ");
                    oldPlateNumber = Console.ReadLine();
                    Console.WriteLine("Insert the new plate number: ");
                    newPlateNumber = Console.ReadLine();

                    string displayQuery = "Select * FROM Cars";
                    SqlCommand displayCommand = new SqlCommand(displayQuery, sqlConnection);
                    SqlDataReader reader = displayCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        if (reader.GetValue(0).ToString() == oldPlateNumber)
                        {
                             var car = new Car(reader.GetValue(0).ToString(),
                                              reader.GetValue(1).ToString(),
                                              reader.GetValue(2).ToString(),
                                              reader.GetValue(3).ToString());

                            temporaryProducer = car.ProducerName;
                            temporaryModel = car.Model;
                            temporaryOwner = car.Owner;
                            break;
                        }
                    }
                    reader.Close();


                    string deleteQuery = $@"DELETE FROM Cars WHERE plate_number = '{oldPlateNumber}'";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
                    deleteCommand.ExecuteNonQuery();

                    string insertQuery = $@"INSERT INTO Cars(plate_number,car_producer,car_model,car_owner) VALUES('{newPlateNumber}','{temporaryProducer}','{temporaryModel}','{temporaryOwner}')";
                    SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection);
                    insertCommand.ExecuteNonQuery();

                    Console.WriteLine($"The plate number has been changed from {oldPlateNumber} to {newPlateNumber}");

                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    sqlConnection.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        internal static void ChangeOwner()
        {
            Console.Clear();

            try
            {
                string keyPlateNumber;
                string newOwner;

                SqlConnection sqlConnection;
                string connectionString = GetConnectionString();

                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    Console.WriteLine("Connection to DataBase established successfully");

                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();

                    Console.WriteLine("Insert plate number of the car: ");
                    keyPlateNumber = Console.ReadLine();
                    Console.WriteLine("Insert new owner's name: ");
                    newOwner = Console.ReadLine();

                    string displayQuery = "Select * FROM Cars";
                    SqlCommand displayCommand = new SqlCommand(displayQuery, sqlConnection);
                    SqlDataReader reader = displayCommand.ExecuteReader();

                    while(reader.Read())
                    {
                        if(reader.GetValue(0).ToString() == keyPlateNumber)
                        {
                            var car = new Car(reader.GetValue(0).ToString(),
                                              reader.GetValue(1).ToString(),
                                              reader.GetValue(2).ToString(),
                                              reader.GetValue(3).ToString());

                            break;

                        }
                    }
                    reader.Close();

                    string updateQuery = $@"UPDATE Cars SET car_owner = '{newOwner}' WHERE plate_number = '{keyPlateNumber}'";
                    SqlCommand updateCommand = new SqlCommand(@updateQuery, sqlConnection);
                    updateCommand.ExecuteNonQuery();
                    Console.WriteLine($"The car's (plate number: {keyPlateNumber}) owner has been changed to {newOwner}.");

                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    sqlConnection.Close();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static void DisplayDbData()
        {
            Console.Clear();

            try
            {
                SqlConnection sqlConnection;
                string connectionString = GetConnectionString();

                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();

                    string displayQuery = "Select * FROM Cars";
                    SqlCommand displayCommand = new SqlCommand(@displayQuery, sqlConnection);
                    SqlDataReader dataReader = displayCommand.ExecuteReader();

                    Console.WriteLine("A list of the cars your company is responsible for: ");
                    Console.WriteLine("------------------------------");

                    while (dataReader.Read())
                    {
                        var car = new Car(dataReader.GetValue(0).ToString(),
                                          dataReader.GetValue(1).ToString(),
                                          dataReader.GetValue(2).ToString(),
                                          dataReader.GetValue(3).ToString());

                        Console.WriteLine(car.PlateNumber);
                        Console.WriteLine(car.ProducerName);
                        Console.WriteLine(car.Model);
                        Console.WriteLine(car.Owner);
                        Console.WriteLine("------------------------------");
                    }
                    dataReader.Close();

                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void RemoveFromDB()
        {
            Console.Clear();

            try
            {
                SqlConnection sqlConnection;
                string connectionString = GetConnectionString();

                using (sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    Console.WriteLine("Connection to DataBase established successfully");

                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    Console.Clear();

                    string plateNumberToDelete;
                    Console.Write("Insert plate number of a car, that you want to delete: ");
                    plateNumberToDelete = Console.ReadLine();

                    string deleteQuery = $@"DELETE FROM Cars WHERE plate_number = '{plateNumberToDelete}'";
                    SqlCommand deleteCommand = new SqlCommand(deleteQuery, sqlConnection);
                    deleteCommand.ExecuteNonQuery();
                    Console.WriteLine($"\nThe car with plate number: {plateNumberToDelete}, has been removed successfully from the DataBase");

                    Console.WriteLine("\nPress any key to continue");
                    Console.ReadKey();
                    

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
