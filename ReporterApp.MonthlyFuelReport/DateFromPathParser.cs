using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace ReporterApp.MonthlyFuelReport;

public static class DateFromPathParser
{
    [Pure]
    public static DateTime ExtractDateFromCarFile(FileInfo fileInfo)
    {
        if (fileInfo == null)
            throw new ArgumentNullException(nameof(fileInfo));

        // Ищет формат DD.MM.YYYY.car.json
        string pattern = @"(\d{2})\.(\d{2})\.(\d{4})\.car\.json$";
        Match match = Regex.Match(fileInfo.Name, pattern);

        if (!match.Success)
        {
            throw new FormatException(
                $"Invalid file name format. Expected: DD.MM.YYYY.car.json, but got: {fileInfo.Name}"
            );
        }

        int day = int.Parse(match.Groups[1].Value);
        int month = int.Parse(match.Groups[2].Value);
        int year = int.Parse(match.Groups[3].Value);

        return new DateTime(year, month, day);
    }
}
