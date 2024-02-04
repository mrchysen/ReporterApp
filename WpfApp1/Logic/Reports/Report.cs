using System.Text;

namespace KrasTechMontacApplication.Logic.Reports;

/// <summary>
/// Report class
/// </summary>
public class Report
{
    public string Separator { get; set; } = "\n\n";
    public string Title { get; set; } = string.Empty;
    public string BasePart { get; set; } = string.Empty;
    public override string ToString()
    {
        return new StringBuilder()
                   .Append(Title)
                   .Append(Separator)
                   .Append(BasePart)
                   .ToString();
    }
    
}
