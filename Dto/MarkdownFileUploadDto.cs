namespace markdown_note_taking_app.Dto
{
    public record MarkdownFileUploadDto
    {
        IFormFile MarkdownFile { get; init; }
    }
}
