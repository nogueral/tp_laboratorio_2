using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archivos;

namespace Clases_Instanciables
{
    public class Jornada
    {
        #region Atributos
        List<Alumno> alumnos;
        Universidad.EClases clase;
        Profesor instructor;
        #endregion

        #region Propiedades

        /// <summary>
        /// Propiedad Alumnos
        /// </summary>
        public List<Alumno> Alumnos
        {
            get { return this.alumnos; }

            set { this.alumnos = value; }
        }

        /// <summary>
        /// Propiedad Clase
        /// </summary>
        public Universidad.EClases Clase
        {
            get { return this.clase; }

            set { this.clase = value; }
        }

        /// <summary>
        /// Propiedad Instructor
        /// </summary>
        public Profesor Instructor
        {
            get { return this.instructor; }

            set { this.instructor = value; }
        }
        #endregion

        #region Constructores
        /// <summary>
        /// constructor por defecto.
        /// </summary>
        private Jornada()
        {
            this.alumnos = new List<Alumno>();
        }

        /// <summary>
        /// constructor parametrizado.
        /// </summary>
        /// <param name="clase"></param>
        /// <param name="instructor"></param>
        public Jornada(Universidad.EClases clase, Profesor instructor) : this()
        {
            this.Clase = clase;
            this.Instructor = instructor;
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Muestra todos los datos de la Jornada.
        /// </summary>
        /// <returns>Una cadena con todos los datos de la Jornada.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<--------------------------------------------------------------------->");
            sb.AppendLine($"CLASE DE {this.clase.ToString()} POR {this.instructor.ToString()}");
            sb.AppendLine("ALUMNOS: ");
            foreach (Alumno item in this.alumnos)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }
        #endregion

        #region Operadores
        /// <summary>
        /// Una Jornada será igual a un Alumno si el mismo participa de la clase.
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns>true si la validacion es correcta, caso contrario devuelve false</returns>
        public static bool operator ==(Jornada j, Alumno a)
        {
            foreach (Alumno item in j.alumnos)
            {
                if(item == a)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Una Jornada será distinta a un Alumno si el mismo no participa de la clase.
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns>true si la validacion es correcta, caso contrario devuelve false</returns>
        public static bool operator !=(Jornada j, Alumno a)
        {
            return !(j == a);
        }

        /// <summary>
        /// Agrega Alumnos a la clase por medio del operador +, validando que no estén previamente cargados.
        /// </summary>
        /// <param name="j"></param>
        /// <param name="a"></param>
        /// <returns>Devuelve un objeto de tipo Jornada con el Alumno cargado (si corresponde)</returns>
        public static Jornada operator +(Jornada j, Alumno a)
        {
            if (j != a)
            {
                j.alumnos.Add(a);
            }

            return j;
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Gestiona el archivo de un objeto de tipo Jornada en formato texto
        /// </summary>
        /// <param name="jornada"></param>
        /// <returns>true si se guardo correctamente, false caso contrario</returns>
        public static bool Guardar(Jornada jornada)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Jornada.txt");
            Texto auxTexto = new Texto();

            return auxTexto.Guardar(path, jornada.ToString());
            
        }

        /// <summary>
        /// Gestiona el guardado de objetos de tipo Jornada en formato texto
        /// </summary>
        /// <returns>Los datos traidos de archivo en formato string</returns>
        public static string Leer()
        {
            string datos;
            string path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "Jornada.txt"); 
            Texto auxTexto = new Texto();

            auxTexto.Leer(path, out datos);

            return datos;
        }

        #endregion
    }
}
