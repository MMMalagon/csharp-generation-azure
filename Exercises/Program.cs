using System;
using System.Linq;

namespace Exercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            BasicLinqQueries();
        }

        static void BasicLinqQueries()
        {
            // T-SQL: SELECT * FROM dbo.ListaProductos;

            // Linq method:
            var r1a = DataLists.ListaProductos
                .Select(r => r);

            // Linq expression:
            var r1b = from r in DataLists.ListaProductos
                      select r;

            foreach (Producto item in r1a)
            {
                Console.WriteLine($"#{item.Id} - {item.Descripcion}");
            }


            // T-SQL: SELECT * FROM dbo.ListaClientes;

            // Linq method:
            var r2a = DataLists.ListaClientes
                .Select(r => r);

            // Linq expression:
            var r2b = from r in DataLists.ListaClientes
                      select r;

            foreach (Cliente item in r2a)
            {
                Console.WriteLine($"#{item.Id} - {item.Nombre}");
            }


            // T-SQL: SELECT Nombre FROM dbo.ListaClientes;

            // Linq method:
            var r3a = DataLists.ListaClientes
                .Select(r => r.Nombre);

            // Linq expression:
            var r3b = from r in DataLists.ListaClientes
                      select r.Nombre;

            foreach (var item in r3a)  // string
            {
                Console.WriteLine(item);
            }


            // T-SQL: SELECT Id, Nombre FROM dbo.ListaClientes;

            // Linq method:
            var r4a = DataLists.ListaClientes
                .Select(r => new { r.Id, r.Nombre });

            // Linq expression:
            var r4b = from r in DataLists.ListaClientes
                      select new { r.Id, r.Nombre };

            foreach (var item in r4a)  // AnonymousType
            {
                Console.WriteLine($"#{item.Id} - {item.Nombre}");
            }


            // T-SQL: SELECT Id AS Code, Nombre AS NombreCompleto FROM dbo.ListaClientes;

            // Linq method:
            var r5a = DataLists.ListaClientes
                .Select(r => new { Code = r.Id, NombreCompleto = r.Nombre });

            // Linq expression:
            var r5b = from r in DataLists.ListaClientes
                      select new { Code = r.Id, NombreCompleto = r.Nombre };

            foreach (var item in r5a)  // AnonymousType
            {
                Console.WriteLine($"#{item.Code} - {item.NombreCompleto}");
            }


            // T-SQL: SELECT Descripcion FROM dbo.ListaProductos WHERE Precio < 0.90;

            // Linq method:
            var r6a = DataLists.ListaProductos
                .Where(r => r.Precio < 0.90)
                .Select(r => r.Descripcion);

            // Linq expression:
            var r6b = from r in DataLists.ListaProductos
                      where r.Precio < 0.90
                      select r.Descripcion;

            foreach (var item in r6a)  // AnonymousType
            {
                Console.WriteLine(item);
            }


            // T-SQL: SELECT Descripcion FROM dbo.ListaProductos WHERE Precio < 0.90 ORDER BY Descripcion;

            // Linq method:
            var r7a = DataLists.ListaProductos
                .Where(r => r.Precio < 0.90)
                .OrderBy(r => r.Descripcion)
                .Select(r => r.Descripcion);

            // Linq expression:
            var r7b = from r in DataLists.ListaProductos
                      where r.Precio < 0.90
                      orderby r.Descripcion
                      select r.Descripcion;

            foreach (var item in r7a)  // AnonymousType
            {
                Console.WriteLine(item);
            }


            // T-SQL: SELECT Descripcion FROM dbo.ListaProductos WHERE Precio < 0.90 ORDER BY Descripcion DESC;

            // Linq method:
            var r8a = DataLists.ListaProductos
                .Where(r => r.Precio < 0.90)
                .OrderByDescending(r => r.Descripcion)
                .Select(r => r.Descripcion);

            // Linq expression:
            var r8b = from r in DataLists.ListaProductos
                      where r.Precio < 0.90
                      orderby r.Descripcion descending
                      select r.Descripcion;

            foreach (var item in r8a)  // AnonymousType
            {
                Console.WriteLine(item);
            }

            // Be careful with the filtering order using Linq methods (expressions don't allow
            // it, at least not directly).
            //
            // It's important to mention this won't be ordered on DBMS but on client.
            //
            // The reason behind that is the selection is already done, so the retrieved data
            // isn't ordered at all, and the client (the machine executing this code) will be
            // in charge of sorting the data.
            var r8aa = DataLists.ListaProductos
                .Where(r => r.Precio < 0.90)
                .Select(r => r.Descripcion)
                .OrderByDescending(r => r);  // Description is already selected, so be aware which vars are selected

            // This example that uses Linq expressions is the same as above but it's more
            // noticeable what's happening as the query doesn't have the ORDER BY filtering
            // defined but outside as a (Linq) method.
            var r8ba = (from r in DataLists.ListaProductos
                        where r.Precio < 0.90
                        select r.Descripcion).OrderByDescending(r => r);


            // T-SQL: SELECT Descripcion FROM dbo.ListaProductos WHERE Descripcion Contains(Descripcion, "boli");

            // Linq method:
            var r9a = DataLists.ListaProductos
                .Where(r => r.Descripcion.ToLower().Contains("boli"))
                .Select(r => r.Descripcion);

            // Linq expression:
            var r9b = from r in DataLists.ListaProductos
                      where r.Descripcion.ToLower().Contains("boli")
                      select r.Descripcion;

            foreach (var item in r8a)  // AnonymousType
            {
                Console.WriteLine(item);
            }


            // Count -> Count elements
            // Distinct -> Different values
            // Max -> Max value
            // Min -> Min value
            // Sum -> Sum values
            // Average -> Average/mean values

            // T-SQL: SELECT Count(*) FROM dbo.ListaProductos WHERE Precio < 0.90;

            var r10a = DataLists.ListaProductos
                .Where(r => r.Precio < 0.90)
                .Count();

            var r10b = DataLists.ListaProductos
                .Count(r => r.Precio < 0.90);

            var r10c = (from r in DataLists.ListaProductos
                        where r.Precio < 0.90
                        select r).Count();  // I don't know why there's no count keyword, but ok
        }
    }
}