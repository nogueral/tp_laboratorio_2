using System;
using System.Text;

namespace Entidades
{
    public class Suv : Vehiculo
    {

        #region Constructor

        /// <summary>
        /// Cconstructor
        /// </summary>
        /// <param name="marca"></param>
        /// <param name="chasis"></param>
        /// <param name="color"></param>
        public Suv(EMarca marca, string chasis, ConsoleColor color): base(chasis, marca, color)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Las camionetas son grandes
        /// </summary>
        public override ETamanio Tamanio
        {
            get
            {
                return ETamanio.Grande;
            }
        }

        #endregion

        #region Sobrecargas

        /// <summary>
        /// Crea un stringbuilder con todos los datos del vehiculo
        /// </summary>
        /// <returns>un string con todos los datos</returns>
        public override string Mostrar()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("SUV");
            sb.AppendLine(base.Mostrar());
            sb.AppendLine($"TAMAÑO : {this.Tamanio}");
            sb.AppendLine("");
            sb.AppendLine("---------------------");

            return sb.ToString();
        }

        #endregion
    }
}
