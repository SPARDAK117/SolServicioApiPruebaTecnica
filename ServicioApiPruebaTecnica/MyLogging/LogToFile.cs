namespace ServicioApiPruebaTecnica.MyLogging
{
    public class LogToFile : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToFile");
            //Logica para salvar Logs
        }
    }
}
