namespace MiPrimerApp;

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("Hola, bienvenido a tu primera app en C#.");
        Console.Write("Como te llamas? ");
        string? nombre = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nombre))
        {
            nombre = "estudiante";
        }

        int numeroFavorito = PedirNumero("Escribe un numero entero mayor que 0: ");

        Console.WriteLine();
        Console.WriteLine($"Mucho gusto, {nombre}.");
        Console.WriteLine($"El cuadrado de {numeroFavorito} es {numeroFavorito * numeroFavorito}.");

        if (numeroFavorito % 2 == 0)
        {
            Console.WriteLine("Tu numero es par.");
        }
        else
        {
            Console.WriteLine("Tu numero es impar.");
        }

        Console.WriteLine();
        Console.WriteLine($"Vamos a contar del 1 al {numeroFavorito}:");

        for (int i = 1; i <= numeroFavorito; i++)
        {
            Console.WriteLine(i);
        }

        Console.WriteLine();
        Console.WriteLine("Presiona Enter para salir.");
        Console.ReadLine();
    }

    private static int PedirNumero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int numero) && numero > 0)
            {
                return numero;
            }

            Console.WriteLine("Entrada no valida. Intenta de nuevo.");
        }
    }
}
