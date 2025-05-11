using System.Text;

namespace ReporterApp.Core.Reports;

public static class StringBuilderExtension
{
    public static StringBuilder RemoveAllCr(this StringBuilder sb) 
        => sb.Replace("\r", "");

    public static StringBuilder EnsureLastLfRemoved(this StringBuilder sb)
    {
        if(sb.Length == 0)
        {
            return sb;
        }

        var lastChar = sb[^1];

        if(lastChar == '\n')
        {
            return sb.Remove(sb.Length - 1, 1);
        }

        return sb;
    }
}
