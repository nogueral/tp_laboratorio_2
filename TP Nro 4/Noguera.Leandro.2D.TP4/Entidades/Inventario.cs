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
    public class Inventario
    {
        List<Venta> ventas;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Inventario()
        {
            Ventas = new List<Venta>();
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

                return productos = ProductoDAO.Leer();
            }
        }

        /// <summary>
        /// Getter / Setter lista de ventas
        /// </summary>
        public List<Venta> Ventas { get => ventas; set => ventas = value; }
        #endregion

        #region Sobrecargas
        /// <summary>
        /// Verifica si una venta ya esta cargada a la lista de ventas
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="v"></param>
        /// <returns>true si esta cargada, false caso contrario</returns>
        public static bool operator ==(Inventario inv, Venta v)
        {
            foreach (Venta item in inv.ventas)
            {
                if(item.TicketNro == v.TicketNro)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifica si una venta no esta cargada a la lista de ventas
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="v"></param>
        /// <returns>true si no esta cargada, false caso contrario</returns>
        public static bool operator !=(Inventario inv, Venta v)
        {
            return !(inv == v);
        }

        /// <summary>
        /// Carga una nueva venta a la lista. Descuenta del stock los productos comprados e imprime el ticket de compra en formato txt
        /// </summary>
        /// <param name="inv"></param>
        /// <param name="v"></param>
        /// <returns>objeto inventario con la nueva venta agregada a la lista</returns>
        public static Inventario operator +(Inventario inv, Venta v)
        {
            if(inv != null && v != null && inv != v)
            {
                Producto prod;

                for (int i = 0; i < v.Items.Count; i++)
                {
                    for (int j = 0; j < Inventario.Productos.Count; j++)
                    {
                        if (v.Items[i].Id == Inventario.Productos[j].Id)
                        {
                            prod = Inventario.Productos[j];
                            prod.Cantidad -= 1;
                            v.MontoTotal += Inventario.Productos[j].Precio;
                            prod.Modificar();
                            break;
                        }
                    }
                } 

                Venta.PrintTicket(v);
                inv.Ventas.Add(v);
            }
            else
            {
                throw new VentasException("Esta venta ya se encuentra cerrada");
            }

            return inv;
        }


        #endregion

        #region Metodos
        /// <summary>
        /// Guarda un objeto de tipo inventario en archivo XML
        /// </summary>
        /// <param name="inv"></param>
        /// <returns>true si lo guardo, false caso contrario</returns>
        public static bool Guardar(Inventario inv)
        {
            string path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "inventario.xml");
            Xml<Inventario> auxUni = new Xml<Inventario>();

            return auxUni.Guardar(path, inv);
        }

        /// <summary>
        /// Lee un objeto de tipo inventario guardado en un archivo XML
        /// </summary>
        /// <returns>el objeto de tipo inventario con los datos cargados</returns>
        public static Inventario Leer()
        {
            Inventario datos = new Inventario();
            string path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "inventario.xml");
            Xml<Inventario> inv = new Xml<Inventario>();

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

        public static Inventario PuntoVenta1(Inventario inv)
        {
            Venta venta1 = new Venta();
            venta1 += 1;
            venta1 += 2;
            venta1 += 3;
            inv += venta1;
            Console.WriteLine($"Se carga venta desde {Thread.CurrentThread.Name}");

        }
        #endregion
    }
}
