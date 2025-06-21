namespace Standardization_of_logs;

class Program
{
    static void Main(string[] args)
    {
        
        string[] logs = 
        [
            "10.03.2025 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'",
            "10.03.2025 15:14:49.523 WARNING Версия программы: '3.4.0.48729'",
            "10.03.2025 15:14:49.523 ERROR Версия программы: '3.4.0.48729'",
            "10.03.2025 15:14:49.523 DEBUG Версия программы: '3.4.0.48729'",
            "10.03.2 15:14:49.523 INFORMATION Версия программы: '3.4.0.48729'",
            "10.03.2025 15:14:49.523 dsN Версия программы: '3.4.0.48729'",
            "10.03.2025 15:14:49.523 INFORMATION skjdgflмы: '3.4.0.48729'",
            "2025-03-10 15:14:51.5882 | INFO|11|MobileComputer.GetDeviceId | Код устройства: '@MINDEO-M40-D-410244015546'",
            "MobileComputer.GetDeviceId | INFO|11|2025-03-10 15:14:51.5882 | Код устройства: '@MINDEO-M40-D-410244015546'"
        ];


        foreach (var l in logs)
        {
            Log log = new Log();
            log.CheckLog(l);
        }
        

    }
}