using System;
using System.Linq;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NorthwindExercises.Model;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace NorthwindExercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var connectionString = config["ConnectionStrings:Northwind"];

            Exercises(connectionString);
        }

        static void Exercises(string connectionString)
        {
            // Custom DbContextOptions(Builder)
            var optionsBuilder = new DbContextOptionsBuilder<NorthwindModel>();
            optionsBuilder.UseSqlServer(connectionString);

            // Instantiate DbContext class with custom DbContext
            var dbo = new NorthwindModel(optionsBuilder.Options);


            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Clientes que residen en USA");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Customers WHERE Country = 'USA'
            var usaCustomers = from c in dbo.Customers
                               where c.Country.Equals("USA")
                               select c;

            foreach (var customer in usaCustomers)
            {
                Console.WriteLine($"#{customer.CustomerID} - {customer.CompanyName} ({customer.Country})");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Proveedores (Suppliers) de Berlin");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Suppliers WHERE Country = 'Berlin'
            var berlinSuppliers = from s in dbo.Suppliers
                                  where s.City.Equals("Berlin")
                                  select s;

            foreach (var supplier in berlinSuppliers)
            {
                Console.WriteLine($"#{supplier.SupplierID} - {supplier.CompanyName} ({supplier.City} - {supplier.Country})");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Empleados con identificadores 3, 5 y 8");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Employees WHERE EmployeeID IN (3, 5, 8)
            // https://stackoverflow.com/a/1075564
            var employeeIdentifiers = new List<int>() { 3, 5, 8 };
            var employeeSelection = from e in dbo.Employees
                                    where employeeIdentifiers.Contains(e.EmployeeID)
                                    select e;

            foreach (var employee in employeeSelection)
            {
                Console.WriteLine($"#{employee.EmployeeID} - {employee.LastName}, {employee.FirstName}");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Productos con stock mayor de cero");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE UnitsInStock > 0
            var productsInStock = from p in dbo.Products
                                  where p.UnitsInStock > 0
                                  select p;

            foreach (var product in productsInStock)
            {
                Console.WriteLine($"#{product.ProductID} - {product.ProductName} ({product.UnitsInStock} units in stock)");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Productos con stock mayor de cero de los proveedores con identificadores 1, 3 y 5");
            /////////////////////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE SupplierID IN (1, 3, 5) 
            var supplierIdentifiers = new List<int>() { 1, 3, 5 };
            var productsSelection = from p in dbo.Products
                                    where p.SupplierID.HasValue
                                      && supplierIdentifiers.Contains(p.SupplierID.Value)
                                    select p;

            foreach (var product in productsSelection)
            {
                Console.WriteLine($"Supplier #{product.SupplierID} - #{product.ProductID} - {product.ProductName} ({product.UnitsInStock} units in stock)");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Productos con precio mayor de 20 y menor 90");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Products WHERE UnitPrice > 20 AND UnitPrice < 90
            var minPrice = 20;
            var maxPrice = 90;
            var productsBetweenTwoPrices = from p in dbo.Products
                                           where p.UnitPrice > minPrice && p.UnitPrice < maxPrice
                                           select p;

            foreach (var product in productsBetweenTwoPrices)
            {
                Console.WriteLine($"#{product.ProductID} - {product.ProductName} (${product.UnitPrice:F2})");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Pedidos entre 01/01/1997 y 15/07/1997");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE OrderDate >= '1997/01/01' AND OrderDate <= '1997/09/15'
            var minDate = new DateTime(1997, 01, 01);
            var maxDate = new DateTime(1997, 09, 15);
            var ordersBetweenTwoDates = from o in dbo.Orders
                                        where o.OrderDate >= minDate && o.OrderDate <= maxDate
                                        select o;

            foreach (var order in ordersBetweenTwoDates)
            {
                Console.WriteLine($"#{order.OrderID} - {order.OrderDate}");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Pedidos registrados por los empleados con identificador 1, 3, 4 y 8 en 1997");
            /////////////////////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1997 AND EmployeeID IN (1, 3, 4, 8)
            employeeIdentifiers = new List<int>() { 1, 3, 4, 8 };
            var year = 1997;

            var ordersFrom1997AndFromSelectedEmployees = from o in dbo.Orders
                                                         where o.OrderDate.HasValue
                                                           && o.OrderDate.Value.Year == year
                                                           && employeeIdentifiers.Contains(o.EmployeeID.Value)
                                                         select o;

            foreach (var order in ordersFrom1997AndFromSelectedEmployees)
            {
                Console.WriteLine($"#{order.OrderID} - {order.OrderDate} - Employee #{order.EmployeeID}");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Pedidos de abril de 1996");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1996 AND MONTH(OrderDate) = 4
            year = 1996;
            var month = 4;

            var ordersFromApril1996 = from o in dbo.Orders
                                      where o.OrderDate.HasValue
                                        && o.OrderDate.Value.Year == year
                                        && o.OrderDate.Value.Month == month
                                      select o;

            foreach (var order in ordersFromApril1996)
            {
                Console.WriteLine($"#{order.OrderID} - {order.OrderDate}");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Pedidos del realizado los dia uno de cada mes del año 1998");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * dbo.Orders WHERE YEAR(OrderDate) = 1998 AND DAY(OrderDate) = 1
            year = 1998;
            var day = 1;

            var ordersFromFirstDayOfEachMonth1998 = from o in dbo.Orders
                                                    where o.OrderDate.HasValue
                                                      && o.OrderDate.Value.Year == year
                                                      && o.OrderDate.Value.Day == day
                                                    select o;

            foreach (var order in ordersFromFirstDayOfEachMonth1998)
            {
                Console.WriteLine($"#{order.OrderID} - {order.OrderDate}");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Clientes que no tiene fax");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Customers WHERE Fax = NULL
            var customersWithoutFax = from c in dbo.Customers
                                      where c.Fax == null
                                      select c;

            foreach (var customer in customersWithoutFax)
            {
                Console.WriteLine($"#{customer.CustomerID} - {customer.CompanyName} - Fax: {customer.Fax}");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de los 10 productos más baratos");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products ORDER BY UnitPrice
            var topTenCheapestProducts = (from p in dbo.Products
                                          orderby p.UnitPrice
                                          select p).Take(10);

            foreach (var product in topTenCheapestProducts)
            {
                Console.WriteLine($"#{product.ProductID} - {product.ProductName} (${product.UnitPrice:F2})");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de los 10 productos más caros con stock");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products ORDER BY UnitPrice DESC
            var topTenMostExpensiveProductsInStock = (from p in dbo.Products
                                                      where p.UnitsInStock > 0
                                                      orderby p.UnitPrice descending
                                                      select p).Take(10);

            foreach (var product in topTenMostExpensiveProductsInStock)
            {
                Console.WriteLine($"#{product.ProductID} - {product.ProductName} (${product.UnitPrice:F2}) ({product.UnitsInStock} units in stock)");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Cliente de UK y nombre de empresa que comienza por B");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT * FROM dbo.Customers WHERE CompanyName LIKE 'B%' AND Country = 'Uk'
            var customersFromUKWithCompanyNameStartingWithB = from c in dbo.Customers
                                                              where c.Country.Equals("UK")
                                                                && c.CompanyName.StartsWith("B")
                                                              select c;

            foreach (var customer in customersFromUKWithCompanyNameStartingWithB)
            {
                Console.WriteLine($"#{customer.CustomerID} - {customer.CompanyName} ({customer.Country})");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Productos de identificador de categoria 3 y 5");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT TOP(10) * FROM dbo.Products WHERE CategoryID IN (3, 5)
            var categoryIdentifiers = new List<int>() { 3, 5 };
            var topTenProductsFromSelectedCategories = (from p in dbo.Products
                                                        where p.CategoryID.HasValue
                                                          && categoryIdentifiers.Contains(p.CategoryID.Value)
                                                        select p).Take(10);

            foreach (var product in topTenProductsFromSelectedCategories)
            {
                Console.WriteLine($"#{product.ProductID} - {product.ProductName} - Category #{product.CategoryID}");
            }

            Console.WriteLine(string.Empty);

            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Importe total del stock");
            /////////////////////////////////////////////////////////////////////////////////

            // SELECT SUM(UnitInStock * UnitPrice) FROM Products
            var totalStockValue = (from p in dbo.Products
                                   select p.UnitsInStock * p.UnitPrice).Sum();
            // var totalStockValue = dbo.Products.Sum(p => p.UnitsInStock * p.UnitPrice);

            Console.WriteLine($"{totalStockValue:F2}");

            Console.WriteLine(string.Empty);


            /////////////////////////////////////////////////////////////////////////////////
            Console.WriteLine("Listado de Pedidos de los clientes de Argentina");
            /////////////////////////////////////////////////////////////////////////////////            

            // SELECT CustomerID FROM dbo.Customers WHERE Country = 'Argentina'
            // SELECT * FROM dbo.Orders WHERE CustomerID IN ('CACTU', 'OCEAN', 'RANCH')

            Console.WriteLine("Opción 1: SELECTs independientes");

            var argentinianCustomers = from c in dbo.Customers
                                       where c.Country.Equals("Argentina")
                                       select c.CustomerID;
            /*
            var customerIdentifiers = new List<string>() { "CACTU", "OCEAN", "RANCH" };
            var ordersFromSelectedCustomer = from o in dbo.Orders
                                             where customerIdentifiers.Contains(o.CustomerID)
                                             select o;
             */
            var ordersFromArgentinianCustomers = from o in dbo.Orders
                                                 where argentinianCustomers.Contains(o.CustomerID)
                                                 select o;

            foreach (var order in ordersFromArgentinianCustomers)
            {
                Console.WriteLine($"#{order.OrderID} - Category #{order.CustomerID}");
            }

            Console.WriteLine(string.Empty);

            // SELECT * FROM dbo.Orders WHERE CustomerID IN (SELECT CustomerID FROM dbo.Customers WHERE Country = 'Argentina')

            Console.WriteLine("Opción 2: SELECTs anidados");

            ordersFromArgentinianCustomers = from o in dbo.Orders
                                             where (from c in dbo.Customers
                                                    where c.Country == "Argentina"
                                                    select c.CustomerID).Contains(o.CustomerID)
                                             select o;

            foreach (var order in ordersFromArgentinianCustomers)
            {
                Console.WriteLine($"#{order.OrderID} - Category #{order.CustomerID}");
            }

            Console.WriteLine(string.Empty);

            // SELECT o.* FROM dbo.Orders AS o JOIN dbo.Customers AS c ON o.CustomerID = c.CustomerID WHERE c.Country = 'Argentina'

            Console.WriteLine("Opción 3: JOIN de tablas");

            ordersFromArgentinianCustomers = from o in dbo.Orders
                                             join c in dbo.Customers on o.CustomerID equals c.CustomerID
                                             where c.Country.Equals("Argentina")
                                             select o;

            foreach (var order in ordersFromArgentinianCustomers)
            {
                Console.WriteLine($"#{order.OrderID} - Category #{order.CustomerID}");
            }

            Console.WriteLine(string.Empty);
        }
    }
}