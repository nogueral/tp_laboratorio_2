using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Archivos;
using Excepciones;

namespace Entidades
{
    public static class Inventario
    {
        static List<Venta> ventas;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        static Inventario()
        {
            ventas = new List<Venta>();
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Retorna la lista de productos cargada en la base de datos
        /// </summary>
        public static List<Producto> Productos
        {
            get
            {
                List<Producto> productos = new List<Producto>();
                ProductoDAO prod = new ProductoDAO();

                return productos = prod.Leer();
            }

        }

        /// <summary>
        /// Getter / Setter lista de ventas
        /// </summary>
        public static List<Venta> Ventas { get => ventas; }
        #endregion


        #region Metodos
        /// <summary>
        /// Guarda un objeto de tipo inventario en archivo XML
        /// </summary>
        /// <param name="inv"></param>
        /// <returns>true si lo guardo, false caso contrario</returns>
        public static bool Guardar(List<Venta> ventas)
        {
            string path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "inventario.xml");
            Xml<List<Venta>> auxUni = new Xml<List<Venta>>();

            return auxUni.Guardar(path, ventas);
        }

        /// <summary>
        /// Lee un objeto de tipo inventario guardado en un archivo XML
        /// </summary>
        /// <returns>el objeto de tipo inventario con los datos cargados</returns>
        public static List<Venta> Leer()
        {
            List<Venta> datos = new List<Venta>();
            string path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "inventario.xml");
            Xml<List<Venta>> inv = new Xml<List<Venta>>();

            inv.Leer(path, out datos);

            return datos;

        }

        /// <summary>
        /// muestra todos los datos cargados en el objeto inventario
        /// </summary>
        /// <returns>los datos cargados</returns>
        public static string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("LISTA DE PRODUCTOS");
            sb.AppendLine("<-------------------------------------------------------------->");

            foreach (Producto item in Inventario.Productos)
            {
                sb.AppendLine(item.ToString());
            }

            sb.AppendLine("<-------------------------------------------------------------->");

            return sb.ToString();
        }

        /// <summary>
        /// Genera un hardcodeo de ventas a traves de un hilo
        /// </summary>
        public static void PuntoVenta1()
        {
            

            try
            {
                Venta v1 = new Venta();
                v1 += 1;
                v1 += 2;
                v1 += 3;
                v1 += 4;
                if (Inventario.Ventas + v1)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Se genero una nueva venta en {Thread.CurrentThread.Name}");
                }

            }
            catch (VentasException e)
            {

                Console.WriteLine(e.Message);
            }

            try
            {
                Venta v2 = new Venta();
                v2 += 1;
                v2 += 2;
                v2 += 3;
                v2 += 4;
                if (Inventario.Ventas + v2)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Se genero una nueva venta en {Thread.CurrentThread.Name}");
                }

            }
            catch (VentasException e)
            {

                Console.WriteLine(e.Message);
            }

            try
            {
                Venta v3 = new Venta();
                v3 += 1;
                v3 += 2;
                v3 += 3;
                v3 += 4;
                if (Inventario.Ventas + v3)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Se genero una nueva venta en {Thread.CurrentThread.Name}");
                }

            }
            catch (VentasException e)
            {

                Console.WriteLine(e.Message);
            }

        }

        /// <summary>
        /// // Genera un hardcodeo de ventas a traves de un hilo
        /// </summary>
        public static void PuntoVenta2()
        {
            

            try
            {
                Venta v1 = new Venta();
                v1 += 1;
                v1 += 2;
                v1 += 3;
                v1 += 4;
                if (Inventario.Ventas + v1)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Se genero una nueva venta en {Thread.CurrentThread.Name}");
                }

            }
            catch (VentasException e)
            {

                Console.WriteLine(e.Message);
            }

            try
            {
                Venta v2 = new Venta();
                v2 += 1;
                v2 += 2;
                v2 += 3;
                v2 += 4;
                if (Inventario.Ventas + v2)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Se genero una nueva venta en {Thread.CurrentThread.Name}");
                }

            }
            catch (VentasException e)
            {

                Console.WriteLine(e.Message);
            }

            try
            {
                Venta v3 = new Venta();
                v3 += 1;
                v3 += 2;
                v3 += 3;
                v3 += 4;
                if (Inventario.Ventas + v3)
                {
                    Thread.Sleep(2000);
                    Console.WriteLine($"Se genero una nueva venta en {Thread.CurrentThread.Name}");
                }

            }
            catch (VentasException e)
            {

                Console.WriteLine(e.Message);
            }
        }
        #endregion
    }
}
