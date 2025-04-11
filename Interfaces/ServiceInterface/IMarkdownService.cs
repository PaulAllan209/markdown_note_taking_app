using Entities.Models;
using markdown_note_taking_app.Dto;

namespace markdown_note_taking_app.Interfaces.ServiceInterface
{
    public interface IMarkdownService
    {
        Task<IEnumerable<MarkdownFileDto>> GetAllMarkdownFilesAsync(bool trackChanges);
        Task<MarkdownFileDto> GetMarkdownFileAsync(Guid fileId, bool trackChanges);
        Task<IEnumerable<MarkdownFileDto>> GetMarkdownFileByIdsAsync(IEnumerable<Guid> fileIds, bool trackChanges);
        Task<MarkdownFileDto> CreateMarkdownFileAsync(MarkdownFileUploadDto markdownFile);
        Task DeleteMarkdownFileAsync(Guid fileId, bool trackChanges);
        Task<MarkdownFile> GetMarkdownFileAndCheckIfItExistsAsync(Guid fileId, bool trackChanges);
        Task<MarkdownFileConvertToHtmlDto> GetMarkdownFileAsHtmlAsync(Guid fileId, bool trackChanges);
    }
}
