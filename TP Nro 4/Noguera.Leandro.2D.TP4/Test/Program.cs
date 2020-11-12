using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archivos;
using Entidades;
using Excepciones;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Noguera.Leandro.2D.TP4";
            Inventario inventario = new Inventario();
            ProductoPerecedero p1 = new ProductoPerecedero("Leche", 1, 62, 30, Producto.ETipo.perecedero);
            ProductoPerecedero p2 = new ProductoPerecedero("Yogur", 2, 78, 30, Producto.ETipo.perecedero);
            ProductoNoPerecedero p3 = new ProductoNoPerecedero("Galletitas", 3, 28, 30, Producto.ETipo.noPerecedero);
            ProductoNoPerecedero p4 = new ProductoNoPerecedero("Harina", 4, 28, 30, Producto.ETipo.noPerecedero);
            Venta venta = new Venta();

            try
            {
                Console.WriteLine("\nSE CARGAN PRODUCTOS:");
                Console.WriteLine();
                inventario += p1;
                inventario += p2;
                inventario += p3;
                inventario += p4;
            }
            catch (ProductosException e)
            {

                Console.WriteLine(e.Message);

            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            try
            {
                Console.WriteLine("\nSE MUESTRAN LOS PRODUCTOS CARGADOS A LA BASE DE DATOS:");
                Console.WriteLine();
                Console.WriteLine(Inventario.Mostrar());
            }
            catch (ArchivosException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException.Message);
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            try
            {
                Console.WriteLine("\nSE INTENTA CARGAR NUEVAMENTE p1:");
                Console.WriteLine();

                inventario += p1;

            }
            catch (ProductosException e)
            {

                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            try
            {
                Console.WriteLine("\nSE AGREGAN ARTICULOS A LA VENTA:");
                Console.WriteLine();

                venta += 1;
                venta += 2;
                venta += 3;

            }
            catch (VentasException e)
            {

                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            try
            {
                Console.WriteLine("\nSE INTENTA CARGAR UN ID ERRONEO: ");
                Console.WriteLine();

                venta += 18;

            }
            catch (VentasException e)
            {

                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            try
            {
                Console.WriteLine("\nSE CIERRA LA VENTA (Se guarda en el listado de ventas de inventario e imprime un ticket en txt):");
                Console.WriteLine();

                inventario += venta;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);

            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            try
            {
                Console.WriteLine("\nSE IMPRIME EL LISTADO DE PRODUCTOS PARA VERIFICAR QUE BAJO EL STOCK DE LOS PRODUCTOS VENDIDOS: ");
                Console.WriteLine();

                Console.WriteLine(Inventario.Mostrar());
            }
            catch (ArchivosException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException.Message);
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();

            try
            {
                Console.WriteLine("\nSE GUARDA EL INVENTARIO EN UN ARCHIVO XML");
                Console.WriteLine();
                if (Inventario.Guardar(inventario))
                    Console.WriteLine("Guardado con exito");

            }
            catch (ArchivosException e)
            {

                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException.Message);
            }

            Console.WriteLine("Presione una tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
