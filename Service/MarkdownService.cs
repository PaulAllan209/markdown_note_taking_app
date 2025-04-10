using AutoMapper;
using Contracts;
using Entities.Models;
using markdown_note_taking_app.Dto;
using markdown_note_taking_app.Interfaces;
using markdown_note_taking_app.Interfaces.ServiceInterface;

namespace markdown_note_taking_app.Service
{
    public class MarkdownService : IMarkdownService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public MarkdownService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<MarkdownFileDto> CreateMarkdownFileAsync(MarkdownFileDto markdownFile)
        {
            var markdownFileEntity = _mapper.Map<MarkdownFile>(markdownFile);
            await _repository.SaveAsync();
            return markdownFile;
        }

        public Task<MarkdownFileDto> DeleteMarkdownFileAsync(Guid fileId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MarkdownFileDto>> GetAllMarkdownFilesAsync(bool trackChanges)
        {
            var markdownFileEntities = await _repository.MarkDown.GetAllMarkdownFilesAsync(trackChanges);
            var markdownFileDtos = _mapper.Map<IEnumerable<MarkdownFileDto>>(markdownFileEntities);
            return markdownFileDtos;
        }


        public async Task<MarkdownFileDto> GetMarkdownFileAsync(Guid fileId, bool trackChanges)
        {
            var markdownFileEntity = GetMarkdownFileAndCheckIfItExistsAsync(fileId, trackChanges);
            return _mapper.Map<MarkdownFileDto>(markdownFileEntity);
        }

        public Task<IEnumerable<MarkdownFileDto>> GetMarkdownFileByIdsAsync(IEnumerable<Guid> fileIds, bool trackChanges)
        {
            throw new NotImplementedException();
        }
        public async Task<MarkdownFile> GetMarkdownFileAndCheckIfItExistsAsync(Guid fileId, bool trackChanges)
        {
            var markDownFile = await _repository.MarkDown.GetMarkdownFileAsync(fileId, trackChanges);

            if (markDownFile == null)
                throw new FileNotFoundException();

            return markDownFile;
        }
    }
}
