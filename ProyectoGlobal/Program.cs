using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
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

            //string asd = "asd";
            //Console.WriteLine(asd.Length);

            Console.WriteLine("Termine: " + verifEc());
            realizarEcSubOperaciones("13*9/50-22-5+2");







            Console.ReadKey();

        }




        public static string verifEc()
        {
            string ecuacion;
            Console.WriteLine("Escriba la ecuacion");
            ecuacion = Console.ReadLine();

            string ec_terminada = "";
            char[] mis_letras = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '+', '-', '*', '/', '(', ')' };
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

            if (ecuacion != ec_terminada)
            {
                veriFinal(ec_terminada);
            }


            return ec_terminada;

        }

        public static string verifParentesis(string ec)                                  //Verifica la apertura y cierre de los parentesis en la ecuacion 
        {

            char[] mis_num = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            char[] mis_op = { '+', '-', '*', '/' };

            Boolean pAbre = false;
            Boolean pCierra = false;
            string resultadoEc = "";
            int contA = 0;
            int contB = 0;

            for (int i = 0; i < ec.Length; i++)
            {

                Boolean agregoCarac = true;

                if (ec[i] == '(')
                {
                    agregoCarac = false;

                    if (i >= 1)
                    {
                        for (int j = 0; j < mis_op.Length; j++)
                        {
                            if (ec[i - 1] == mis_op[j])
                            {
                                agregoCarac = true;
                            }

                        }
                    }

                    if (i < ec.Length - 1)
                    {
                        for (int j = 0; j < mis_num.Length; j++)
                        {
                            if (ec[i + 1] == mis_num[j])
                            {
                                agregoCarac = true;
                            }
                        }
                    }

                    if (agregoCarac == true)
                    {
                        contA++;
                    }


                }
                else if (ec[i] == ')')
                {
                    agregoCarac = false;

                    if (i >= 1)
                    {
                        for (int j = 0; j < mis_num.Length; j++)
                        {

                            if (ec[i - 1] == mis_num[j])
                            {
                                agregoCarac = true;
                            }

                        }
                    }

                    if (i < ec.Length - 1)
                    {
                        for (int j = 0; j < mis_op.Length; j++)
                        {
                            if (ec[i + 1] == mis_op[j])
                            {
                                agregoCarac = true;
                            }
                        }
                    }


                    if (agregoCarac == true)
                    {
                        contB++;
                    }


                    if (contB > contA)
                    {
                        agregoCarac = false;
                        if (contB > 0)
                        {
                            contB--;
                        }

                    }

                }

                if (agregoCarac == true)
                {
                    resultadoEc += ec[i];
                }

            }

            Console.WriteLine("contA = " + contA + " --- contB = " + contB);             //<---TRAZA 
            Console.WriteLine("ASI QUEDAAAAAAAA: " + resultadoEc);



            return resultadoEc;

        }

        public static string veriFinal(string ec)
        {

            string car;
            string result = null;
            Console.WriteLine("Hay caracteres que no pertenecen a la ecuacion o estan mal colocados, van a ser eliminados");
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
                else if (car == "N")
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
            int[] prioridadParentesisAbrir = new int[ecuacion.Length / 2];
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

                }
                else if (ecuacion[i] == ')')
                {
                    //Paso por todo el array de pares no encontrados, buscando el ultimo par no completo
                    for (int j = numeroParentesis; j >= 0; j--)
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
            int substringLenght = prioridadParentesisCerrar[numeroParentesis] - prioridadParentesisAbrir[numeroParentesis] - 1;

            //Nuevo substring para miniecuaciones
            string miniEcuacion = ecuacion.Substring(prioridadParentesisAbrir[numeroParentesis] + 1, substringLenght);

            Console.WriteLine(miniEcuacion);

            return 0;

        }

        public static String realizarEcSubOperaciones(String subEcuacion)
        {
            int tipoUltimaOperacion = -1;
            int numerosDespuesUltOp = 0;

            for (int i = 0; i < subEcuacion.Length; i++)
            {
                if (subEcuacion[i] == '*')
                {
                    tipoUltimaOperacion = 0;
                    numerosDespuesUltOp = subEcuacion.Length - i;
                }
                else if (subEcuacion[i] == '/')
                {
                    tipoUltimaOperacion = 1;
                    numerosDespuesUltOp = subEcuacion.Length - i;
                }
                else if (subEcuacion[i] == '+')
                {
                    tipoUltimaOperacion = 2;
                    numerosDespuesUltOp = subEcuacion.Length - i;
                }
                else if (subEcuacion[i] == '-')
                {
                    tipoUltimaOperacion = 3;
                    numerosDespuesUltOp = subEcuacion.Length - i;
                }
                //Bucle para pasar por cada una de las operaciones
            }
            for (int j = 0; j < 4; j++)
            {
                subEcuacion = realizarEcSub(subEcuacion, j, tipoUltimaOperacion, numerosDespuesUltOp);

               
            }
            Console.WriteLine(subEcuacion + " subEcuacion");
            return "hola";
        }


        public static string realizarEcSub(string subEcuacion, int numeroOP, int operacionFinalTipo, int numAntesOperacionFinal)
        {
            int indexOpFinal = subEcuacion.Length - numAntesOperacionFinal;
            int finalEcuacion = subEcuacion.Length - 1;
            string resultado, operacionRealizada, resultadoFinal;
            int posicionOperacionActual = 0;
            bool operacionEncontrada = false;
            char[] operacion = { '*', '/', '+', '-' };


            //Contador de numeros antes y despues de la operacion
            int contadorNumeros = 0;
            int numerosAntes = 0;

            int contadorOperaciones = 0;

            //Pasa por cada char de la ecuacion
            for (int i = 0; i < subEcuacion.Length; i++)
            {
                //Si el caracter es digito, se lo agrega en el contador
                if (Char.IsDigit(subEcuacion, i))
                {

                    contadorNumeros++;


                    if (i == subEcuacion.Length) //else if (operacionEncontrada && i == '6')
                    {

                        try
                        {
                            int numeroAnterior = int.Parse(subEcuacion.Substring(posicionOperacionActual - numerosAntes, numerosAntes));
                            int numeroSiguiente = int.Parse(subEcuacion.Substring(posicionOperacionActual + 1));
                            int resultadoOperacion = 0;


                            switch (numeroOP)
                            {
                                case 0:
                                    resultadoOperacion = numeroAnterior * numeroSiguiente;
                                    return Convert.ToString(resultadoOperacion);
                                    break;
                                case 1:
                                    resultadoOperacion = numeroAnterior / numeroSiguiente;
                                    return Convert.ToString(resultadoOperacion);
                                    break;
                                case 2:
                                    resultadoOperacion = numeroAnterior + numeroSiguiente;

                                    return Convert.ToString(resultadoOperacion);
                                    break;
                                case 3:
                                    resultadoOperacion = numeroAnterior - numeroSiguiente;
                                    return Convert.ToString(resultadoOperacion);
                                    break;
                            }
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine(e.ToString());
                            Console.WriteLine("SOY BASURA");
                        }

                        

                    }





                }
                else if (subEcuacion[i] == operacion[numeroOP] && !operacionEncontrada) //Si el caracter es igual a la operacion con la que se esta trabajando
                {
                    posicionOperacionActual = i; //Guardar la posicion de la operacion
                    operacionEncontrada = true;  //Marcar la operacion como encontrada
                    numerosAntes = contadorNumeros;
                    contadorNumeros = 0;
                }
                else if (operacionEncontrada && !Char.IsDigit(subEcuacion, i) && subEcuacion[i] != 'p')
                {


                    int numeroAnterior = int.Parse(subEcuacion.Substring(posicionOperacionActual - numerosAntes, numerosAntes));
                    int numeroSiguiente = int.Parse(subEcuacion.Substring(posicionOperacionActual + 1, contadorNumeros));
                    int resultadoOperacion = 0;
                    contadorOperaciones++;


                    switch (numeroOP)
                    {
                        case 0:
                            resultadoOperacion = numeroAnterior * numeroSiguiente;
                            break;
                        case 1:
                            resultadoOperacion = numeroAnterior / numeroSiguiente;
                            break;
                        case 2:
                            resultadoOperacion = numeroAnterior + numeroSiguiente;
                            break;
                        case 3:
                            resultadoOperacion = numeroAnterior - numeroSiguiente;
                            break;
                    }
                    // desde 0 hasta el principio de la operacion, el resultado operacion y lo que de la operacion

                    if (posicionOperacionActual - numerosAntes == 0)
                    {
                        operacionRealizada = resultadoOperacion + subEcuacion.Substring(posicionOperacionActual + contadorNumeros + 1);
                    }
                    else
                    {
                        operacionRealizada = subEcuacion.Substring(0, posicionOperacionActual - numerosAntes - 1) + resultadoOperacion + subEcuacion.Substring(posicionOperacionActual + contadorNumeros + 2);
                    }

                    resultadoFinal = realizarEcSub(operacionRealizada, numeroOP, operacionFinalTipo, numAntesOperacionFinal);
                    return resultadoFinal;


                }
                else
                {
                    contadorNumeros = 0;
                }

                //si esta es la operacion en el ultimo lugar, se realiza de manera diferente con los parametros pasados al final
                if (i == indexOpFinal && numeroOP == operacionFinalTipo){
                    try
                    {
                        Console.WriteLine(int.Parse(subEcuacion.Substring(0, indexOpFinal)) + "nUMERO ANTERIOR UWU");
                        int numeroAnterior = int.Parse(subEcuacion.Substring(0, indexOpFinal));

                        int numeroSiguiente = int.Parse(subEcuacion.Substring(indexOpFinal + 1));
                        int resultadoOperacion = 0;


                        switch (numeroOP)
                        {
                            case 0:
                                resultadoOperacion = numeroAnterior * numeroSiguiente;
                                return Convert.ToString(resultadoOperacion);
                                break;
                            case 1:
                                resultadoOperacion = numeroAnterior / numeroSiguiente;
                                return Convert.ToString(resultadoOperacion);
                                break;
                            case 2:
                                resultadoOperacion = numeroAnterior + numeroSiguiente;

                                return Convert.ToString(resultadoOperacion);
                                break;
                            case 3:
                                resultadoOperacion = numeroAnterior - numeroSiguiente;
                                return Convert.ToString(resultadoOperacion);
                                break;
                        }
                    }
                        catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        Console.WriteLine("SOY BASURA PERO DE LA ULTIMA QUE SALE");
                    }
            }
                

               




            }


            return subEcuacion;
        }



    }
}
