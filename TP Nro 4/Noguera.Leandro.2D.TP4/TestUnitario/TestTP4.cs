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
            Inventario inventario = new Inventario();
            ProductoPerecedero p1 = new ProductoPerecedero("Leche", 1, 62, 30, Producto.ETipo.perecedero);

            inventario += p1;
            inventario += p1;
        }

        /// <summary>
        /// Verifica que se instancie la lista de tipo Venta al instanciar un objeto de tipo Inventario
        /// </summary>
        [TestMethod]
        public void ListaVentas_Test()
        {
            Inventario inventario = new Inventario();

            Assert.IsNotNull(inventario.Ventas);
        }
    }
}
