using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoGlobal
{
    internal class Program
    {
        static void Main(string[] args)
        {
 
            Console.WriteLine("Termine: " + verifEc());




            
            

            Console.ReadKey();

        }




        public static string verifEc()                                           
        {
            string ecuacion;
            Console.WriteLine("Escriba la ecuacion");
            ecuacion = Console.ReadLine();

            string ec_terminada = "";
            char[] mis_letras = {'1','2','3','4','5','6','7','8','9','0','+','-','*','/','(',')'};
            char[] letras;

            letras = ecuacion.ToCharArray();

            foreach (char let in letras)
            {
                for (int i = 0; i < mis_letras.Length; i++)
                {
                    if (let == mis_letras[i])
                    {
                        ec_terminada += let;
                    }
                    
                }
            }

            ec_terminada = verifParentesis(ec_terminada);

            if(ecuacion != ec_terminada)
            {
                veriFinal(ec_terminada);
            }

            
            return ec_terminada;

        }

        public static string verifParentesis(string ec)                                  //Verifica la apertura y cierre de los parentesis en la ecuacion 
        {

            Boolean pAbre = false;
            Boolean pCierra = false;
            string resultadoEc = "";
            int contA = 0;
            int contB = 0;

            for (int i = 0; i < ec.Length; i++)
            {
                if (ec[i] == '(')
                {
                    contA++;

                }
                else if (ec[i] == ')')
                {
                    contB++;

                }

            }

            if (contA != contB)
            {

                Console.WriteLine("---ENTRA SON DISTINTOS---");

                for (int i = 0; i < ec.Length; i++)
                {

                    Boolean agregoCarac = true;

                    if (ec[i] == '(')
                    {

                        if (pAbre == true && pCierra == false)
                        {
                            agregoCarac = false;
                        }
                        
                        pAbre = true;

                    }
                    else if (ec[i] == ')')
                    {

                        pCierra = true;
                        
                        if (pCierra == true && pAbre == false)
                        {
                            agregoCarac = false;
                        }

                    }

                    if (agregoCarac == true)
                    {
                        resultadoEc += ec[i];
                    }

                    if (pAbre == true && pCierra == true)
                    {
                        pAbre = false;
                        pCierra = false;
                    }

                }

            }
            else
            {
                resultadoEc = ec;
            }

            //Console.WriteLine("Primera vez " + resultadoEc);

            return resultadoEc;
            
        }

        public static string veriFinal(string ec)
        {

            string car;
            string result = null;
            Console.WriteLine("Hay caracteres que no pertenecen a la ecuacion, van a ser eliminados");
            Console.WriteLine(Environment.NewLine + "La ecuacion quedará asi: " + ec + Environment.NewLine);
            Console.WriteLine("Esta de acuerdo? Y = SI, N = NO");
            
            do
            {
                car = Console.ReadLine().ToUpper();
                if (car == "Y")
                {
                    Console.WriteLine("Calculando...");                                                         //<----- ENTRAN LOS CALCULOS
                    result = ec;
                }
                else if(car == "N")
                {
                    result = verifEc();                         
                }
                else
                {
                    Console.WriteLine("No ingresó ni 'Y' ni 'N', intente de nuevo");
                }

            } while (car != "Y" && car != "N");

            return result;
        }


        public static float hacerCalculo(string ecuacion)
        {
            float resultado;

            //Creacion de dos arrays para indicar las posiciones de los pares de parentesis relacionados
            int[] prioridadParentesisAbrir = new int[ecuacion.Length/2];
            int[] prioridadParentesisCerrar = new int[ecuacion.Length / 2];

            //Array indicando si el numero de parentesis abierto ya tiene su par encontrado
            bool[] parNoEncontrado = new bool[ecuacion.Length / 2];

            //Contadores de parentesis
            int numeroParentesis = -1;

            //Iteracion por toda la ecuacion
            for (int i = 0; i < ecuacion.Length; i++)
            {
                //Console.WriteLine("index: " + i);
                //parentesisCerrar = parentesisAbrir;
                if (ecuacion[i] == '(')
                {
                    
                    numeroParentesis++;
                    //Si encuentra un parentesis abierto, lo guarda en un array y de valor va el index
                    prioridadParentesisAbrir[numeroParentesis] = i;
                    //Console.WriteLine("index de (: " + i);

                    //Se declara no encontrado el index del numero del par
                    parNoEncontrado[numeroParentesis] = true;

                }else if (ecuacion[i] == ')')
                {
                    //Paso por todo el array de pares no encontrados, buscando el ultimo par no completo
                    for(int j = numeroParentesis; j >= 0; j--)
                    {
                        //Si encuentra uno que no esta completo
                        if (parNoEncontrado[j] == true)
                        {
                            //Guarda en el index del numero del par, la posicion donde esta el parentesis cerrando
                            prioridadParentesisCerrar[j] = i;
                            //Console.WriteLine("index de ): "+i);

                            //Cambia el bool de ese par como falso
                            parNoEncontrado[j] = false;
                        }
                    }
                }
            }

            //Contar el tamaño que tendra el substring dentro del mayor par de parentesis
            int substringLenght = prioridadParentesisCerrar[numeroParentesis] - prioridadParentesisAbrir[numeroParentesis] -1;

            //Nuevo substring para miniecuaciones
            string miniEcuacion = ecuacion.Substring(prioridadParentesisAbrir[numeroParentesis]+1, substringLenght);

            Console.WriteLine(miniEcuacion);

            return 0;

        }

        public static string realizarEcSub(string subEcuacion)
        {
            string resultado;




            return "a";
        }

    }
}
