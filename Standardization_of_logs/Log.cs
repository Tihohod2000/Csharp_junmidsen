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
            @"^(?<date>\d{4}-\d{2}-\d{2}|\d{2}\.\d{2}\.\d{4})\s+" +
            @"(?<time>\d{2}:\d{2}:\d{2}(?:\.\d{1,4})?)\s*" +
            @"(?:\s*\|\s*[^|]*)*?" +
            @"(?<level>INFORMATION|INFO|WARNING|WARN|ERROR|DEBUG)\b\s*" +
            @"(?:\s*\|\s*[^|]*)*" + 
            @"\s*(?<message>.+)$",
            RegexOptions.ExplicitCapture);
    
        return regex.IsMatch(log);
    }
}