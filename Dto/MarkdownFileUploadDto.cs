namespace markdown_note_taking_app.Dto
{
    public record MarkdownFileUploadDto
    {
        public IFormFile MarkdownFile { get; init; }
    }
}
