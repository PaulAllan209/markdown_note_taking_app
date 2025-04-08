using Entities.Models;
using markdown_note_taking_app.Data;
using markdown_note_taking_app.Interfaces;

namespace markdown_note_taking_app.Repositories
{
    public class MarkdownRepository : IMarkdownRepository
    {
        private readonly DataContext _dataContext;

        public MarkdownRepository(DataContext dataContext)
        {
            _dataContext = dataContext;          
        }

        public void CreateMarkdownFile(MarkdownFile markdownFile)
        {
            _dataContext.Add(markdownFile);
        }

        public void DeleteMarkdownFile(MarkdownFile markdownFile)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MarkdownFile>> GetAllMarkdownFilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<MarkdownFile>> GetByIdsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MarkdownFile> GetMarkdownFileAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
