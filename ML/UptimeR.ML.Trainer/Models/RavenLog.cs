namespace UptimeR.ML.Trainer.Models;

public class RavenLog
{
    public string ServiceName { get; set; } = "";
    public List<AnomalyLog> Logs { get; set; } = new();
}