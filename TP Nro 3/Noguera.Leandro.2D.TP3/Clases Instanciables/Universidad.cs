using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Archivos;
using Excepciones;

namespace Clases_Instanciables
{
    public class Universidad
    {
        #region Enumerados
        /// <summary>
        /// enum EClases
        /// </summary>
        public enum EClases
        {
            Programacion,
            Laboratorio,
            Legislacion,
            SPD
        }
        #endregion

        #region Atributos
        List<Alumno> alumnos;
        List<Jornada> jornada;
        List<Profesor> profesores;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public Universidad()
        {
            alumnos = new List<Alumno>();
            jornada = new List<Jornada>();
            profesores = new List<Profesor>();
        }
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
        /// Propiedad Jornadas
        /// </summary>
        public List<Jornada> Jornadas
        {
            get { return this.jornada; }

            set { this.jornada = value; }
        }

        /// <summary>
        /// Propiedad Profesores
        /// </summary>
        public List<Profesor> Instructores
        {
            get { return this.profesores; }

            set { this.profesores = value; }
        }

        /// <summary>
        /// Indexador jornada
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public Jornada this [int i]
        {
            get { return this.jornada[i]; }

            set { this.jornada[i] = value; }
        }
        #endregion

        #region Metodos

        /// <summary>
        /// Muestra todos los datos del objeto de tipo Universidad recibido por parametro
        /// </summary>
        /// <param name="uni"></param>
        /// <returns>una cadena con todos los datos del objeto de tipo Universidad recibido por parametro</returns>
        private static string MostrarDatos(Universidad uni)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in uni.jornada)
            {
                sb.AppendLine(item.ToString());
            }

            return sb.ToString();
        }

        /// <summary>
        /// Hace publicos los datos del metodo de clase MostrarDatos
        /// </summary>
        /// <returns>todos los datos del objeto de tipo Universidad</returns>
        public override string ToString()
        {
            return MostrarDatos(this);
        }

        /// <summary>
        /// Gestiona el archivo de un objeto de tipo Universidad en formato xml
        /// </summary>
        /// <param name="uni"></param>
        /// <returns>true si se leyo correctamente, false caso contrario</returns>
        public static bool Guardar(Universidad uni)
        {
            string path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "universidad.xml");
            Xml<Universidad> auxUni = new Xml<Universidad>();

            return auxUni.Guardar(path, uni);
        }

        /// <summary>
        /// Gestiona la lectura de objetos de tipo Universidad en formato xml
        /// </summary>
        /// <returns>Objeto de tipo Universidad</returns>
        public static Universidad Leer()
        {
            Universidad datos = new Universidad();
            string path = String.Concat(AppDomain.CurrentDomain.BaseDirectory, "universidad.xml");
            Xml<Universidad> auxUni = new Xml<Universidad>();

            auxUni.Leer(path, out datos);

            return datos;

        }

        #endregion

        #region Operadores

        /// <summary>
        /// Un Universidad será igual a un Alumno si el mismo está inscripto en él.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns>true si la validacion es correcta, caso contrario devuelve false</returns>
        public static bool operator ==(Universidad g, Alumno a)
        {
            foreach (Alumno item in g.alumnos)
            {
                if (item == a)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Un Universidad será distinto a un Alumno si el mismo no está inscripto en él.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="a"></param>
        /// <returns>true si la validacion es correcta, caso contrario devuelve false</returns>
        public static bool operator !=(Universidad g, Alumno a)
        {
            return !(g == a);
        }

        /// <summary>
        /// Un Universidad será igual a un Profesor si el mismo está dando clases en él.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns>true si la validacion es correcta, caso contrario devuelve false</returns>
        public static bool operator ==(Universidad g, Profesor i)
        {
            foreach (var item in g.jornada)
            {
                if (item.Instructor == i)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Un Universidad será distinto a un Profesor si el mismo no está dando clases en él.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <returns>true si la validacion es correcta, caso contrario devuelve false</returns>
        public static bool operator !=(Universidad g, Profesor i)
        {
            return !(g == i);
        }

        /// <summary>
        /// La igualación entre un Universidad y una Clase retornará el primer Profesor capaz de dar esa clase.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="clase"></param>
        /// <returns>retornará el primer Profesor capaz de dar esa clase. Sino, lanzará la Excepción SinProfesorException.</returns>
        public static Profesor operator ==(Universidad u, EClases clase)
        {
            foreach (var item in u.profesores)
            {
                if(item == clase)
                {
                    return item;
                }
            }

            throw new SinProfesorException("No hay profesor disponible para la clase requerida");
        }

        /// <summary>
        /// retornará el primer Profesor que no pueda dar la clase.
        /// </summary>
        /// <param name="u"></param>
        /// <param name="clase"></param>
        /// <returns>retornará el primer Profesor que no pueda dar la clase. si no lo encuentra retorna null</returns>
        public static Profesor operator !=(Universidad u, EClases clase)
        {
            foreach (var item in u.profesores)
            {
                if (item != clase)
                {
                    return item;
                }
            }

            return null;
        }

        /// <summary>
        /// Agrega un alumno a la lista verificando que no esté previamente cargado
        /// </summary>
        /// <param name="u"></param>
        /// <param name="a"></param>
        /// <returns>devuelve la lista con el alumno agregado, si ya estaba cargado previamente lanza la excepcion AlumnoRepetidoException</returns>
        public static Universidad operator +(Universidad u, Alumno a)
        {
            if (u != a)
            {
                u.alumnos.Add(a);

            } else
            {
                throw new AlumnoRepetidoException("El alumno que intenta cargar, ya fue agregado a la lista previamente");
            }

            return u;
        }

        /// <summary>
        /// Agrega un profesor a la lista verificando que no esté previamente cargado
        /// </summary>
        /// <param name="u"></param>
        /// <param name="i"></param>
        /// <returns>devuelve la lista con el profesor agregado (si corresponde realizar la carga)</returns>
        public static Universidad operator +(Universidad u, Profesor i)
        {
            if (u != i)
            {

                u.profesores.Add(i);
            }

            return u;
        }

        /// <summary>
        /// genera y agrega una nueva Jornada indicando la clase, un Profesor que pueda darla y la lista de alumnos que la toman.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="clase"></param>
        /// <returns>El objeto de tipo Universidad con la Jornada cargada</returns>
        public static Universidad operator +(Universidad g, EClases clase)
        {
            Jornada auxJornada = new Jornada(clase, (g == clase));

            foreach (Alumno item in g.alumnos)
            {
                if (item == clase)
                {
                    auxJornada += item;
                }
            }

            g.jornada.Add(auxJornada);

            return g;
        }
        #endregion


    }
}
