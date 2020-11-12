using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excepciones;
using Archivos;

namespace Entidades
{
     public class Venta
    {
        double ticketNro;
        List<Producto> items;
        double montoTotal;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Venta()
        {
            ticketNro = 0;
            items = new List<Producto>();
            montoTotal = 0;
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Getter / Setter numero de ticket
        /// </summary>
        public double TicketNro { get => ticketNro; set => ticketNro = value; }

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
                
            } else
            {
                throw new VentasException("No se pudo ingresar producto a la lista. Verifique ID o stock disponible");
            }

            return venta;
        }

        /// <summary>
        /// imprime un ticket en formato txt
        /// </summary>
        /// <param name="venta"></param>
        /// <returns>true si imprimio el ticket, caso contrario false</returns>
        public static bool PrintTicket(Venta venta)
        {
            string ticket = DateTime.Now.ToString("ddMMyyHHmmss");
            string path = AppDomain.CurrentDomain.BaseDirectory + ticket;
            double.TryParse(ticket, out venta.ticketNro);

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
        #endregion
    }
}
