using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Archivos;
using Entidades;
using Excepciones;

namespace FormProducto
{
    public partial class FormProducto : Form
    {
        Inventario inventario;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public FormProducto()
        {
            InitializeComponent();
            inventario = new Inventario();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Establece el origen de datos del combobox desde Producto.ETipo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormProducto_Load(object sender, EventArgs e)
        {
            this.cmbTipoProd.DataSource = Enum.GetValues(typeof(Producto.ETipo));
        }

        /// <summary>
        /// Cierra el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Limpia los textboxs
        /// </summary>
        private void Limpiar()
        {
            this.txtdescripcion.Clear();
            this.txtId.Clear();
            this.txtCantidad.Clear();
            this.txtPrecio.Clear();
        }

        /// <summary>
        /// Llama al metodo limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
        }

        /// <summary>
        /// Crea un objeto de tipo Producto y lo agrega a la base de datos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                int idProducto;
                double precio;
                int cantidad;


                if (double.TryParse(this.txtPrecio.Text, out precio) && int.TryParse(this.txtId.Text, out idProducto) &&
                    int.TryParse(this.txtCantidad.Text, out cantidad))
                {
                    Producto prod;

                    if ((Producto.ETipo)this.cmbTipoProd.SelectedValue == Producto.ETipo.perecedero)
                    {
                        prod = new ProductoPerecedero(this.txtdescripcion.Text, idProducto, precio, cantidad, Producto.ETipo.perecedero);

                        inventario += prod;
                        MessageBox.Show("Cargado con exito");
                        MessageBox.Show(prod.ToString());
                    } 
                    else
                    {
                        prod = new ProductoNoPerecedero(this.txtdescripcion.Text, idProducto, precio, cantidad, Producto.ETipo.noPerecedero);

                        inventario += prod;
                        MessageBox.Show("Cargado con exito");
                        MessageBox.Show(prod.ToString());
                    }

                    this.Limpiar();

                } else
                {
                    MessageBox.Show("Verifique campos numericos", "Carga de Productos");
                    this.Limpiar();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message);
            }
        }
        #endregion
    }
}
