using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Excepciones;

namespace Entidades
{
    public class ProductoDAO
    {
        private SqlConnection conexion;
        private SqlCommand comando;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ProductoDAO()
        {
            conexion = new SqlConnection();
            conexion.ConnectionString = "Data Source=.\\sqlexpress; Initial Catalog=TP4; Integrated Security=True;";
            comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType = CommandType.Text;
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Ejecuta ExecuteNonQuery() en una conexion SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>True si se ejecuto, false caso contrario</returns>
        public bool EjecutarNonQuery(string sql)
        {
            bool ejecuto = false;
            try
            {
                comando.CommandText = sql;
                conexion.Open();
                comando.ExecuteNonQuery();
                ejecuto = true;
            }
            catch (Exception e)
            {
                ejecuto = false;
                throw new ArchivosException("Falla al intentar trabajar sobre la base de datos", e);
            }
            finally
            {
                conexion.Close();
            }

            return ejecuto;
        }

        /// <summary>
        /// Inserta un producto a la base de datos
        /// </summary>
        /// <param name="prod"></param>
        /// <returns>True si se guardo, false caso contrario</returns>
        public bool InsertarProducto(Producto prod)
        {
            string sql = "Insert into Productos(descripcion, idProducto, precio, cantidad, tipoProducto) values('" + prod.Descripcion +
                "', " + prod.Id + ", " + prod.Precio.ToString() + ", " + prod.Cantidad.ToString() + ", '" + prod.Tipo.ToString() + "')"; 

            return EjecutarNonQuery(sql);
        }
        /// <summary>
        /// Modifica un producto de la base de datos
        /// </summary>
        /// <param name="prod"></param>
        /// <returns>True si se modifico, false caso contrario</returns>
        public bool ModificarProducto(Producto prod)
        {
            string sql = "Update Productos Set descripcion = '" + prod.Descripcion +
                "', idProducto = " + prod.Id + ", precio = " + prod.Precio.ToString() + ", cantidad = " + prod.Cantidad.ToString() +
                ", tipoProducto = '" + prod.Tipo.ToString() + "' where idProducto = " + prod.Id;

            return EjecutarNonQuery(sql);
        }

        /// <summary>
        /// Elimina un producto de la base de datos
        /// </summary>
        /// <param name="prod"></param>
        /// <returns>true si se elimino, false caso contrario</returns>
        public bool EliminarProducto(Producto prod)
        {
            string sql = "Delete Productos where id = " + prod.Id.ToString();

            return EjecutarNonQuery(sql);
        }

        /// <summary>
        /// Trae el listado de todos los productos guardados en la base de datos
        /// </summary>
        /// <returns>Lista de productos</returns>
        public List<Producto> Leer()
        {
            List<Producto> productos = new List<Producto>();

            try
            {
                comando.CommandText = "Select * from Productos";
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    string tipo = reader["tipoProducto"].ToString();

                    if (tipo == "perecedero")
                    {
                        productos.Add(new ProductoPerecedero(reader["descripcion"].ToString(), int.Parse(reader["idProducto"].ToString()),
                        int.Parse(reader["Precio"].ToString()), int.Parse(reader["cantidad"].ToString()), Producto.ETipo.perecedero));

                    } else
                    {
                        productos.Add(new ProductoNoPerecedero(reader["descripcion"].ToString(), int.Parse(reader["idProducto"].ToString()),
                        int.Parse(reader["Precio"].ToString()), int.Parse(reader["cantidad"].ToString()), Producto.ETipo.noPerecedero));
                    }


                }

                reader.Close();
            }
            catch (Exception e)
            {

                throw new ArchivosException("Falla al intentar leer la base de datos", e);

            }
            finally
            {
                conexion.Close();
            }

            return productos;
        }

        /// <summary>
        /// Trae un producto de la base de datos identificado con el ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto de tipo producto</returns>
        public Producto LeerPorID(int id)
        {
            Producto prod = null;

            try
            {
                comando.CommandText = "Select * from Productos where id = " + id.ToString();
                conexion.Open();
                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    string tipo = reader["tipoProducto"].ToString();

                    if (tipo == "perecedero")
                    {
                        prod = new ProductoPerecedero(reader["descripcion"].ToString(), id,
                        int.Parse(reader["Precio"].ToString()), int.Parse(reader["cantidad"].ToString()), Producto.ETipo.perecedero);

                    } else
                    {
                        prod = new ProductoNoPerecedero(reader["descripcion"].ToString(), id,
                        int.Parse(reader["Precio"].ToString()), int.Parse(reader["cantidad"].ToString()), Producto.ETipo.noPerecedero);
                    }
                }


                reader.Close();
            }
            catch (Exception e)
            {
                throw new ArchivosException("Falla al intentar leer la base de datos", e);
            }
            finally
            {
                conexion.Close();
            }

            return prod;
        }
        #endregion
    }
}
