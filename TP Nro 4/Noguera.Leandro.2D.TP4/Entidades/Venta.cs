using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using Archivos;
using System.Threading;

namespace Entidades
{
    public delegate bool DelegadoVentas(Venta venta);

     public class Venta
    {
        string ticketNro;
        List<Producto> items;
        double montoTotal;
        public static event DelegadoVentas delVentas;

        #region Constructor

        static Venta()
        {
            delVentas += CalcularMontoTotal;
            delVentas += Inventario.ModificarStock;
            delVentas += PrintTicket;
            delVentas += Inventario.CargarVenta;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        public Venta()
        {
            ticketNro = "0";
            items = new List<Producto>();
            montoTotal = 0;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Getter / Setter numero de ticket
        /// </summary>
        public string TicketNro { get => ticketNro; set => ticketNro = value; }

        /// <summary>
        /// Getter / Setter lista de productos
        /// </summary>
        public List<Producto> Items { get => items; set => items = value; }

        /// <summary>
        /// Getter / Setter monto total a abonar
        /// </summary>
        public double MontoTotal { get => montoTotal; set => montoTotal = value; }
        #endregion

        #region Metodos

        /// <summary>
        /// imprime un ticket en formato txt
        /// </summary>
        /// <param name="venta"></param>
        /// <returns>true si imprimio el ticket, caso contrario false</returns>
        public static bool PrintTicket(Venta venta)
        {
            string ticket = DateTime.Now.ToString("ddMMyyHHmmss");
            string path;

            venta.ticketNro = ticket;

            if (Thread.CurrentThread.Name == "Punto de venta 2")
            {
                venta.ticketNro += "_02";

            } else
            {
                venta.ticketNro += "_01";
                
            }

            path = AppDomain.CurrentDomain.BaseDirectory + venta.ticketNro;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Ticket Nro: {venta.ticketNro}");
            foreach (Producto item in venta.items)
            {
                sb.AppendLine(String.Format("Descripcion: {0} Precio: ${1: #,###.00}", item.Descripcion,item.Precio));
            }

            sb.AppendLine(String.Format("Monto total: ${0: #,###.00}", venta.montoTotal));
            sb.AppendLine("<-------------------------------------------->");

            Texto texto = new Texto();

            return texto.Guardar(path, sb.ToString());
        }

        /// <summary>
        /// lee un ticket guardado en formato txt
        /// </summary>
        /// <param name="path"></param>
        /// <returns>los datos guardados</returns>
        public static string Leer(string path)
        {
            string datos;
            Texto auxTexto = new Texto();

            auxTexto.Leer(path, out datos);

            return datos;
        }

        /// <summary>
        /// Calcula y asigna el valor del monto total de una venta
        /// </summary>
        /// <param name="venta"></param>
        /// <returns>true si se se efectua la carga, false caso contrario</returns>
        public static bool CalcularMontoTotal(Venta venta)
        {
            if (venta != null)
            {
                for (int i = 0; i < venta.Items.Count; i++)
                {
                    for (int j = 0; j < Inventario.Productos.Count; j++)
                    {
                        if (venta.Items[i].Id == Inventario.Productos[j].Id)
                        {
                            venta.MontoTotal += Inventario.Productos[j].Precio;
                            break;
                        }
                    }
                }

                return true;
            }

            return false;
        }

        #endregion

        #region Sobrecargas

        /// <summary>
        /// Verifica a traves del id indicado si el producto existe en la base de datos
        /// </summary>
        /// <param name="venta"></param>
        /// <param name="id"></param>
        /// <returns>el producto solicitado, si no existe lo devuelve en null</returns>
        public static Producto operator ==(Venta venta, int id)
        {
            Producto producto = null;

            foreach (var item in Inventario.Productos)
            {
                if (item.Id == id)
                {
                    producto = item;
                    break;
                }
            }

            return producto;
        }

        /// <summary>
        /// retorna el primer producto que no coincida con el id indicado
        /// </summary>
        /// <param name="venta"></param>
        /// <param name="id"></param>
        /// <returns>objeto de tipo producto</returns>
        public static Producto operator !=(Venta venta, int id)
        {
            Producto producto = null;

            foreach (var item in Inventario.Productos)
            {
                if (item.Id != id)
                {
                    producto = item;
                    break;
                }
            }

            return producto;
        }

        /// <summary>
        /// agrega un nuevo producto a la venta. verifica previamente que exista stock suficiente.
        /// </summary>
        /// <param name="venta"></param>
        /// <param name="id"></param>
        /// <returns>objeto venta con el producto cargado. si no puede cargados lanza VentasException</returns>
        public static Venta operator +(Venta venta, int id)
        {
            Producto producto = (venta == id);

            if (producto != null && producto.Cantidad > 0)
            {
                producto.Cantidad = 1;
                venta.items.Add(producto);

            }
            else
            {
                throw new VentasException("No se pudo ingresar producto a la lista. Verifique ID o stock disponible");
            }

            return venta;
        }

        /// <summary>
        /// Verifica si una venta ya esta cargada a la lista de ventas
        /// </summary>
        /// <param name="listaVentas"></param>
        /// <param name="v"></param>
        /// <returns>true si esta cargada, false caso contrario</returns>
        public static bool operator ==(List<Venta> listaVentas, Venta v)
        {
            foreach (Venta item in listaVentas)
            {
                if (item.TicketNro == v.TicketNro)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifica si una venta no esta cargada a la lista de ventas
        /// </summary>
        /// <param name="listaVentas"></param>
        /// <param name="v"></param>
        /// <returns>true si no esta cargada, false caso contrario</returns>
        public static bool operator !=(List<Venta> listaVentas, Venta v)
        {
            return !(listaVentas == v);
        }

        /// <summary>
        /// Carga una nueva venta a la lista. Descuenta del stock los productos comprados e imprime el ticket de compra en formato txt
        /// </summary>
        /// <param name="listaVentas"></param>
        /// <param name="v"></param>
        /// <returns>true si se cargo la venta, false caso contrario</returns>
        public static bool operator +(List<Venta> listaVentas, Venta v)
        {
            if (listaVentas != v)
            {
                delVentas(v);
                
                return true;
            }
            else
            {
                throw new VentasException("Esta venta ya se encuentra cerrada");
            }
        }


        
        #endregion
    }
}
