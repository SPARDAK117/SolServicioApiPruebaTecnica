namespace ServicioApiPruebaTecnica.MyLogging
{
    public class LogToFile : IMyLogger
    {
        private readonly string _filePath;

        // Constructor que recibe la ruta del archivo
        public LogToFile(string filePath)
        {
            _filePath = filePath;
        }

        public void Log(string message)
        {
            // Crear el mensaje con la fecha y hora actual
            string logMessage = $"{DateTime.Now}: {message}";

            // Escribir el mensaje en la consola
            Console.WriteLine(logMessage);
            Console.WriteLine("LogToFile");

            // Lógica para salvar los logs en un archivo
            try
            {
                // Verifica si el archivo existe, si no, lo crea
                if (!File.Exists(_filePath))
                {
                    using (var stream = File.Create(_filePath)) { }
                }

                // Agregar el log al archivo
                using (StreamWriter writer = new StreamWriter(_filePath, true))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                // Manejar posibles errores de I/O
                Console.WriteLine($"Error al escribir el log en el archivo: {ex.Message}");
            }
        }
    }
}
