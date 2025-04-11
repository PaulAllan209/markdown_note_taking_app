using AutoMapper;
using Contracts;
using Entities.Models;
using markdown_note_taking_app.Dto;
using markdown_note_taking_app.Interfaces;
using markdown_note_taking_app.Interfaces.ServiceInterface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

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

        public async Task<MarkdownFileDto> CreateMarkdownFileAsync(MarkdownFileUploadDto markdownFile)
        {
            if (markdownFile == null)
                throw new BadHttpRequestException("File is empty");

            string fileName = Path.GetFileName(markdownFile.MarkdownFile.FileName);

            //Read the file content into a string
            string fileContent;
            using(var reader = new StreamReader(markdownFile.MarkdownFile.OpenReadStream()))
            {
                fileContent = await reader.ReadToEndAsync();
            }

            //Create new MarkdownFileDto for creation in database and return value
            var MarkdownFileDto = new MarkdownFileDto
            {
                Id = Guid.NewGuid(),
                Title = fileName,
                FileContent = fileContent,
                UploadDate = DateTime.Now
            };

            // Last Step
            var markdownFileEntity = _mapper.Map<MarkdownFile>(MarkdownFileDto);
            _repository.MarkDown.CreateMarkdownFile(markdownFileEntity);
            await _repository.SaveAsync();

            return MarkdownFileDto;
        }

        public async Task DeleteMarkdownFileAsync(Guid fileId, bool trackChanges)
        {
            if (fileId == Guid.Empty)
                throw new ArgumentNullException("The file id cannot be empty.");

            var markdownFileEntity = await GetMarkdownFileAndCheckIfItExistsAsync(fileId, trackChanges);

            _repository.MarkDown.DeleteMarkdownFile(markdownFileEntity);
            await _repository.SaveAsync();
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
