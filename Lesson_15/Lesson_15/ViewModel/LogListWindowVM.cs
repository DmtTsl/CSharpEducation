using System.Collections.Generic;

namespace Lesson_15
{
    public class LogListWindowVM
    {
        public List<string> LogEntries { get; set; }
        public LogListWindowVM(IEnumerable<Log> logs)
        {
            LogEntries = new List<string>();
            foreach(Log log in logs)
            {
                LogEntries.Add(log.LogEntry(log.AccNum));
            }
        }
    }
}
