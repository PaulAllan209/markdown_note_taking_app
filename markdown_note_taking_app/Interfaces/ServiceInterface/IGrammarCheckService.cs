namespace markdown_note_taking_app.Interfaces.ServiceInterface
{
    public interface IGrammarCheckService
    {
        Task<string> CheckGrammarMarkdown(string markdownContent);
        Task<string> CheckGrammarFromApi(string content);
    }
}
