namespace Standardization_of_logs;

class Program
{
    static void Main()
    {
        string[] lines;
        lines = File.ReadAllLines("logsInput.txt");
        // string[] logs = 
        // [
        //     "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'",
        //     "10.03.2025 15:14:49.523 WARNING Версия программы: '3.4.0.48729'",
        //     "10.03.2025 15:14:49.523 ERROR Версия программы: '3.4.0.48729'",
        //     "10.03.2025 15:14:49.523 DEBUG Версия программы: '3.4.0.48729'",
        //     "10.03.2 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'",
        //     "10.03.2025 15:14:49.523 dsN Версия программы: '3.4.0.48729'",
        //     "10.03.2025 15:14:49.523 INFORMATION skjdgflмы: '3.4.0.48729'",
        //     "2025-03-10 15:14:51.5882 | INFO|11|MobileComputer.GetDeviceId | Код устройства: '@MINDEO-M40-D-410244015546'",
        //     "MobileComputer.GetDeviceId | INFO|11|2025-03-10 15:14:51.5882 | Код устройства: '@MINDEO-M40-D-410244015546'"
        // ];

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