namespace MiPrimerApp;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("LECCION 2: Entrada de datos");
        Console.Write("Escribe tu nombre: ");
        string nombreUsuario = Console.ReadLine() ?? "";

        Console.Write("Escribe tu edad: ");
        int edadUsuario = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Escribe tu ciudad: ");
        string ciudadUsuario = Console.ReadLine() ?? "";

        Console.WriteLine($"Hola, {nombreUsuario}. tienes {edadUsuario}, Vives en {ciudadUsuario}");
        if (edadUsuario >= 18)
        {
            Console.WriteLine("Eres mayor de edad.");
        }
        else
        {
            Console.WriteLine("Eres menor de edad");
        }
    }

}
