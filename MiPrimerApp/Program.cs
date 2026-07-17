namespace MiPrimerApp;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("LECCION 2: Entrada de datos");
        Console.Write("Escribe tu nombre: ");
        string nombreUsuario = Console.ReadLine() ?? "";



        int edadUsuario;

        while (true)
        {
            Console.Write("Escribe tu edad: ");
            bool edadValid = int.TryParse(Console.ReadLine(), out edadUsuario);
            if (edadValid)
            {
                break;
            }
            Console.WriteLine("La edad no es valida. Intenta de nuevo.");
        }
        Console.Write("Escribe tu ciudad: ");
        string ciudadUsuario = Console.ReadLine() ?? "";

        Console.WriteLine($"Hola, {nombreUsuario}. tienes {edadUsuario} años y Vives en {ciudadUsuario}.");
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
