namespace Compressor;


class Program
{
    static void Main(string[] args)
    {
        start:


        Console.WriteLine("Выберите действие:");
        Console.WriteLine("1. Компрессия строки");
        Console.WriteLine("2. Декомпрессия строки");

        string? userInputAction = Console.ReadLine();
        string userLine;

        Compressor comm = new Compressor();

        string? result;
        switch (Convert.ToInt64(userInputAction))
        {
            case 1:
                Console.WriteLine("Вы ввели 1");
                Console.WriteLine("Введите строку");
                userLine = Console.ReadLine();
                result = comm.Compression(userLine);
                Console.WriteLine(result);
                break;
            case 2:
                Console.WriteLine("Вы ввели 2");
                Console.WriteLine("Введите строку");
                userLine = Console.ReadLine();
                result = comm.Decompression(userLine);
                Console.WriteLine(result);
                break;
            default:
                Console.WriteLine("Вы ввели некорректное значение");
                break;
        }

        do
        {
            Console.WriteLine("Закончить работу? (Y/Да | N/Нет)");
            string exit = Console.ReadLine().ToLower();
            if (exit == "y")
            {
                goto exitLoop;
            }

            if (exit != "n")
            {
                Console.WriteLine("Некорректное значение");
            }
            else
            {
                goto start;
            }
        } while (true);

        exitLoop:

        Console.WriteLine("Конец");
    }
}