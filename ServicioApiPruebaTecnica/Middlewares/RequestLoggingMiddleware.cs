using ServicioApiPruebaTecnica.MyLogging;

namespace ServicioApiPruebaTecnica.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMyLogger _logger;

        public RequestLoggingMiddleware(RequestDelegate next, IMyLogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            //Extrae el nombre del usuario desde el token
            var userName = context.User.Identity.IsAuthenticated
                ? context.User.Identity.Name
                : "Anonymous";

            //Registra la solicitud entrante
            var requestLog = $"[{DateTime.UtcNow}] Request by {userName}: {context.Request.Method} {context.Request.Path}";
            _logger.Log(requestLog);

            //Llamar al siguiente middleware en la tubería
            await _next(context);

            //Registrar el estado de la respuesta
            var responseLog = $"[{DateTime.UtcNow}] Response for {userName}: {context.Response.StatusCode}";
            _logger.Log(responseLog);
        }
    }
}
