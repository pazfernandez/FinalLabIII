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

            //hacerCalculo(ecuacion);
            hacerCalculo("5+(3*8)-(4+(5/2))");

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

    }
}
