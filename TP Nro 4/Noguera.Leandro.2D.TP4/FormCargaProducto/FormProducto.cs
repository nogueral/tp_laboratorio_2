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

namespace FormCargaProducto
{
    public partial class FormProducto : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FormProducto()
        {
            InitializeComponent();
        }

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
        /// Crea un objeto de tipo Producto y lo agrega a la base de dato
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

                this.txtPrecio.Text = this.txtPrecio.Text.Replace(".", ",");


                if (double.TryParse(this.txtPrecio.Text, out precio) && int.TryParse(this.txtId.Text, out idProducto) &&
                    int.TryParse(this.txtCantidad.Text, out cantidad))
                {
                    Producto prod;

                    if ((Producto.ETipo)this.cmbTipoProd.SelectedValue == Producto.ETipo.perecedero)
                    {
                        prod = new ProductoPerecedero(this.txtDescripcion.Text, idProducto, precio, cantidad, Producto.ETipo.perecedero);

                        if(Inventario.Productos + prod)
                        {
                            MessageBox.Show("Cargado con exito");
                            MessageBox.Show(prod.ToString());
                        }

                    }
                    else
                    {
                        prod = new ProductoNoPerecedero(this.txtDescripcion.Text, idProducto, precio, cantidad, Producto.ETipo.noPerecedero);

                        if (Inventario.Productos + prod)
                        {
                            MessageBox.Show("Cargado con exito");
                            MessageBox.Show(prod.ToString());
                        }
                    }

                    this.Limpiar();

                }
                else
                {
                    MessageBox.Show("Verifique campos numericos", "Carga de Productos");
                    this.Limpiar();
                }
            }
            catch (Exception exc)
            {

                MessageBox.Show(exc.Message);
                 this.Limpiar();
            }
        }

        /// <summary>
        /// Llamada al metodo limpiar
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.Limpiar();
        }

        /// <summary>
        /// Limpia los textboxs
        /// </summary>
        private void Limpiar()
        {
            this.txtDescripcion.Clear();
            this.txtId.Clear();
            this.txtCantidad.Clear();
            this.txtPrecio.Clear();
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
    }
}
