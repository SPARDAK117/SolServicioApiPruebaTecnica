using ServicioApiPruebaTecnica.Data;

namespace ServicioApiPruebaTecnica.Services
{
    public interface ILogService
    {
        void Log(string username, string action);
    }

    public class LogService : ILogService
    {
        private readonly PruebaTecnicaOMCContextDB _context;

        public LogService(PruebaTecnicaOMCContextDB context)
        {
            _context = context;
        }

        public void Log(string username, string action)
        {
            var logEntry = new LogEntry
            {
                Username = username,
                Action = action,
                Timestamp = DateTime.UtcNow
            };

            _context.LogEntries.Add(logEntry);
            _context.SaveChanges();
        }
    }
}