using System.Reflection;
using PrepareData.Models;

namespace PrepareData;

public class Print
{
    public static void PrintToCSV(List<avgLogs> logs)
    {
        using (StreamWriter writer = new StreamWriter("Logs.csv"))
        {
            string data = ExportListToCSV<avgLogs>(true, logs);

            writer.Write(data);
            writer.Close();
        }
    }

    private static string FormatCSVField(string data)
    {
        return String.Format("\"{0}\"",
            data.Replace("\"", "\"\"\"")
            .Replace("\n", "")
            .Replace("\r", "")
            );
    }

    private static string ExportListToCSV<T>(bool withHeaders, IList<T> source)
    {
        System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();

        if (source == null)
            return "";

        List<string> headers = new List<string>();
        IList<PropertyInfo> props = typeof(T).GetProperties();
        List<PropertyInfo> lstPropInfo = new List<PropertyInfo>(props);

        lstPropInfo.ForEach(pi =>
        {
            headers.Add(FormatCSVField(pi.Name));
        });

        strBuilder.
            Append(String.Join(",", headers.ToArray()))
            .Append("\r\n");

        foreach (var item in source)
        {
            List<string> csvRow = new List<string>();

            foreach (var pi in props)
            {
                csvRow.Add(FormatCSVField(pi.GetValue(item, null)!.ToString()!));
            }
            strBuilder.Append(string.Join(",", csvRow.ToArray())).Append("\r\n");
        }

        return strBuilder.ToString();
    }
}
