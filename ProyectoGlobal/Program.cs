using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
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

            

            String ecuacion = verifEc();
            Console.WriteLine("Termine: " + ecuacion);


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

            bool verifPara;
            bool verifNeg;
            bool verifDivPorCero;

            //verifica parentesis
            ec_terminada = verifParentesis(ec_terminada);
            verifPara = contadoresDesiguales(ec_terminada);

            string ec_parentesis = ec_terminada;

            //verifica que no se repitan las operaciones +, * y /
            ec_terminada = veriOperaciones(ec_terminada);


            //verifica numeros negativos
            ec_terminada = veriNumNegativo(ec_terminada);
            verifNeg = entraVerifNegativo(ec_parentesis, ec_terminada);

            
            //verifica que no haya divisiones por 0
            verifDivPorCero = veriDivPorCero(ec_terminada);

            if (ecuacion != ec_terminada || verifPara == true || verifDivPorCero == true)
            {
                return ec_terminada = veriFinal(ec_terminada, verifPara, verifNeg, verifDivPorCero);
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

            char[] mis_num = {'1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
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

        public static string veriOperaciones(string ec)
        {

            string resultEc = "";
            int indiceUltimoNum = 0;

            char[] mis_num = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', ')' };
            char[] mis_verif = {'+', '*', '/','-'};
            char[] mis_verifSinMenos = { '+', '*', '/'};
            bool borroOp = false;

            for (int i = 0; i < ec.Length; i++)
            {
                if (mis_num.Contains(ec[i]))
                {
                    indiceUltimoNum = i;
                }
            }

            for (int i = 0; i < ec.Length; i++)
            {

                if(i > 0 && i <= indiceUltimoNum)
                {
                    if (mis_verifSinMenos.Contains(ec[i]) && mis_verif.Contains(ec[i - 1]))
                    {
                        borroOp = true;
                    }
                    else if (ec[i] == '-')
                    {
                        if ((mis_verif.Contains(ec[i - 1]) && mis_verif.Contains(ec[i + 1])) )
                        {
                            borroOp= true;
                        }
                    }
                }
                else if(i == 0)
                {
                    if (mis_verifSinMenos.Contains(ec[i]))
                    {
                        borroOp = true;
                    }
                }
                else
                {
                    borroOp = true;
                }

                if (borroOp == false)
                {
                    resultEc += ec[i];
                }
                else
                {
                    borroOp = false;
                }
                
            }


            Console.WriteLine(Environment.NewLine + "ASI QUEDA EN VERIF OPERACIONES: " + resultEc);
            return resultEc;
        }


        public static string veriFinal(string ec, bool contadores, bool verifNeg, bool verifDivPorCero)             
        {

            string car;
            string result = null;

            if (contadores == true)
            {
                Console.WriteLine("FALTA COLOCAR APRENTESIS!!");
                Console.WriteLine("Intente de nuevo");
                ec = verifEc();
                result = ec;
            }
            else if (verifNeg == true)
            {
                Console.WriteLine("Hay aprentesis que faltan en numeros negativos y van a ser agregados");
                Console.WriteLine(Environment.NewLine + "La ecuacion quedará asi: " + ec + Environment.NewLine);
                Console.WriteLine("Esta de acuerdo? Y = SI, N = NO");

                car = Console.ReadLine().ToUpper();

                result = verifCondicion(car, result, ec);
            }
            else if (verifDivPorCero)
            {
                Console.WriteLine("NO SE PUEDE DIVIDIR POR CERO!!");
                Console.WriteLine("Intente de nuevo");
                ec = verifEc();
                result = ec;
            }
            else
            {
                Console.WriteLine("Hay caracteres que no pertenecen a la ecuacion o estan mal colocados, van a ser eliminados o cambiados");
                Console.WriteLine(Environment.NewLine + "La ecuacion quedará asi: " + ec + Environment.NewLine);
                Console.WriteLine("Esta de acuerdo? Y = SI, N = NO");

                car = Console.ReadLine().ToUpper();

                result = verifCondicion(car, result, ec);
                
            }

            return result;

        }


        public static string verifCondicion(string car, string result, string ec)
        {

            if (car == "Y")
            {
                Console.WriteLine("Calculando...");                                                         //<----- ENTRAN LOS CALCULOS
                result = ec;
            }
            else if (car == "N")
            {
                ec = verifEc();
                result = ec;

            }
            else
            {
                Console.WriteLine("No ingresó ni 'Y' ni 'N', intente de nuevo");
                car = Console.ReadLine().ToUpper();
                verifCondicion(car,result,ec);

            }

            return result;

        }

        public static bool veriDivPorCero(string ec)                            //<---ERROR de indice fuera de rango, arreglar
        {
            char[] mis_num = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            char[] mis_numCero = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int indexFueraDeRango = 0;


            for (int i = 0; i < ec.Length; i++)
            {
                if (mis_numCero.Contains(ec[i]))
                {
                    indexFueraDeRango = i;
                }
            }

            for (int i = 0; i < ec.Length; i++)
            {
                if (ec[i] == '0' && i < indexFueraDeRango && i > 0)
                {
                    if (ec[i - 1] == '/' && !mis_num.Contains(ec[i + 1]))
                    {
                        return true;
                    }
                }
                else if (ec[i] == '0' && i == indexFueraDeRango)
                {
                    if (ec[i - 1] == '/')
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string hacerCalculo(string ecuacion)
        {
            bool parentesisDeNeg = false;
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
                    //Si lo siguiente al parentesis abrir es un menos, entonces hay que tomarlo como numero negativo
                    if (parentesisNumsNegativos(ecuacion, i))
                    {
                        Console.WriteLine("ENTRE EN BUCLE NEGATIVOS");
                        String numeroNegativo;
                        int contadorNumeros = 0;

                        bool cierreNegEncontrado = false;

                        //Se pasa por el numero negativo para ver la cantidad de digitos
                        for (int j = i + 2; j < ecuacion.Length; j++)
                        {
                            if (ecuacion[j] != ')' && !cierreNegEncontrado)
                            {
                                Console.WriteLine("ª");
                                contadorNumeros++;

                            }
                            else
                            {
                                cierreNegEncontrado = true;
                            }
                            
                            
                            
                        }
                        //Se guarda un substring con el numero negativo, ya con n adelante y sin parentesis
                        numeroNegativo = "n" + ecuacion.Substring(i + 2, contadorNumeros);

                        Console.WriteLine(numeroNegativo + " SOY UN NUMERO NEGATIVO");


                        //Se guarda la nueva ecuacion con el numero negativo reemplazando el de antes
                        ecuacion = ecuacion.Substring(0, i)+numeroNegativo+ecuacion.Substring(i+contadorNumeros+3);

                        Console.WriteLine(ecuacion + " SOY LA ECUACION CON UN NUMERO NEGATIVO");
                    }
                    else
                    {
                        numeroParentesis++;
                        //Si encuentra un parentesis abierto, lo guarda en un array y de valor va el index
                        prioridadParentesisAbrir[numeroParentesis] = i;
                        //Console.WriteLine("index de (: " + i);

                        //Se declara no encontrado el index del numero del par
                        parNoEncontrado[numeroParentesis] = true;

                    }
                    

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


            string miniEcuacion;

               

            if (numeroParentesis >= 0)
            {
                //Contar el tamaño que tendra el substring dentro del mayor par de parentesis
                int substringLenght = prioridadParentesisCerrar[numeroParentesis] - prioridadParentesisAbrir[numeroParentesis] - 1;

                //Nuevo substring para miniecuaciones
                miniEcuacion = ecuacion.Substring(prioridadParentesisAbrir[numeroParentesis] + 1, substringLenght);
                String nuevaEcuacion = realizarEcSubOperaciones(miniEcuacion);
                miniEcuacion = ecuacion.Substring(0, prioridadParentesisAbrir[numeroParentesis]) + nuevaEcuacion + ecuacion.Substring(prioridadParentesisCerrar[numeroParentesis] + 1);
 
                miniEcuacion = hacerCalculo(miniEcuacion);
                return miniEcuacion;
            }
            else
            {
                miniEcuacion = ecuacion;
                miniEcuacion = realizarEcSubOperaciones(miniEcuacion);
                
            }

            return miniEcuacion;    

 
   

        }

        public static String realizarEcSubOperaciones(String subEcuacion)
        {
            obtenerDatosUltOp(subEcuacion);
            
            for (int j = 0; j < 4; j++)
            {
                subEcuacion = realizarEcSub(subEcuacion, j);
                Console.WriteLine(subEcuacion + " subEcuacion");

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
                if (Char.IsDigit(subEcuacion, i) || subEcuacion[i] == '.')
                {

                    contadorNumeros++;


                    if (i == subEcuacion.Length) //else if (operacionEncontrada && i == '6')
                    {
                        //Retorna la ecuacion hecha si es la ultima
                        return siOPEsFinal(subEcuacion, numeroOP, posicionOperacionActual, numerosAntes);

                    }


                }
                else if (subEcuacion[i] == operacion[numeroOP] && !operacionEncontrada) //Si el caracter es igual a la operacion con la que se esta trabajando
                {
                    
                    posicionOperacionActual = i; //Guardar la posicion de la operacion
                    operacionEncontrada = true;  //Marcar la operacion como encontrada
                    numerosAntes = contadorNumeros;
                    contadorNumeros = 0;
                }
                else if (operacionEncontrada && (!Char.IsDigit(subEcuacion, i) || !(subEcuacion[i] == '.')) && subEcuacion[i] != 'p')
                {

                    contadorOperaciones++;

                    operacionRealizada = opNoUltima(subEcuacion, posicionOperacionActual, numerosAntes, contadorNumeros, numeroOP);

                    Console.WriteLine(operacionRealizada + " OPERACION INTERMEDIA REALIZADA");
                    resultadoFinal = realizarEcSub(operacionRealizada, numeroOP);
                    return resultadoFinal;


                }
                else
                {
                    contadorNumeros = 0;
                }

                //si esta es la operacion en el ultimo lugar, se realiza de manera diferente con los parametros pasados al final
                if (i == indexOpFinal && numeroOP == operacionFinalTipo){

                    //Retorna la ecuacion hecha si es la ultima
                    return funcionOPultima(subEcuacion, posicionOperacionActual, numerosAntes, contadorNumeros, numeroOP, indexOpFinal);
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



        public static string siOPEsFinal(String subEcuacion, int numeroOP, int posicionOperacionActual, int numerosAntes)
        {
            try
            {
                float numeroAnterior = float.Parse(subEcuacion.Substring(posicionOperacionActual - numerosAntes, numerosAntes));
                float numeroSiguiente = float.Parse(subEcuacion.Substring(posicionOperacionActual + 1));
                float resultadoOperacion = 0;


                switch (numeroOP)
                {
                    case 0:
                        resultadoOperacion = numeroAnterior * numeroSiguiente;
                        return Convert.ToString(resultadoOperacion);
                        break;
                    case 1:
                        if (numeroSiguiente == 0)
                        {
                            return "NO SE PUEDE DIVIDIR POR 0";

                        }
                        else
                        {
                            resultadoOperacion = numeroAnterior / numeroSiguiente;
                            break;
                        }
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
            }

            return "SLAENLC";
        }
        

        public static String opNoUltima(String subEcuacion, int posicionOperacionActual, int numerosAntes, int contadorNumeros, int numeroOP)
        {
            float numeroAnterior = float.Parse(subEcuacion.Substring(posicionOperacionActual - numerosAntes, numerosAntes));
            float numeroSiguiente = float.Parse(subEcuacion.Substring(posicionOperacionActual + 1, contadorNumeros));
            float resultadoOperacion = 0;

            String operacionRealizada;


            switch (numeroOP)
            {
                case 0:
                    resultadoOperacion = numeroAnterior * numeroSiguiente;
                    break;
                case 1:
                    if(numeroSiguiente == 0)
                    {
                        return "NO SE PUEDE DIVIDIR POR 0";
                        break;
                    }
                    else
                    {
                        resultadoOperacion = numeroAnterior / numeroSiguiente;
                        break;
                    }
                    
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

            return operacionRealizada;
        }





        public static String funcionOPultima(String subEcuacion, int posicionOperacionActual, int numerosAntes, int contadorNumeros, int numeroOP, int indexOpFinal)
        {
            try
            {
                Console.WriteLine(subEcuacion.Substring(indexOpFinal - numerosAntes, numerosAntes) + "nUMERO ANTERIOR UWU");
                float numeroAnterior = float.Parse(subEcuacion.Substring(indexOpFinal - numerosAntes, numerosAntes));

                float numeroSiguiente = float.Parse(subEcuacion.Substring(indexOpFinal + 1));
                float resultadoOperacion = 0;

                String operacionRealizada;


                switch (numeroOP)
                {
                    case 0:
                        resultadoOperacion = numeroAnterior * numeroSiguiente;

                        break;
                    case 1:
                        if (numeroSiguiente == 0)
                        {
                            return "NO SE PUEDE DIVIDIR POR 0";
                            
                        }
                        else
                        {
                            resultadoOperacion = numeroAnterior / numeroSiguiente;
                            break;
                        }

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
                    Console.WriteLine(resultadoOperacion + " ULTIMA OPERACION REALIZADA");
                    return Convert.ToString(resultadoOperacion);

                }
                else
                {
                    operacionRealizada = subEcuacion.Substring(0, indexOpFinal - numerosAntes) + resultadoOperacion;
                    Console.WriteLine(operacionRealizada + " ULTIMA OPERACION REALIZADA");
                    return operacionRealizada;
                }
            }


            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }
            return "SEAMCLC";
        }







        public static bool parentesisNumsNegativos(String miniEcuacion, int indexParentesis)
        {
            Console.WriteLine(miniEcuacion + " ESTOY SIENDO PROCESADA PARA VER SI TENGO NEGATIVOS");
            if (miniEcuacion[indexParentesis+1] == '-')
            {
                Console.WriteLine("que siiiiiiiiiiiiiiiii");
                return true;
            }

            return false;

        }




    }
}
