using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Excepciones;
using Clases_Abstractas;
using Clases_Instanciables;

namespace Test_Unitarios
{
    [TestClass]
    public class ColeccionesTest
    {
        /// <summary>
        /// Testea que se haya instanciado el atributo de tipo coleccion
        /// </summary>
        [TestMethod]
        public void Test_ListaAlumnos()
        {
            Universidad u = new Universidad();

            Assert.IsNotNull(u.Alumnos);
        }

    }
}
