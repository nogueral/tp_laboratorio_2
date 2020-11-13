using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            ProductoPerecedero p1 = new ProductoPerecedero("Leche", 1, 62.40f, 30, Producto.ETipo.perecedero);
            ProductoPerecedero p2 = new ProductoPerecedero("Yogur", 2, 78.66f, 30, Producto.ETipo.perecedero);
            ProductoNoPerecedero p3 = new ProductoNoPerecedero("Galletitas", 3, 28.33f, 30, Producto.ETipo.noPerecedero);
            ProductoNoPerecedero p4 = new ProductoNoPerecedero("Harina", 4, 28.44f, 30, Producto.ETipo.noPerecedero);
            Venta venta = new Venta();
            Thread hilo1 = new Thread(Inventario.PuntoVenta1);
            Thread hilo2 = new Thread(Inventario.PuntoVenta2);

            try
            {
                Console.WriteLine("\nSE CARGAN PRODUCTOS:");
                Console.WriteLine();

                if(Inventario.Productos+p1)
                    Console.WriteLine($"Producto: {p1.Descripcion} cargado con exito");
                if (Inventario.Productos + p2)
                    Console.WriteLine($"Producto: {p2.Descripcion} cargado con exito");
                if (Inventario.Productos + p3)
                    Console.WriteLine($"Producto: {p3.Descripcion} cargado con exito");
                if (Inventario.Productos + p4)
                    Console.WriteLine($"Producto: {p4.Descripcion} cargado con exito");
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

                if (Inventario.Productos + p1)
                    Console.WriteLine($"Producto: {p1.Descripcion} cargado con exito");

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

                if (Inventario.Ventas+venta)
                    Console.WriteLine($"Venta guardada con exito. Ticket Nro: {venta.TicketNro}");

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
                hilo1.Name = "Punto de venta 1";
                hilo2.Name = "Punto de venta 2";
                Console.WriteLine("SE GENERAN VENTAS DESDE DOS HILOS DIFERENTES");
                hilo1.Start();
                hilo2.Start();
                Thread.Sleep(10000);

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
                Console.WriteLine("\nSE GUARDA EL LISTADO DE VENTAS EN UN ARCHIVO XML");
                Console.WriteLine();
                if (Inventario.Guardar(Inventario.Ventas))
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
