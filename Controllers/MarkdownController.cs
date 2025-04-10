using Contracts;
using Entities.Models;
using markdown_note_taking_app.Interfaces.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace markdown_note_taking_app.Controllers
{
    [Route("api/markdown")]
    [ApiController]
    public class MarkdownController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly ILoggerManager _logger;

        public MarkdownController(IServiceManager serviceManager, ILoggerManager logger)
        {
            _serviceManager = serviceManager;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetMarkdownFiles()
        {
            var markdownFiles = await _serviceManager.MarkdownService.GetAllMarkdownFilesAsync(trackChanges: false);

            return Ok(markdownFiles);
        }


    }
}
