using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace System.Collections.Generic
{
    public static class ListExtension
    {
        /// <summary>
        /// Calcula la cantidad total de productos contenidos en una lista (stock total)
        /// </summary>
        /// <param name="listaProductos"></param>
        /// <returns>el acumulado de la variable cantidad</returns>
        public static int StockTotal(this List<Producto> listaProductos)
        {
            int acumuladorStock = 0;

            foreach (Producto producto in listaProductos)
            {
                acumuladorStock += producto.Cantidad;
            }

            return acumuladorStock;
        }

        /// <summary>
        /// Calcula el monto total de ventas contenidas en un listado de tipo Venta
        /// </summary>
        /// <param name="listaVentas"></param>
        /// <returns>el acumulado de venta</returns>
        public static double TotalVentas(this List<Venta> listaVentas)
        {
            double montoAcumulado = 0;

            foreach (Venta venta in listaVentas)
            {
                montoAcumulado += venta.MontoTotal;
            }

            return montoAcumulado;
        }
    }
}
