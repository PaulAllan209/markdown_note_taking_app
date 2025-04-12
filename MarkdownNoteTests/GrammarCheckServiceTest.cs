using markdown_note_taking_app.Service;
using Xunit;
using Xunit.Abstractions;

namespace markdown_note_taking_app.Tests
{
    public class GrammarCheckServiceTest
    {
        private readonly ITestOutputHelper _output;

        public GrammarCheckServiceTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task CheckGrammarMarkdownTest()
        {
            var grammar_check_service = new GrammarCheckService();

            var input = "i dont know what im doing. this is fine.";
            var result = await grammar_check_service.CheckGrammarMarkdown(input);

            _output.WriteLine(result);
        }
    }
}
