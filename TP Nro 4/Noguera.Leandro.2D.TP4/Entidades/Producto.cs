using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Excepciones;
using Archivos;

namespace Entidades
{
    [Serializable]
    [XmlInclude(typeof(ProductoNoPerecedero))]
    [XmlInclude(typeof(ProductoPerecedero))]
    public abstract class Producto
    {
        string descripcion;
        int idProducto;
        double precio;
        int cantidad;
        ETipo tipoProducto;

        public enum ETipo
        {
            perecedero,
            noPerecedero,
        }

        #region Constructores
        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Producto()
        {


        }
        /// <summary>
        /// Constructor de objetos de tipo Producto
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="idProducto"></param>
        /// <param name="precio"></param>
        /// <param name="cantidad"></param>
        /// <param name="tipoProducto"></param>
        public Producto(string descripcion, int idProducto, double precio, int cantidad, ETipo tipoProducto) : this()
        {
            this.Descripcion = descripcion;
            this.Id = idProducto;
            this.Precio = precio;
            this.Cantidad = cantidad;
            this.tipoProducto = tipoProducto;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Getter / Setter Descripcion
        /// </summary>
        public string Descripcion
        {
            get { return this.descripcion; }

            set
            {
                this.descripcion = value;

                if (String.IsNullOrEmpty(value))
                {
                    this.descripcion = "Unknown";
                }
            }
        }

        /// <summary>
        /// Getter / Setter ID
        /// </summary>
        public int Id
        {
            get { return this.idProducto; }

            set
            {
                this.idProducto = value;

                if (ValidarNegativos(value))
                {
                    this.idProducto = 0;
                }
            }
        }

        /// <summary>
        /// Getter / Setter Precio
        /// </summary>
        public double Precio
        {
            get { return this.precio; }

            set
            {
                this.precio = value;

                if (ValidarNegativos(value))
                {
                    this.precio = 0;
                }
            }

        }

        /// <summary>
        /// Getter / Setter Cantidad
        /// </summary>
        public int Cantidad
        {
            get { return this.cantidad; }

            set
            {
                this.cantidad = value;

                if (ValidarNegativos(value))
                {
                    this.cantidad = 0;
                }
            }
        }

        /// <summary>
        /// Getter / Setter tipo producto
        /// </summary>
        public ETipo Tipo
        {
            get { return this.tipoProducto; }

            set { this.tipoProducto = value; }
        }
        #endregion

        #region Sobrecarga de operadores

        /// <summary>
        /// Verifica si un producto se encuentra cargado en la lista
        /// </summary>
        /// <param name="productos"></param>
        /// <param name="auxProducto"></param>
        /// <returns>true si lo encuentra, false caso contrario</returns>
        public static bool operator ==(List<Producto> productos, Producto auxProducto)
        {

            foreach (Producto item in productos)
            {
                if(item.idProducto == auxProducto.idProducto)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Verifica si un producto no se encuentra cargado en la lista
        /// </summary>
        /// <param name="productos"></param>
        /// <param name="auxProducto"></param>
        /// <returns>true si no lo encuentra, false caso contrario</returns>
        public static bool operator !=(List<Producto> productos, Producto auxProducto)
        {
            return !(productos == auxProducto);
        }

        /// <summary>
        /// Verifica si un producto no esta cargado en la base de datos y lo agrega.
        /// </summary>
        /// <param name="productos"></param>
        /// <param name="auxProducto"></param>
        /// <returns>true si logra cargar el producto, si ya se encontraba en la lista lanza ProductosException</returns>
        public static bool operator +(List<Producto> productos, Producto auxProducto)
        {

            if (productos != auxProducto)
            {
                auxProducto.Guardar();
                return true;
            }
            else
            {
                throw new ProductosException("Producto previamente cargado a la base de datos, solo se modifican los datos");
            }

        }

        /// <summary>
        /// Verifica si un producto no esta cargado en la base de datos y lo elimina
        /// </summary>
        /// <param name="productos"></param>
        /// <param name="auxProducto"></param>
        /// <returns>el objeto de tipo inventario con el producto borrado, si no se encontraba en la lista lanza ProductosException</returns>
        public static bool operator -(List<Producto> productos, Producto auxProducto)
        {
            if (productos == auxProducto)
            {
                auxProducto.Eliminar();
                return true;
            } 
            else
            {
                throw new ProductosException("Este producto no se encuentra cargado en la base de datos");
            }


        }

        #endregion

        #region Metodos

        /// <summary>
        /// Valida si un numero es menor a 0
        /// </summary>
        /// <param name="numero"></param>
        /// <returns>true si es menor, false caso contrario</returns>
        public static bool ValidarNegativos(double numero)
        {
            if (numero < 0)
            {
                return true;
            } else
            {
                return false;
            }
        }
        /// <summary>
        /// Genera un stringbuilder con todos los datos del producto
        /// </summary>
        /// <returns></returns>
        protected virtual string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Descripcion: {this.descripcion}");
            sb.AppendLine($"Cantidad: {this.cantidad.ToString()}");
            sb.AppendLine($"ID Producto: {this.idProducto.ToString()}");
            sb.AppendLine(String.Format("Precio: ${0: #,###.00}", this.precio));

            return sb.ToString();
        }

        /// <summary>
        /// override del metodo mostrar
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.Mostrar();
        }

        /// <summary>
        /// Inserta el objeto Producto en la base de datos
        /// </summary>
        /// <returns>True si se inserto, false caso contrario</returns>
        public bool Guardar()
        {
            ProductoDAO prod = new ProductoDAO();
            return prod.InsertarProducto(this);
        }

        /// <summary>
        /// Modificar el objeto Producto en la base de datos
        /// </summary>
        /// <returns>True si se modifico, false caso contrario</returns>
        public bool Modificar()
        {
            ProductoDAO prod = new ProductoDAO();
            return prod.ModificarProducto(this);
        }


        /// <summary>
        /// Eliminar el objeto Producto de la base de datos
        /// </summary>
        /// <returns>True si se elimino, false caso contrario</returns>
        public bool Eliminar()
        {
            ProductoDAO prod = new ProductoDAO();
            return prod.EliminarProducto(this);
        }
        #endregion
    }
}
