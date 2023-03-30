using System;
using System.Linq;

namespace Exercises
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            // BasicLinqQueries();
            Exercises();
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


            // T-SQL: SELECT TOP 1 * FROM dbo.ListaClientes WHERE Id = 4;

            // Linq method:
            var cliente1 = DataLists.ListaClientes
                .FirstOrDefault();  // First() may throw an exception if nothing matches. This method defaults to null

            // Linq expression
            var cliente2 = (from r in DataLists.ListaClientes
                            select r).FirstOrDefault();


            // Pagination done on DBMS
            var lista1 = DataLists.ListaProductos
                .OrderBy(r => r.Descripcion)
                .Skip(5)
                .Take(5)
                .Select(r => r);

            // Pagination done on client
            var lista2 = DataLists.ListaProductos
                .OrderBy(r => r.Descripcion)
                .Select(r => r)
                .Skip(5)
                .Take(5);


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

        static void Exercises()
        {
            var now = DateTime.Now;
            var date = DateTime.Now.AddDays(-30);

            // var s1 = (now - date).TotalDays;
            // var s2 = now.Subtract(date).TotalDays;

            var r1 = now.Subtract(date);

            // Clients list greater than 40 years old
            var fortyYearsAgo = now.AddYears(-40);

            var q1 = DataLists.ListaClientes
                .Where(r => r.FechaNac < fortyYearsAgo)
                .Select(r => r);

            foreach (var item in q1)
            {
                Console.WriteLine($"#{item.Id} - {item.Nombre} - {item.FechaNac}");
            }

            Console.WriteLine(string.Empty);

            // Products list which name starts by C ordered by price
            var q2 = DataLists.ListaProductos
                .Where(r => r.Descripcion.ToLower().StartsWith("c"))
                .OrderBy(r => r.Precio)
                .Select(r => r);

            foreach (var item in q2)
            {
                Console.WriteLine($"#{item.Id} - {item.Descripcion} - {item.Precio} euros");
            }

            Console.WriteLine(string.Empty);

            // Ask for order id and list content
            Console.Write("Insert OrderId: ");
            int.TryParse(Console.ReadLine(), out var orderId);

            /*
            var q3 = DataLists.ListaLineasPedido
                .Where(o => o.IdPedido.Equals(orderId))
                .Join(DataLists.ListaProductos,
                    o => o.IdPedido,
                    p => p.Id,
                    (o, p) => new {  })
                .Select(r => r);
            */

            var q3 = from lp in DataLists.ListaLineasPedido
                     where lp.IdPedido == orderId
                     join p in DataLists.ListaProductos on lp.IdProducto equals p.Id
                     select new
                     {
                         Id = lp.IdPedido,
                         IdProducto = lp.IdProducto,
                         Descripcion = p.Descripcion,
                         Precio = p.Precio,
                         Cantidad = lp.Cantidad,
                         Subtotal = p.Precio * lp.Cantidad
                     };

            var q3b = DataLists.ListaLineasPedido
                .Where(r => r.IdPedido == orderId)
                .Select(r => new
                {
                    IdProducto = r.IdProducto,
                    Descripcion = DataLists.ListaProductos
                        .Where(s => s.Id == r.IdProducto)
                        .Select(s => s.Descripcion)
                        .FirstOrDefault(),
                    Precio = DataLists.ListaProductos
                        .Where(s => s.Id == r.IdProducto)
                        .Select(s => s.Precio)
                        .FirstOrDefault(),
                    Cantidad = r.Cantidad
                    // Subtotal missing ¿another sub-select? I prefer joining
                });

            var q3c = from r in DataLists.ListaLineasPedido
                      where r.IdPedido == orderId
                      select new
                      {
                          r.IdProducto,
                          Descripcion = (from s in DataLists.ListaProductos
                                         where s.Id == r.IdProducto
                                         select s.Descripcion).FirstOrDefault(),
                          Precio = (from s in DataLists.ListaProductos
                                    where s.Id == r.IdProducto
                                    select s.Precio).FirstOrDefault(),
                          r.Cantidad
                      };

            foreach (var item in q3)
            {
                Console.WriteLine($"Pedido #{item.Id} - Producto #{item.IdProducto} - {item.Descripcion} - {item.Cantidad} x {item.Precio} = {item.Subtotal}");
            }

            Console.WriteLine(string.Empty);

            // Ask for order id and print total
            Console.Write("Insert OrderId: ");
            int.TryParse(Console.ReadLine(), out orderId);

            var q4 = (from lp in DataLists.ListaLineasPedido
                      where lp.IdPedido == orderId
                      join p in DataLists.ListaProductos on lp.IdProducto equals p.Id
                      select p.Precio * lp.Cantidad).Sum();


            var q4b = DataLists.ListaLineasPedido
                .Where(lp => lp.IdPedido == orderId)
                .Sum(lp => lp.Cantidad * DataLists.ListaProductos
                    .Where(s => s.Id == lp.IdProducto)
                    .Select(s => s.Precio)
                    .FirstOrDefault());


            Console.WriteLine($"Pedido #{orderId} - Total: {q4} euros");

            Console.WriteLine(string.Empty);

            // Orders list that contains "lapicero" (supposedly id #11)
            /*
            var q5 = from p in DataLists.ListaProductos
                     where p.Descripcion.ToLower().Contains("lapicero")
                     join lp in DataLists.ListaLineasPedido on p.Id equals lp.IdProducto
                     join o in DataLists.ListaPedidos on lp.IdPedido equals o.Id
                     select new
                     {
                         Id = o.Id,
                         FechaPedido = o.FechaPedido,
                         Cantidad = lp.Cantidad
                     };
            */
            var q5 = from o in DataLists.ListaPedidos
                     join lp in DataLists.ListaLineasPedido on o.Id equals lp.IdPedido
                     join p in DataLists.ListaProductos on lp.IdPedido equals p.Id
                     where p.Descripcion.ToLower().Contains("lapicero")
                     select new
                     {
                         Id = o.Id,
                         FechaPedido = o.FechaPedido,
                         Cantidad = lp.Cantidad
                     };

            Console.WriteLine("Pedidos que contienen \"lapicero\":");
            foreach (var item in q5)
            {
                Console.WriteLine($"Pedido #{item.Id} - Fecha: {item.FechaPedido} - Cantidad: {item.Cantidad}");
            }

            Console.WriteLine(string.Empty);

            // Amount (count) of orders that contains "cuaderno grande"
            /*
            var q6 = (from p in DataLists.ListaProductos
                      where p.Descripcion.ToLower().Contains("cuaderno grande")
                      join lp in DataLists.ListaLineasPedido on p.Id equals lp.IdProducto
                      select lp.IdPedido).Distinct().Sum();
            */

            var q6 = (from lp in DataLists.ListaLineasPedido
                      join p in DataLists.ListaProductos on lp.IdProducto equals p.Id
                      where p.Descripcion.ToLower().Contains("cuaderno grande")
                      select lp.IdPedido).Distinct().Sum();

            Console.WriteLine($"Total de pedidos que contienen \"cuaderno grande\": {q6}");

            Console.WriteLine(string.Empty);

            // Total units sold of "cuaderno pequeño"
            /*
            var q7 = (from p in DataLists.ListaProductos
                      where p.Descripcion.ToLower().Contains("cuaderno pequeño")
                      join lp in DataLists.ListaLineasPedido on p.Id equals lp.IdProducto
                      select lp.Cantidad).Sum();
            */

            var q7 = (from lp in DataLists.ListaLineasPedido
                      join p in DataLists.ListaProductos on lp.IdProducto equals p.Id
                      where p.Descripcion.ToLower().Contains("cuaderno pequeño")
                      select lp.Cantidad).Sum();

            Console.WriteLine($"Total de pedidos que contienen \"cuaderno pequeño\": {q7}");

            Console.WriteLine(string.Empty);

            // Order with the biggest amount of items/products
            var q8 = (from lp in DataLists.ListaLineasPedido
                      group lp by lp.IdPedido into p
                      orderby p.Sum(lp => lp.Cantidad) descending
                      select new
                      {
                          IdPedido = p.Key,
                          CantitadTotal = p.Sum(lp => lp.Cantidad)
                      }).FirstOrDefault();  // .OrderByDescending(r => r.CantitadTotal).FirstOrDefault();

            var q8b = DataLists.ListaLineasPedido
                .GroupBy(lp => lp.IdPedido)
                .Select(lp => new
                {
                    IdPedido = lp.Key,
                    CantitadTotal = lp.Sum(lp => lp.Cantidad)
                })
                .OrderByDescending(lp => lp.CantitadTotal)
                .FirstOrDefault();

            Console.WriteLine($"Pedido con la mayor cantidad de items: #{q8.IdPedido} - Total items: {q8.CantitadTotal}");

            Console.WriteLine(string.Empty);

            // Orders list ordered by date
            var q9 = from o in DataLists.ListaPedidos
                     orderby o.FechaPedido descending
                     select o;

            foreach (var item in q9)
            {
                Console.WriteLine($"Pedido #{item.Id} - Fecha: {item.FechaPedido}");
            }

        }
    }
}