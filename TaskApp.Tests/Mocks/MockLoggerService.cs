using TaskApp.Services;
using System.Collections.Generic;

public class MockLoggerService : LoggerService
{
    public List<string> Logs { get; } = new List<string>();

    public override void Log(string message)
    {
        Logs.Add(message);
    }
}
