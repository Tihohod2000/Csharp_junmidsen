using System.Globalization;
using System.Text.RegularExpressions;

namespace Standardization_of_logs;

public class Log
{
    public DateTime data { get; set; }
    public string lvlLog { get; set; }
    public string method = "DEFAULT";
    public string message { get; set; }


    public bool IsValid(string? log)
    {
        if (log == null) return false;

        var regex = new Regex(
            @"(?<datetime>(?<date>\d{4}-\d{2}-\d{2}|\d{2}\.\d{2}\.\d{4})\s+" +
            @"(?<time>\d{2}:\d{2}:\d{2}(?:\.\d{1,4})?))\s*" +
            @"(?:\s*\|\s*[^|]*)*?" +
            @"(?<level>INFORMATION|INFO|WARNING|WARN|ERROR|DEBUG)\b\s*" +
            @"(?:\s*\|\s*(?<method>[A-Za-z0-9_.]+)\s*)*" +
            @"(?:\s*\|\s*|\s*)(?<message>.+)$",
            RegexOptions.ExplicitCapture);

        var match = regex.Match(log);

        Console.WriteLine(match.Groups["method"].Value);
        // Console.WriteLine(String.Join(" ",match.Groups.Keys));

        return regex.IsMatch(log);
    }

    public void ParseFromLog(string? log)
    {
        // if (log == null) return ;
        // // if (log == null) return false;
        // if (!IsValid(log))
        // {
        //     return;
        // }

        var regex = new Regex(
            @"(?<datetime>(?<date>\d{4}-\d{2}-\d{2}|\d{2}\.\d{2}\.\d{4})\s+" +
            @"(?<time>\d{2}:\d{2}:\d{2}(?:\.\d{1,4})?))\s*" +
            @"(?:\s*\|\s*[^|]*)*?" +
            @"(?<level>INFORMATION|INFO|WARNING|WARN|ERROR|DEBUG)\b\s*" +
            @"(?:\s*\|\s*(?<method>[A-Za-z0-9_.]+)\s*)*" +
            @"\s*[|]*(?<message>\s*.[^|]+)$",
            RegexOptions.ExplicitCapture);

        var match = regex.Match(log);
        // IEnumerable<string> groupsMatch = match.Groups.Keys;

        string[] formats = {
            "yyyy-MM-dd HH:mm:ss.ffff",
            "dd.MM.yyyy HH:mm:ss.fff"
        };

        if (DateTime.TryParseExact(match.Groups["datetime"].Value,
                formats, CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime result
            ))
        {
            data = result;
        }
        
        // data = DateTime.ParseExact(
        //     match.Groups["datetime"].Value,
        //     "dd.MM.yyyy HH:mm:ss.fff",
        //     CultureInfo.InvariantCulture);
        lvlLog = match.Groups["level"].Value;
        if (match.Groups["method"].Success)
        {
            method = match.Groups["method"].Value;
        }

        message = match.Groups["message"].Value;
    }


    public void CheckLog(string log)
    {
        try
        {
            if (!IsValid(log))
            {
                writeInFile(false, log);
                
                
                return;
            }


            ParseFromLog(log);
            string[] arrayOfCorrectLog = new[] { data.ToString(), lvlLog.ToString(), method.ToString(), message.ToString() };
            string correctLog = string.Join(" ", arrayOfCorrectLog);
            writeInFile(true, correctLog);
            //Тут делать запись в файл Logs.txt    
        }
        catch
        {
            // ignored
        }
    }


    public void writeInFile(bool correct, string line)
    {
        if (!correct)
        {
            string filePath = "problems.txt";
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(line);
            }
        }
        else
        {
            string filePath = "Logs.txt";
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(line);
            }
        }
    }
}