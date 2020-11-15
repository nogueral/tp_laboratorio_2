using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Entidades;
using Excepciones;

namespace TestUnitario
{
    [TestClass]
    public class TestTP4
    {
        /// <summary>
        /// Testea que se lance la exception ProductosException al intentar cargar un Producto duplicado a la base de datos
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ProductosException))]
        public void ProductoRepetido_Test()
        {
            bool retorno;
            ProductoPerecedero p1 = new ProductoPerecedero("Leche", 1, 62, 30, Producto.ETipo.perecedero);

            retorno = Inventario.Productos + p1;
            retorno = Inventario.Productos + p1;
        }

        /// <summary>
        /// Verifica que se instancie la lista de tipo Producto al instanciar un objeto de tipo Venta
        /// </summary>
        [TestMethod]
        public void ListaProductos_Test()
        {
            Venta v = new Venta();

            Assert.IsNotNull(v.Items);
        }
    }
}
