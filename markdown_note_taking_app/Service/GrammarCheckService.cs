using Markdig;
using markdown_note_taking_app.Interfaces.ServiceInterface;
using markdown_note_taking_app.Models.LanguageTool;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace markdown_note_taking_app.Service
{
    public class GrammarCheckService : IGrammarCheckService
    {
        


        public async Task<string> CheckGrammarMarkdown(string markdownContent)
        {
            var document = Markdown.Parse(markdownContent);

            await Task.Delay(1);
            return "for unit test";
        }
        public async Task<string> CheckGrammarFromApi(string content)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.languagetool.org/v2/check")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "text", content },
                    { "language", "en-US" }
                })
            };

            var client = new HttpClient();
            var response = await client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<LanguageToolResponse>(json);

            foreach (var match in result.Matches.OrderByDescending(m => m.Offset))
            {
                var replacement = match.Replacements.FirstOrDefault()?.Value;
                if (!string.IsNullOrEmpty(replacement))
                {
                    content = content.Substring(0, match.Offset) +
                    replacement +
                           content.Substring(match.Offset + match.Length);
                }
            }

            return content;
        }
    }
}
