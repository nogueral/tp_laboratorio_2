using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clases_Abstractas
{
    public abstract class Universitario : Persona
    {
        #region atributos
        int legajo;

        #endregion

        #region Constructores

        /// <summary>
        /// Constructor
        /// </summary>
        public Universitario() : base()
        {

        }

        /// <summary>
        /// Constructor parametrizado
        /// </summary>
        /// <param name="legajo"></param>
        /// <param name="nombre"></param>
        /// <param name="apellido"></param>
        /// <param name="dni"></param>
        /// <param name="nacionalidad"></param>
        public Universitario(int legajo, string nombre, string apellido, string dni, ENacionalidad nacionalidad) : base(nombre, apellido, dni, nacionalidad)
        {
            this.legajo = legajo;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Metodo abstracto ParticiparEnClase();
        /// </summary>
        /// <returns></returns>
        protected abstract string ParticiparEnClase();

        /// <summary>
        /// Valida que dos instancias de objeto sean del mismo tipo
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>True si son del mismo tipo, caso contrario devuelve false</returns>
        public override bool Equals(object obj)
        {
            if(ReferenceEquals(this.GetType(), obj.GetType()))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Muestra todos los datos correspondientes a un Universitario
        /// </summary>
        /// <returns></returns>
        protected virtual string MostrarDatos()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            sb.AppendLine($"LEGAJO NÚMERO: {this.legajo}");

            return sb.ToString();
        }

        #endregion

        #region Operadores

        /// <summary>
        /// Dos Universitario serán iguales si y sólo si son del mismo Tipo y su Legajo o DNI son iguales.
        /// </summary>
        /// <param name="pg1"></param>
        /// <param name="pg2"></param>
        /// <returns>True si la validacion es correcta. Caso contrario retorna false</returns>
        public static bool operator ==(Universitario pg1, Universitario pg2)
        {
            if(pg1.Equals(pg2) && (pg1.Dni == pg2.Dni || pg1.legajo == pg2.legajo))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Dos Universitario serán iguales si y sólo si son del mismo Tipo y su Legajo o DNI son iguales.
        /// </summary>
        /// <param name="pg1"></param>
        /// <param name="pg2"></param>
        /// <returns>True, si los universitarios son distintos. Caso contrario devuelve false</returns>
        public static bool operator !=(Universitario pg1, Universitario pg2)
        {
            return !(pg1 == pg2);
        }
        #endregion


    }
}
