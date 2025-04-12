namespace markdown_note_taking_app.Interfaces
{
    public interface IRepositoryManager
    {
        IMarkdownRepository MarkDown { get; }
        Task SaveAsync();
    }
}
