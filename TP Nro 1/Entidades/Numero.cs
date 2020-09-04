using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    class Numero
    {
        private double numero;

        /// <summary>
        /// Valida el dato recibido y lo asigna a numero
        /// </summary>
        public string SetNumero { set => numero = ValidarNumero(value); }

        /// <summary>
        /// Recibe un numero binario y lo transforma en decimal
        /// </summary>
        /// <param name="binario">parametro recibido en formato string</param>
        /// <returns>numero decimal en formato string</returns>
        public static string BinarioDecimal(string binario)
        {
            string resultado = "Valor invalido";
            double acumulador = 0;
            char[] array = binario.ToCharArray();
            Array.Reverse(array);

            if (EsBinario(binario))
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == '1')
                    {
                        acumulador += Math.Pow(2, i);
                    }
                    
                }

                resultado = acumulador.ToString();
            }


            return resultado;
        }

        /// <summary>
        /// Recibe un numero decimal y lo transforma en binario
        /// </summary>
        /// <param name="numero">parametro recibido, numero decimal en formato double</param>
        /// <returns>Numero binario en formato string</returns>
        public static string DecimalBinario(double numero)
        {
            string resultado = "";
            int numeroEntero = (int)numero;

            if (numeroEntero > 0)
            {
                while (numeroEntero > 0)
                {
                    if (numeroEntero % 2 == 0)
                    {
                        resultado = "0" + resultado;

                    }
                    else
                    {
                        resultado = "1" + resultado;
                    }

                    numeroEntero = numeroEntero / 2;

                }
            } else
            {
                resultado = "Valor invalido";
            }

            return resultado;
        }

        /// <summary>
        /// Recibe un numero decimal y lo transforma en binario
        /// </summary>
        /// <param name="numero">parametro recibido, numero decimal en formato string</param>
        /// <returns>Numero binario en formato string</returns>
        public static string DecimalBinario(string numero)
        {
            string resultado = "Valor invalido";
            double auxNumeroIngresado;

            if (double.TryParse(numero, out auxNumeroIngresado))
            {
                resultado = DecimalBinario(auxNumeroIngresado);
            }

            return resultado;
        }

        /// <summary>
        /// Valida si el elemento recibido por parametro es un numero binario
        /// </summary>
        /// <param name="binario">Numero binario en formato string</param>
        /// <returns>True si es un numero binario, false si no lo es</returns>
        private static bool EsBinario(string binario)
        {
            bool retorno = true;
            char[] array = binario.ToCharArray();

            for (int i = 0; i < array.Length; i++)
            {
                if(array[i] != '0' && array[i] != '1')
                {
                    retorno = false;
                    break;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Numero()
        {
            this.numero = 0;
        }

        /// <summary>
        /// Constructor parametrizado
        /// </summary>
        /// <param name="numero">Recibe un numero en tipo double</param>
        public Numero(double numero)
        {
            this.numero = numero;
        }

        /// <summary>
        /// Constructor parametrizado
        /// </summary>
        /// <param name="numero">Recibe un numero en formato string</param>
        public Numero(string numero)
        {
            this.SetNumero = numero;
        }

        /// <summary>
        /// Suma dos objetos de tipo Numero
        /// </summary>
        /// <param name="n1">Primer parametro recibido objeto de tipo Numero</param>
        /// <param name="n2">Segundo parametro recibido objeto de tipo Numero</param>
        /// <returns>La suma de los dos elementos</returns>
        public static double operator +(Numero n1, Numero n2)
        {
            return n1.numero + n2.numero;
        }

        /// <summary>
        /// Resta dos objetos de tipo Numero
        /// </summary>
        /// <param name="n1">Primer parametro recibido objeto de tipo Numero</param>
        /// <param name="n2">Segundo parametro recibido objeto de tipo Numero</param>
        /// <returns>La resta de los dos elementos</returns>
        public static double operator -(Numero n1, Numero n2)
        {
            return n1.numero - n2.numero;
        }

        /// <summary>
        /// Multiplica dos objetos de tipo Numero
        /// </summary>
        /// <param name="n1">Primer parametro recibido objeto de tipo Numero</param>
        /// <param name="n2">Segundo parametro recibido objeto de tipo Numero</param>
        /// <returns>La multiplicacion de los dos elementos</returns>
        public static double operator *(Numero n1, Numero n2)
        {
            return n1.numero * n2.numero;
        }

        /// <summary>
        /// Divide dos objetos de tipo Numero
        /// </summary>
        /// <param name="n1">Primer parametro recibido objeto de tipo Numero</param>
        /// <param name="n2">Segundo parametro recibido objeto de tipo Numero</param>
        /// <returns>La division de los dos elementos. Si n2 es 0 devuelve double.MinValue</returns>
        public static double operator /(Numero n1, Numero n2)
        {
            if (n2.numero == 0)
            {
                return double.MinValue;

            } else
            {
                return n1.numero / n2.numero;
            }
        }

        /// <summary>
        /// Valida que el parametro recibido sea numerico
        /// </summary>
        /// <param name="strNumero">Numero recibido en formato string</param>
        /// <returns>El valor recibido. Si no tiene formato numerico retorna 0</returns>
        private double ValidarNumero(string strNumero)
        {
            double auxNumero;

            if(!double.TryParse(strNumero, out auxNumero))
            {
                auxNumero = 0;
            }

            return auxNumero;
        }
    }
}
