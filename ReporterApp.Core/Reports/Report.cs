using System.Text;

namespace ReporterApp.Core.Reports;

public class Report
{
    public Report()
    {
        AdditionalInfo = new();
    }

    public Report(string title, string body, List<string>? additionalInfo = null)
    {
        Title = title;
        Body = body;

        AdditionalInfo = additionalInfo ?? new();
    }

    public string Title { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public List<string> AdditionalInfo { get; set; }

    public override string ToString()
    {
        var sb = new StringBuilder()
            .Append(Title)
            .AppendLine("\n")
            .Append(Body);

        if (AdditionalInfo.Count > 0)
        {
            sb.AppendLine()
              .Append(string.Join('\n', AdditionalInfo));
        }

        return sb.ToString();
    }
};