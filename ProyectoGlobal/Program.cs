using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGlobal
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string ecuacion;
            Console.WriteLine("Escriba la ecuacion");
            ecuacion = Console.ReadLine();  
            
            Console.WriteLine("Termine: " + verifEc(ecuacion));

            Console.ReadKey();

        }

        public static string verifEc(string ecuacion)
        {
            string ec_terminada = "";
            char[] mis_letras = {'1','2','3','4','5','6','7','8','9','0','+','-','*','/','(',')'};
            char[] letras;
            Boolean err = false;

            letras = ecuacion.ToCharArray();

            foreach (char let in letras)
            {
                for (int i = 0; i < mis_letras.Length; i++)
                {
                    if (let == mis_letras[i])
                    {
                        ec_terminada += let;
                    }
                    else
                    {
                        err = true;
                    }
                }
            }

            ec_terminada = verifParentesis(ec_terminada);

            /*
            if (err == true)
            {
                char car;
                Console.WriteLine("Hay caracteres que no pertenecen a la ecuacion, van a ser eliminados");
                Console.WriteLine("La ecuacion quedará asi, esta de acuerdo? Y = SI, N = NO");

                do
                {

                } while ();
                

            }
            */
            return ec_terminada;

        }

        public static string verifParentesis(string ec)
        {

            Boolean pAbre = false;
            Boolean pCierra = false;
            string resultadoEc = "";

            for (int i = 0; i < ec.Length; i++)
            {

                Boolean agregoCarac = true;

                if(ec[i] == '('){

                    if (pAbre == true && pCierra == false)
                    {
                        agregoCarac = false;
                    }

                    pAbre = true;

                }else if (ec[i] == ')')
                {

                    pCierra = true;

                    if(pCierra == true && pAbre == false)
                    {
                        agregoCarac=false;
                    }

                }

                if(agregoCarac == true)
                {
                    resultadoEc += ec[i];
                }

                if(pAbre == true && pCierra == true)
                {
                    pAbre = false;
                    pCierra = false;
                }
                
            }


            return resultadoEc;
        }

        public static string devolverString(int num)
        {
            if(num == 0)
            {
                return "todas";
            }
            else if(num == 1){
                return "putas";
            }
            else
            {
                return "jejejje";
            }
        }

        public static string devolverString20(int num)
        {
            if (num == 0)
            {
                return "todas";
            }
            else if (num == 1)
            {
                return "putas";
            }
            else
            {
                return "jejejje";
            }
        }

    }
}
