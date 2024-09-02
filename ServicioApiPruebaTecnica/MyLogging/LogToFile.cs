using System;
using System.IO;

namespace ServicioApiPruebaTecnica.MyLogging
{
    public class LogToFile : IMyLogger
    {
        private readonly string _filePath;

        public LogToFile(string filePath)
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            try
            {
                // Formatear el mensaje con la fecha y hora actuales
                var logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

                // Escribir el mensaje en el archivo
                using (StreamWriter writer = new StreamWriter(_filePath, true))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al escribir en el archivo de log: {ex.Message}");
            }
        }
    }
}
