using System;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cadena = "    Hello             World!           ";
            string cadenasinespcios = System.Text.RegularExpressions.Regex.Replace(cadena, @"\s+", " ");
            Console.WriteLine(cadenasinespcios);

            int numeroFinal = FormatNumber(10);
            Console.WriteLine(numeroFinal);
            Console.ReadLine();
        }

        public static int FormatNumber(int number)
        {
            string binaryRep = Convert.ToString(number, 2);

            char[] chars = binaryRep.ToCharArray();
            Array.Reverse(chars);
            binaryRep = new string(chars);

            int result = Convert.ToInt32(binaryRep, 2);

            return result;
        }
    }
}
