using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Excepciones
{
    public class ArchivosException : Exception
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ArchivosException() : base("Error en archivo")
        {

        }

        /// <summary>
        /// Constructor parametrizado
        /// </summary>
        /// <param name="mensaje"></param>
        public ArchivosException(string mensaje) : base(mensaje)
        {

        }

        /// <summary>
        /// Constructor parametrizado (recibe 2 parametros)
        /// </summary>
        /// <param name="innerException"></param>
        public ArchivosException(Exception innerException) : base("Error en archivo", innerException)
        {

        }
    }
}
