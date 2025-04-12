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

        [Fact]
        public async Task CheckGrammarFromApiTest()
        {
            var grammar_check_service = new GrammarCheckService();

            var input = "This is an exampel sentence";
            var input2 = "She dont knows how to writting a letter correctly.";
            var result1 = await grammar_check_service.CheckGrammarFromApi(input);
            var result2 = await grammar_check_service.CheckGrammarFromApi(input2);

            var expected_result1 = "This is an example sentence";
            var expected_result2 = "She don't knows how to writing a letter correctly.";


            if (result1 != expected_result1)
            {
                throw new Exception();
            }

            else if(result2 != expected_result2)
            {
                _output.WriteLine(result2);
                throw new Exception();
            }
        }
    }
}
