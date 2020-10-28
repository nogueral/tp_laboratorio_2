using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Excepciones;
using Clases_Abstractas;
using Clases_Instanciables;

namespace Test_Unitarios
{
    [TestClass]
    public class ExcepcionesTest
    {
        /// <summary>
        /// Testea que se lance la excepcion AlumnoRepetidoException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(AlumnoRepetidoException))]
        public void Test_AlumnoRepetidoException()
        {
            Universidad u = new Universidad();
            Alumno a = new Alumno(1203, "Leandro", "Noguera", "33252056", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);

            u += a;
            u += a;
        }

        /// <summary>
        /// Testea que se lance la excepcion DniInvalidoException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(DniInvalidoException))]
        public void Test_DniInvalidoException()
        {
            Alumno a = new Alumno(1203, "Leandro", "Noguera", "hola", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);

        }

        /// <summary>
        /// Testea que se lance la excepcion NacionalidadInvalidaException
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NacionalidadInvalidaException))]
        public void Test_NacionalidadInvalidaException()
        {
            Alumno a = new Alumno(1203, "Leandro", "Noguera", "90000000", Persona.ENacionalidad.Argentino, Universidad.EClases.Laboratorio, Alumno.EEstadoCuenta.AlDia);

        }


    }
}
