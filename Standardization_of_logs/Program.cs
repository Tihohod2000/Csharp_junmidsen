namespace Standardization_of_logs;

class Program
{
    /// <summary>
    /// This's program for standardization of logs
    /// </summary>
    /// <param name="args">need path of file with logs</param>
    static void Main(string[] args)
    {
        if (args.Length <= 0)
        {
            Console.WriteLine("В качестве аргумента нужно передайте путь к файлу");
            return;
        }
        
        // Console.WriteLine(arg[0]);
        // Console.WriteLine("");
        string[] lines = File.ReadAllLines(args[0]);

        if (lines.Length < 1)
        {
            Console.WriteLine("Error!!! Файл пуст!!!");
            return;
        }

        foreach (var l in lines)
        {
            Log log = new Log();
            log.CheckLog(l);
        }
        Console.WriteLine("Все корректные логи записаны в файл logs.txt");
        Console.WriteLine("Все некорректные логи записаны в файл problem.txt");
        

    }
}