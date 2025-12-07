using System;
using System.IO;

namespace TaskApp.Services
{
    public class LoggerService
    {
        private readonly string logPath = "logs.txt";

        public void Log(string message)
        {
            string entry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            File.AppendAllText(logPath, entry + Environment.NewLine);
        }
    }
}
