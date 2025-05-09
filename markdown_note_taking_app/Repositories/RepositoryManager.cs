﻿using markdown_note_taking_app.Data;
using markdown_note_taking_app.Interfaces;

namespace markdown_note_taking_app.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly DataContext _dataContext;
        private readonly Lazy<IMarkdownRepository> _markdownRepository;

        public RepositoryManager(DataContext dataContext)
        {
            _dataContext = dataContext;
            _markdownRepository = new Lazy<IMarkdownRepository>(() => new MarkdownRepository(dataContext));
        }

        public IMarkdownRepository MarkDown => _markdownRepository.Value;

        public async Task SaveAsync() => await _dataContext.SaveChangesAsync();
    }
}
