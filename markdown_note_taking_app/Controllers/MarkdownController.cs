using Contracts;
using Entities.Models;
using markdown_note_taking_app.Dto;
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

        [HttpGet("{fileId:guid}")]
        public async Task<IActionResult> GetMarkdownFile(Guid fileId)
        {
            var company = await _serviceManager.MarkdownService.GetMarkdownFileAsync(fileId, trackChanges: false);

            return Ok(company);
        }

        [HttpGet]
        public async Task<IActionResult> GetMarkdownFiles()
        {
            var markdownFiles = await _serviceManager.MarkdownService.GetAllMarkdownFilesAsync(trackChanges: false);

            return Ok(markdownFiles);
        }

        [HttpGet("{fileId:guid}/html")]
        public async Task<IActionResult> GetMarkdownFileAsHtml(Guid fileId, [FromQuery] bool checkGrammar = false)
        {
            if (checkGrammar)
            {
                //Get markdownfile content
                var markdownFileDto = await _serviceManager.MarkdownService.GetMarkdownFileAsync(fileId, false);
                string markdownFileContent = markdownFileDto.FileContent;

                //check grammar
                string markdownFileContentChecked = await _serviceManager.GrammarCheckService.CheckGrammarMarkdownAsync(markdownFileContent);
                var markdownFileDtoChecked = markdownFileDto with { FileContent = markdownFileContentChecked };

                //convert to html
                var markdown_html_dto = _serviceManager.MarkdownService.ConvertMarkdownFileDtoToHtml(markdownFileDtoChecked);

                return Ok(markdown_html_dto);

            }
            else
            {
                var markdownFileConvertToHtmlDto = await _serviceManager.MarkdownService.GetMarkdownFileAsHtmlAsync(fileId, trackChanges: false);
                return Ok(markdownFileConvertToHtmlDto);
            }
            
        }

        [HttpGet("{fileId:guid}/html/grammar")]
        public async Task<IActionResult> GetMarkdownFileAsHtmlAndGrammarChecked(Guid fileId)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> UploadMarkdownFile([FromForm] MarkdownFileUploadDto markDownFile)
        {
            var MarkdownFileDto = await _serviceManager.MarkdownService.CreateMarkdownFileAsync(markDownFile);

            return Ok(MarkdownFileDto);
        }

        [HttpDelete("{fileId:guid}")]
        public async Task<IActionResult> DeleteMarkdownFile(Guid fileId)
        {
            await _serviceManager.MarkdownService.DeleteMarkdownFileAsync(fileId, trackChanges: false);

            return Ok();
        }


    }
}
