using Contracts;
using markdown_note_taking_app.Interfaces.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace markdown_note_taking_app.Controllers
{
    public class MarkdownController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _logger;

        public MarkdownController(IServiceManager _serviceManager)
        {
            
        }
    }
}
