using Markdig;
using markdown_note_taking_app.Interfaces.ServiceInterface;

namespace markdown_note_taking_app.Service
{
    public class GrammarCheckService : IGrammarCheckService
    {
        

        public Task<string> CheckGrammarFromApi(string content)
        {
            throw new NotImplementedException();
        }

        public async Task<string> CheckGrammarMarkdown(string markdownContent)
        {
            var document = Markdown.Parse(markdownContent);

            await Task.Delay(1);
            return "for unit test";
        }
    }
}
