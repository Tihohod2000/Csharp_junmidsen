using System.Globalization;
using System.Text.RegularExpressions;

namespace Standardization_of_logs;

public class Log
{
    private string? _data;
    private string? _lvlLog;
    private string _method = "DEFAULT";
    private string? _message;
    
    
    private readonly Regex _dataTimeRegex = new Regex(
        @"(?<date>\d{4}-\d{2}-\d{2}|\d{2}\.\d{2}\.\d{4})\s+(?<time>\d{2}:\d{2}:\d{2}(?:\.\d{1,})?)\s*");

    private readonly Regex _levelRegex = new Regex(@"(?<level>INFO\w*|WARN\w*|ERROR|DEBUG)\b\s*");

    private readonly Regex _methodRegex = new Regex(@"(?<method>[^|][A-Za-z]+[.]+[A-Za-z]+)\s*");

    private readonly Regex _messageRegex = new Regex(@"(?<message>[А-Яа-я]+\s+[А-Яа-я]+[:]+.*)$");

    private string? LvlLog
    {
        get => _lvlLog;
        set => _lvlLog = NormalizeLevel(value);
    }

    private static string? NormalizeLevel(string? level)
    {
        return level switch
        {
            "INFORMATION" => "INFO",
            "WARNING" => "WARN",
            _ => level
        };
    }

    private Match[] TryParseInfo(string log)
    {
        Match[] matches = 
        [
            _dataTimeRegex.Match(log),
            _levelRegex.Match(log),
            _methodRegex.Match(log),
            _messageRegex.Match(log)
        ];

        return matches;
    }

    private bool IsValid(string? log)
    {
        if (log == null) return false;
        
        Match[] arrayMatch = TryParseInfo(log);

        int countCorrect = 0;
        //проверяет хватает ли данных для валидации
        foreach (var match in arrayMatch)
        {
            if (match.Success) countCorrect++;
        }

        if (countCorrect >= 3) return true;
        return false;
    }

    private void ParseFromLog(string? line)
    {
        if (line == null) return ;
        
        Match[] arrayMatch = TryParseInfo(line);
        
        string[] formats = {
            "yyyy-MM-dd",
            "dd.MM.yyyy"
        };

        if (DateTime.TryParseExact(arrayMatch[0].Groups["date"].Value,
                formats, CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime result
            ))
        {
            _data = result.ToString("dd-MM-yyyy") + $"\t{arrayMatch[0].Groups["time"]}";
        }
        
        LvlLog = arrayMatch[1].Groups["level"].Value;
        
        if (arrayMatch[2].Groups["method"].Success)
        {
            _method = arrayMatch[2].Groups["method"].Value;
        }

        _message = arrayMatch[3].Groups["message"].Value;
    }


    public void CheckLog(string line)
    {
        try
        {
            //запись в файл problems.txt
            if (!IsValid(line))
            {
                WriteInFile(false, line);
                return;
            }


            ParseFromLog(line);
            //запись в файл logs.txt
            if (_message != null && _data != null && LvlLog != null)
            {
                string[] infoOfCorrectLog = [_data, LvlLog, _method, _message];
                string correctLog = string.Join("\t", infoOfCorrectLog);
                WriteInFile(true, correctLog);
            }
        }
        catch
        {
            // ignored
        }
    }


    private static void WriteInFile(bool correct, string line)
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
            string filePath = "logs.txt";
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                writer.WriteLine(line);
            }
        }
    }
}