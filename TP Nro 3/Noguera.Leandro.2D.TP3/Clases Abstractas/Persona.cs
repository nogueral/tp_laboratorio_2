using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Excepciones;

namespace Clases_Abstractas
{
    public abstract class Persona
    {
        #region Enumerados
        /// <summary>
        /// Enum Nacionalidad
        /// </summary>
        public enum ENacionalidad
        {
            Argentino,
            Extranjero
        }
        #endregion

        #region atributos
        string apellido;
        int dni;
        ENacionalidad nacionalidad;
        string nombre;
        #endregion

        #region Propiedades
        /// <summary>
        /// Propiedad Apellido
        /// </summary>
        public string Apellido
        {
            get { return this.apellido; }

            set { this.apellido = ValidarNombreApellido(value); }
        }

        /// <summary>
        /// Propiedad Dni
        /// </summary>
        public int Dni
        {
            get { return this.dni; }

            set { this.dni = ValidarDni(this.nacionalidad, value); }
        }

        /// <summary>
        /// Propiedad Nacionalidad
        /// </summary>
        public ENacionalidad Nacionalidad
        {
            get { return this.nacionalidad; }

            set { this.nacionalidad = value; }
        }

        /// <summary>
        /// Propiedad Nombre
        /// </summary>
        public string Nombre
        {
            get { return this.nombre; }

            set { this.nombre = ValidarNombreApellido(value); }
        }

        /// <summary>
        /// Propiedad StringToDNI
        /// </summary>
        public string StringToDNI
        {
            set { this.dni = ValidarDni(this.nacionalidad, value); }
        }
        #endregion

        #region Constructores

        /// <summary>
        /// Constructor 
        /// </summary>
        public Persona()
        {

        }

        /// <summary>
        /// Constructor parametrizado (recibe 3 parametros)
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, ENacionalidad nacionalidad)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Nacionalidad = nacionalidad;
        }

        /// <summary>
        /// Constructor parametrizado (recibe 4 parametros - dni tipo int)
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, int dni, ENacionalidad nacionalidad) : this(nombre, apellido, nacionalidad)
        {

            this.Dni = dni;

        }

        /// <summary>
        /// Constructor parametrizado (recibe 4 parametros - dni tipo string)
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Persona(string nombre, string apellido, string dni, ENacionalidad nacionalidad) : this(nombre, apellido, nacionalidad)
        {

            this.StringToDNI = dni;

        }
        #endregion

        #region Metodos
        /// <summary>
        /// Sobrecarga del metodo ToString()
        /// </summary>
        /// <returns>Devuelve todos los datos de la persona</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"NOMBRE COMPLETO: {this.Apellido}, {this.Nombre}");
            sb.AppendLine($"NACIONALIDAD: {this.Nacionalidad}");
            sb.AppendLine($"DNI: {this.Dni}");

            return sb.ToString();
        }

        /// <summary>
        /// Valida que el DNI sea correcto, teniendo en cuenta su nacionalidad. Argentino entre 1 y 89999999 y Extranjero entre 90000000 y 99999999
        /// </summary>
        /// <param name="nacionalidad"></param>
        /// <param name="dato"></param>
        /// <returns>El dato validado. Si no hay concordancia lanza la excepcion NacionalidadInvalidaException. Si el DNI esta fuera de los rangos lanza DniInvalidoException</returns>
        private int ValidarDni(ENacionalidad nacionalidad, int dato)
        {
            if (dato < 1 || dato > 99999999)
            {
                throw new DniInvalidoException("El DNI ingresado es invalido");
            }

            switch (nacionalidad)
            {
                case ENacionalidad.Argentino:
                    if(dato >= 90000000 && dato <= 99999999) 
                    {
                        dato = 0;

                        throw new NacionalidadInvalidaException("No hay coincidencia entre la nacionalidad y el Nro de DNI");
                    }
                    break;
                case ENacionalidad.Extranjero:
                    if (dato >= 1 && dato <= 89999999)
                    {
                        dato = 0;

                        throw new NacionalidadInvalidaException("No hay coincidencia entre la nacionalidad y el Nro de DNI");
                    }
                    break;
            }

            return dato;
        }

        /// <summary>
        /// Valida si el DNI presenta algun error de formato
        /// </summary>
        /// <param name="nacionalidad"></param>
        /// <param name="dato"></param>
        /// <returns>Devuelve el DNI validado, caso contrario lanza la excepcion DniInvalidoException</returns>
        private int ValidarDni(ENacionalidad nacionalidad, string dato)
        {
            int auxDni;

            if(String.IsNullOrEmpty(dato) || !int.TryParse(dato, out auxDni))
            {
                auxDni = 0;

                throw new DniInvalidoException("El dato ingresado es invalido. DNI debe ser numerico");

            }

            return this.ValidarDni(nacionalidad, auxDni);
        }

        /// <summary>
        /// Valida que los nombres sean cadenas con caracteres válidos para nombres
        /// </summary>
        /// <param name="dato"></param>
        /// <returns>El nombre validado, caso contrario retorna "Unknown"</returns>
        private string ValidarNombreApellido(string dato)
        {
            string pattern = @"\d";

            if (!String.IsNullOrEmpty(dato) && dato.Length > 2)
            {
                Regex auxRegex = new Regex(pattern);

                MatchCollection auxMatch = auxRegex.Matches(dato);

                if (auxMatch.Count == 0)
                {
                    return dato;
                }
            }

            return "Unknown";
           
        }
        #endregion
    }
}
