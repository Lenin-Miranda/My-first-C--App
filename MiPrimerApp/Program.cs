namespace MiPrimerApp;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("LECCION 1: Sintaxis basica de C#");
        Console.WriteLine();

        string nombre = "Juan";
        int edad = 20;
        double altura = 1.72;
        bool leGustaProgramar = true;
        char inicial = 'J';
        string holaMundo = "Hola mundo";

        // Ejercicio: muestra aqui las variables usando Console.WriteLine(...).
        Console.WriteLine($"Tu altura es {altura}");
        Console.WriteLine($"Te gusta: {leGustaProgramar}");
        Console.WriteLine($"Tu inicial es: {inicial}");
        Console.WriteLine($"Mi primer {holaMundo}");

        const int diasDeLaSemana = 7;
        int numeroA = 10;
        int numeroB = 3;

        Console.WriteLine("2. Operadores");
        Console.WriteLine($"Constante dias de la semana: {diasDeLaSemana}");
        Console.WriteLine($"{numeroA} + {numeroB} = {numeroA + numeroB}");
        Console.WriteLine($"{numeroA} - {numeroB} = {numeroA - numeroB}");
        Console.WriteLine($"{numeroA} * {numeroB} = {numeroA * numeroB}");
        Console.WriteLine($"{numeroA} / {numeroB} = {numeroA / numeroB}");
        Console.WriteLine($"{numeroA} % {numeroB} = {numeroA % numeroB}");
        Console.WriteLine();

        Console.WriteLine("3. Condicional if");
        if (edad >= 18)
        {
            Console.WriteLine("Eres mayor de edad.");
        }
        else
        {
            Console.WriteLine("Eres menor de edad.");
        }

        Console.WriteLine();
        Console.WriteLine("4. Condicional switch");
        string colorFavorito = "azul";

        switch (colorFavorito)
        {
            case "azul":
                Console.WriteLine("Tu color favorito es azul.");
                break;
            case "rojo":
                Console.WriteLine("Tu color favorito es rojo.");
                break;
            default:
                Console.WriteLine("Tienes otro color favorito.");
                break;
        }

        Console.WriteLine();
        Console.WriteLine("5. Bucle for");
        for (int i = 1; i <= 3; i++)
        {
            Console.WriteLine($"Vuelta numero {i}");
        }

        Console.WriteLine();
        Console.WriteLine("6. Bucle while");
        int contador = 1;

        while (contador <= 3)
        {
            Console.WriteLine($"Contador actual: {contador}");
            contador++;
        }

        Console.WriteLine();
        Console.WriteLine("7. Metodos");
        int resultadoSuma = Sumar(4, 6);
        string saludo = CrearSaludo(nombre);

        Console.WriteLine($"4 + 6 = {resultadoSuma}");
        Console.WriteLine(saludo);
        Console.WriteLine();

        Console.WriteLine("Practica sugerida:");
        Console.WriteLine("- Cambia los valores de las variables.");
        Console.WriteLine("- Agrega una multiplicacion nueva.");
        Console.WriteLine("- Crea tu propio metodo llamado Restar.");
    }

    private static int Sumar(int numero1, int numero2)
    {
        return numero1 + numero2;
    }

    private static string CrearSaludo(string nombre)
    {
        return $"Hola, {nombre}. Bienvenido a C#.";
    }
}
