using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Builder;
using EntityFrameworkDemo.Model;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EntityFrameworkDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets<Secrets>()
                .AddEnvironmentVariables()
                .Build();

            var services = new ServiceCollection()
                .Configure<Secrets>(Configuration.GetSection(nameof(Secrets)))
                .AddOptions()
                .BuildServiceProvider();
            */
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var connectionString = config["NorthwindConnectionString"];

            QueriesWithADONET(connectionString);
            QueriesWithEntityFramework();
        }

        static void QueriesWithADONET(string connectionString)
        {
            // DbConnectionStringBuilder connectionStringBuilder = new DbConnectionStringBuilder();  // Generic connector

            // Build connection string for SQL database
            /*
            var connectionString = new SqlConnectionStringBuilder()  // System.Data.SqlClient, not Microsoft.Data.SqlClient
            {
                DataSource = "example.com",
                InitialCatalog = "Northwind",
                UserID = "user",
                Password = "password",
                IntegratedSecurity = false,  // Integrated user (gets Windows login credentials, or Active Directory, who knows)
                TrustServerCertificate = false,
                Encrypt = false
            };
            */

            var sqlConnectionString = new SqlConnectionStringBuilder(connectionString);  // System.Data.SqlClient, not Microsoft.Data.SqlClient

            Console.WriteLine($"Connection string: {sqlConnectionString.ToString()}");

            // var connection = new SqlConnection(connectionString.ToString());
            var connection = new SqlConnection()
            {
                ConnectionString = sqlConnectionString.ToString()
            };

            Console.WriteLine($"Connection status: {connection.State.ToString()}");

            connection.Open();

            Console.WriteLine($"Connection status: {connection.State.ToString()}");

            // var command = connection.CreateCommand();  // IntelliSense, idk
            var command = new SqlCommand()
            {
                Connection = connection,
                CommandText = "SELECT * FROM dbo.Customers"
            };

            // SqlDataReader reader = command.ExecuteReader();
            var reader = command.ExecuteReader();  // SELECT
            // var nonQuery = command.ExecuteNonQuery();  // INSERT, UPDATE, DELETE

            if (!reader.HasRows)
            {
                Console.WriteLine("Registries not found!");
            }
            else
            {
                while (reader.Read() == true)
                {
                    Console.WriteLine($"#{reader["CustomerID"]} - {reader.GetValue(1)}");
                }
            }

            // Close reader (responsible for query retrieval)
            reader.Close();
            
            // Dispose (clean-up) command (query sentence)
            command.Dispose();

            // Close connection and dispose (clean-up)
            connection.Close();
            connection.Dispose();
        }
    
        static void QueriesWithEntityFramework()
        {
            // Intantiate context class
            var dbo = new NorthwindModel();


            // SELECT * FROM dbo.Customers
            var clients = dbo.Customers
                .ToList();

            var clients2 = from c in dbo.Customers
                           select c;

            foreach ( var c in clients )
            {
                Console.WriteLine($"#{c.CustomerID} - {c.CompanyName}");
            }


            // INSERT
            Console.Write("Insert Customer ID: ");
            var customerId = Console.ReadLine();

            var customer = new Customer()
            {
                CustomerID = customerId,
                CompanyName = "Empresa Inventada SAU",
                ContactName = "Manuel Martín",
                ContactTitle = "Empleado",
                Address = "Calle Mario Bros, 64",
                Region = "C-LM",
                City = "Cazalegas",
                PostalCode = "45683",
                Country = "Spain",
                Phone = "925925925",
                Fax = "925925925"
            };

            dbo.Customers.Add(customer);
            dbo.SaveChanges();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


            // UPDATE
            customer = dbo.Customers
                .Where(r => r.CustomerID.Equals("MMM61"))
                .FirstOrDefault();

            customer.CompanyName = "Empresa Aún Más Inventada S.L.";
            customer.ContactName = "Manu Tenorio (el de OT)";

            dbo.Customers.Update(customer);
            dbo.SaveChanges();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            

            // DELETE
            customer = dbo.Customers
                .Where(r => r.CustomerID == "MMM61")
                .FirstOrDefault();

            dbo.Customers.Remove(customer);
            dbo.SaveChanges();

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();


            // SELECT * FROM dbo.Customers
            var newClients = dbo.Customers
                .ToList();

            foreach (var c in newClients)
            {
                Console.WriteLine($"#{c.CustomerID} - {c.CompanyName}");
            }
        }
    }
}