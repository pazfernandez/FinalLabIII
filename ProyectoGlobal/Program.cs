﻿using System;
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
        public static int operacionFinalTipo { get; set; } = -1;
        public static int numAntesOperacionFinal { get; set; } = 0;
        
        static void Main(string[] args)
        {

            Console.WriteLine("Termine: " + verifEc());

            String ecuacion = verifEc();

            Console.WriteLine("Resultado de la operacion: "+hacerCalculo(ecuacion));








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
            Console.WriteLine("ASI QUEDA EN VERIF CARACTERES: " + ec_terminada);

            Boolean verifPara;
            Boolean verifNeg;

            ec_terminada = verifParentesis(ec_terminada);
            verifPara = contadoresDesiguales(ec_terminada);

            string ec_parentesis = ec_terminada;

            ec_terminada = veriNumNegativo(ec_terminada);
            verifNeg = entraVerifNegativo(ec_parentesis, ec_terminada);

            if (ecuacion != ec_terminada || verifPara == true)
            {
                return ec_terminada = veriFinal(ec_terminada, verifPara, verifNeg);
            }

            return ec_terminada;

        }

        public static string verifParentesis(string ec)                                  //Verifica la apertura y cierre de los parentesis en la ecuacion 
        {

            char[] mis_num = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', };
            char[] mis_op = { '+', '-', '*', '/', '(' };

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

                            if (ec[i - 1] == mis_num[j] || (ec[i - 1] == ')' && contB < contA))
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
            Console.WriteLine("ASI EN VERIFICACION PARENTESIS: " + resultadoEc);

            return resultadoEc;

        }

        public static Boolean contadoresDesiguales(string ec)
        {
            int contA = 0;
            int contB = 0;

            for (int i = 0; i < ec.Length; i++)
            {
                if (ec[i] == '(')
                {
                    contA++;
                }else if(ec[i] == ')')
                {
                    contB++;
                }
            }

            if(contA == contB)
            {
                return false;
            }else
            {
                return true;
            }

        }

        public static Boolean entraVerifNegativo(string ec, string ec_terminada)
        {
            
            if(ec.Length != ec_terminada.Length)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        public static string veriNumNegativo(string ec)
        {

            char[] mis_num = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            //char[] mis_verif = { '+', '-', '*', '/', '('};
            char[] mis_verif = { '+', '-', '*', '/'};

            string resultadoEc = "";
            Boolean tengoQueCerrar = false;

            int indexCerrar = 0;

            for (int i = 0; i < ec.Length; i++)
            {

                if (tengoQueCerrar == true)
                {

                    Boolean cerrar = true;

                    for (int j = 0; j < mis_num.Length; j++)
                    {
                        if (ec[i] == mis_num[j])
                        {
                            cerrar = false;
                            break;
                        }
                    }

                    if(ec[i] == '-' && (i > 0 && Char.IsDigit(ec[i - 1])))
                    {
                        cerrar = true;
                    }
                    else if (ec[i] == '-')
                    {
                        cerrar = false;
                    }

                    if (cerrar == true)
                    {
                        resultadoEc += ')';
                        tengoQueCerrar = false;
                        cerrar = false;
                    }

                }

                //---------

                if (ec[i] == '-')
                {
                    if (i == 0)
                    {
                        resultadoEc += '(';
                        tengoQueCerrar = true;

                    }
                    else if (ec[i - 1] == '(')
                    {
                        int j = i + 1;
                        while (Char.IsDigit(ec[j]))
                        {
                            j++;
                            indexCerrar = j;
                        }

                        if (ec[indexCerrar] != ')')
                        {
                            resultadoEc += '(';
                            tengoQueCerrar = true;
                        }
                        

                    }
                    else
                    {
                        for (int j = 0; j < mis_verif.Length; j++)
                        {
                            if (ec[i - 1] == mis_verif[j])
                            {
                                resultadoEc += '(';
                                tengoQueCerrar = true;
                            }
                        }
                    }
                   
                }

                resultadoEc += ec[i];

                if (i == ec.Length - 1 && tengoQueCerrar == true)
                {
                    resultadoEc += ')';
                }

            }

            Console.WriteLine(Environment.NewLine + "ASI QUEDA EN VERIF NUMERO NEGATIVO: " + resultadoEc);

            return resultadoEc;
        }


        public static string veriFinal(string ec, Boolean contadores, Boolean verifNeg)             //ERROR EN RECURSIVIDAD
        {

            string car;
            string result = null;

            if (contadores == true)
            {
                Console.WriteLine("FALTA COLOCAR APRENTESIS!!");
                Console.WriteLine("Intente de nuevo");
                //result = verifEc();
                ec = verifEc();                                                     //<---??????
            }
            else if (verifNeg == true)
            {
                Console.WriteLine("Hay aprentesis que faltan en numeros negativos y van a ser agregados");
            }
            else
            {
                Console.WriteLine("Hay caracteres que no pertenecen a la ecuacion o estan mal colocados, van a ser eliminados o cambiados");
            }

            do
            {

                Console.WriteLine(Environment.NewLine + "La ecuacion quedará asi: " + ec + Environment.NewLine);
                Console.WriteLine("Esta de acuerdo? Y = SI, N = NO");

                car = Console.ReadLine().ToUpper();
                

                if (car == "Y")
                {
                    Console.WriteLine("Calculando...");                                                         //<----- ENTRAN LOS CALCULOS
                    result = ec;
                }
                else if (car == "N")
                {
                    result = verifEc();                                                 //<--- ????????
                }
                else
                {
                    Console.WriteLine("No ingresó ni 'Y' ni 'N', intente de nuevo");
                }

            } while (car != "Y" && car != "N");

            return result;
        }


        public static string hacerCalculo(string ecuacion)
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



            if (numeroParentesis >= 0)
            {

                //Contar el tamaño que tendra el substring dentro del mayor par de parentesis
                int substringLenght = prioridadParentesisCerrar[numeroParentesis] - prioridadParentesisAbrir[numeroParentesis] - 1;

                //Nuevo substring para miniecuaciones
                string miniEcuacion = ecuacion.Substring(prioridadParentesisAbrir[numeroParentesis] + 1, substringLenght);

                //Console.WriteLine(miniEcuacion);

                String nuevaEcuacion = realizarEcSubOperaciones(miniEcuacion);
                miniEcuacion = ecuacion.Substring(0, prioridadParentesisAbrir[numeroParentesis]) + nuevaEcuacion + ecuacion.Substring(prioridadParentesisCerrar[numeroParentesis] + 1);

                //Console.WriteLine(hacerCalculo(miniEcuacion) + " MINIECUACION RECURSIVA");

                if(numeroParentesis > 0)
                {
                   // Console.WriteLine(hacerCalculo(miniEcuacion) + " MINIECUACION RECURSIVA");

                }
                else
                {
                    return realizarEcSubOperaciones(miniEcuacion);
                }
            }
            else
            {
                return realizarEcSubOperaciones(ecuacion);
            }

            return ecuacion;

        }

        public static String realizarEcSubOperaciones(String subEcuacion)
        {
            obtenerDatosUltOp(subEcuacion);
            
            for (int j = 0; j < 4; j++)
            {
                subEcuacion = realizarEcSub(subEcuacion, j);

               
            }
            //Console.WriteLine(subEcuacion + " subEcuacion");
            return subEcuacion;
        }


        public static string realizarEcSub(string subEcuacion, int numeroOP)
        {
            obtenerDatosUltOp(subEcuacion);
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

                    //Console.WriteLine(operacionRealizada + " OPERACION INTERMEDIA REALIZADA");
                    resultadoFinal = realizarEcSub(operacionRealizada, numeroOP);
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
                        //Console.WriteLine(subEcuacion.Substring(indexOpFinal - numerosAntes, numerosAntes) + "nUMERO ANTERIOR UWU");
                        int numeroAnterior = int.Parse(subEcuacion.Substring(indexOpFinal - numerosAntes, numerosAntes));

                        int numeroSiguiente = int.Parse(subEcuacion.Substring(indexOpFinal + 1));
                        int resultadoOperacion = 0;


   

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

                        if (indexOpFinal - numerosAntes == 0)
                        {
                            //Console.WriteLine(resultadoOperacion + " ULTIMA OPERACION REALIZADA");
                            return Convert.ToString(resultadoOperacion);

                        }
                        else
                        {
                            operacionRealizada = subEcuacion.Substring(0, indexOpFinal - numerosAntes) + resultadoOperacion;
                            //Console.WriteLine(operacionRealizada+" ULTIMA OPERACION REALIZADA");
                            return operacionRealizada;
                        }
                    }

                    
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());

                    }
            }
                

               




            }


            return subEcuacion;
        }





        public static void obtenerDatosUltOp(String ecuacion)
        {
            int tipoUltimaOperacion = -1;
            int numerosDespuesUltOp = 0;

            for (int i = 0; i < ecuacion.Length; i++)
            {
                if (ecuacion[i] == '*')
                {
                    tipoUltimaOperacion = 0;
                    numerosDespuesUltOp = ecuacion.Length - i;
                }
                else if (ecuacion[i] == '/')
                {
                    tipoUltimaOperacion = 1;
                    numerosDespuesUltOp = ecuacion.Length - i;
                }
                else if (ecuacion[i] == '+')
                {
                    tipoUltimaOperacion = 2;
                    numerosDespuesUltOp = ecuacion.Length - i;
                }
                else if (ecuacion[i] == '-')
                {
                    tipoUltimaOperacion = 3;
                    numerosDespuesUltOp = ecuacion.Length - i;
                }
                //Bucle para pasar por cada una de las operaciones
                operacionFinalTipo = tipoUltimaOperacion;
                numAntesOperacionFinal = numerosDespuesUltOp;
            }
        }





    }
}
