using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    static class Calculadora
    {
        /// <summary>
        /// Valida que el elemento recibido sera un operador "+, -, * o /"
        /// </summary>
        /// <param name="operador">El operador recibido en formato char</param>
        /// <returns>El operador en formato string. Si el parametro es erroneo devuelve "+"</returns>
        private static string ValidarOperador(char operador)
        {
            string operadorRetorno = "+";

            if(operador == '+' || operador == '-' || operador == '*' || operador == '/')
            {
                operadorRetorno = Convert.ToString(operador);
            }

            return operadorRetorno;
        }

        /// <summary>
        /// Valida y realiza la operacion pedida entre dos objetos de tipo Numero
        /// </summary>
        /// <param name="n1">Objeto de tipo numero</param>
        /// <param name="n2">Objeto de tipo numero</param>
        /// <param name="operador">Operador en formato string</param>
        /// <returns>El resultado de la operacion</returns>
        private static double Operar(Numero n1, Numero n2, string operador)
        {
            char auxValidar;
            string auxOperador;
            double resultado = 0;

            auxValidar = Convert.ToChar(operador);

            auxOperador = ValidarOperador(auxValidar);

            switch (auxOperador)
            {
                case "+":
                    resultado = n1 + n2;
                    break;
                case "-":
                    resultado = n1 - n2;
                    break;
                case "*":
                    resultado = n1 * n2;
                    break;
                case "/":
                    resultado = n1 / n2;
                    break;
            }

            return resultado;
        }
    }
}
