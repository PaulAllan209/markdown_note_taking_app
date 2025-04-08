using Entities.Models;

namespace markdown_note_taking_app.Interfaces
{
    public interface IMarkdownRepository
    {
        Task<IEnumerable<MarkdownFile>> GetAllMarkdownFilesAsync();
        Task<IEnumerable<MarkdownFile>> GetByIdsAsync();
        Task<MarkdownFile> GetMarkdownFileAsync();
        void CreateMarkdownFile(MarkdownFile markdownFile);
        void DeleteMarkdownFile(MarkdownFile markdownFile);
    }
}
