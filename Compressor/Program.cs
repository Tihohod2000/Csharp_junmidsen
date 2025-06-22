using System.Text.RegularExpressions;

namespace Compressor;

class Program
{
    static void Main()
    {
        start:

        Console.WriteLine("Выберите действие:");
        Console.WriteLine("1. Компрессия строки");
        Console.WriteLine("2. Декомпрессия строки");
        string? userInputAction = Console.ReadLine();

        Regex latin = new Regex(@"^[A-Za-z]+$");
        Compressor comm = new Compressor();
        string? userLine;
        string? result;

        switch (Convert.ToInt64(userInputAction))
        {
            case 1:
                Console.WriteLine("Вы ввели 1");
                Console.WriteLine("Введите строку из латинских букв");
                userLine = Console.ReadLine();
                if (userLine != null)
                {
                    var match = latin.Match(userLine);
                    if (match.Success)
                    {
                        result = comm.Compression(userLine);
                        Console.WriteLine(result);
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели некорректную строку");
                        Console.WriteLine("Должны быть только латинские буквы");
                    }
                }

                break;
            case 2:
                Console.WriteLine("Вы ввели 2");
                Console.WriteLine("Введите сжатую строку");
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
            string exit = Console.ReadLine()!.ToLower();
            if (exit == "y" || exit == "да")
            {
                goto exitLoop;
            }

            if (exit != "n" || exit != "нет")
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