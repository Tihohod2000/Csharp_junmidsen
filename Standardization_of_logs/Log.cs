using System.Globalization;
using System.Text.RegularExpressions;

namespace Standardization_of_logs;

public class Log
{
    public DateTime data { get; set; }
    public string lvlLog { get; set; }
    public string method = "DEFAULT";
    public string message { get; set; }


    public Match[] tryParseInfo(string log)
    {
        var dataTimeRegex = new Regex(
            @"(?<datetime>(?<date>\d{4}-\d{2}-\d{2}|\d{2}\.\d{2}\.\d{4})\s+(?<time>\d{2}:\d{2}:\d{2}(?:\.\d{1,4})?))\s*", RegexOptions.ExplicitCapture);

        var levelRegex = new Regex(@"(?<level>INFO\w*|WARN\w*|ERROR|DEBUG)\b\s*");

        var methodRegex = new Regex(@"(?<method>[^|][A-Za-z]+[.]+[A-Za-z]+)\s*", RegexOptions.ExplicitCapture);

        var messageRegex = new Regex(@"(?<message>[А-Яа-я]+\s+[А-Яа-я]+[:]+.*)$");

        Match[] arrayMatch = 
        [
            dataTimeRegex.Match(log),
            levelRegex.Match(log),
            methodRegex.Match(log),
            messageRegex.Match(log)
        ];

        return arrayMatch;
    }
    
    public bool IsValid(string? log)
    {
        if (log == null) return false;
        
        Match[] arrayMatch = tryParseInfo(log);

        int countCorrect = 0;
        foreach (var Match in arrayMatch)
        {
            if (Match.Success) countCorrect++;
        }

        if (countCorrect >= 3) return true;
        return false;
    }

    public void ParseFromLog(string? log)
    {
        if (log == null) return ;
        

        var dataTimeRegex = new Regex(
            @"(?<datetime>(?<date>\d{4}-\d{2}-\d{2}|\d{2}\.\d{2}\.\d{4})\s+(?<time>\d{2}:\d{2}:\d{2}(?:\.\d{1,4})?))\s*", RegexOptions.ExplicitCapture);

        var levelRegex = new Regex(@"(?<level>INFO\w*|WARN\w*|ERROR|DEBUG)\b\s*");

        var methodRegex = new Regex(@"(?<method>[^|][A-Za-z]+[.]+[A-Za-z]+)\s*", RegexOptions.ExplicitCapture);

        var messageRegex = new Regex(@"(?<message>[А-Яа-я]+\s+[А-Яа-я]+[:]+.*)$");

        Match[] arrayMatch = tryParseInfo(log);
        
        string[] formats = {
            "yyyy-MM-dd HH:mm:ss.ffff",
            "dd.MM.yyyy HH:mm:ss.fff"
        };

        if (DateTime.TryParseExact(arrayMatch[0].Groups["datetime"].Value,
                formats, CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime result
            ))
        {
            data = result;
        }
        
        lvlLog = arrayMatch[1].Groups["level"].Value;
        if (arrayMatch[2].Groups["method"].Success)
        {
            method = arrayMatch[2].Groups["method"].Value;
        }

        message = arrayMatch[3].Groups["message"].Value;
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