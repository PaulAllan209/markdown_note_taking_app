using Entities.Models;
using Markdig;
using Markdig.Syntax.Inlines;
using Markdig.Syntax;
using markdown_note_taking_app.Interfaces.ServiceInterface;
using markdown_note_taking_app.Models.LanguageTool;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace markdown_note_taking_app.Service
{
    public class GrammarCheckService : IGrammarCheckService
    {
        


        public async Task<string> CheckGrammarMarkdownAsync(string markdownContent)
        {
            var document = Markdown.Parse(markdownContent);

            var replacements = new Dictionary<int, string>();

            ProcessDocumentAsync(document, replacements);

            if (replacements.Any())
            {
                // Sort by descending to avoid offset issues when replacing
                foreach (var replacement in replacements.OrderByDescending(r => r.Key))
                {
                    // Replace original text with corrected text at the specific location
                    // This is simplified - real implementation would need to handle offsets properly
                    markdownContent = markdownContent.Substring(0, replacement.Key) +
                                     replacement.Value +
                                     markdownContent.Substring(replacement.Key + replacement.Value.Length);
                }
            }

            return markdownContent;
        }
        public async Task<string> CheckGrammarFromApiAsync(string content)
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

        private async Task ProcessDocumentAsync(MarkdownObject markdownObject, Dictionary<int, string> replacements)
        {
            // If this is a leaf block with text
            if (markdownObject is LeafBlock leafBlock && leafBlock.Inline != null)
            {
                foreach (var inline in leafBlock.Inline)
                {
                    if (inline is LiteralInline literalInline)
                    {
                        // Extract text content for grammar checking
                        string originalText = literalInline.Content.ToString();

                        // Check grammar on the text content
                        string correctedText = await CheckGrammarFromApiAsync(originalText);

                        // If there's a correction, store it for replacement
                        if (correctedText != originalText)
                        {
                            replacements[literalInline.Span.Start] = correctedText;
                        }
                    }
                }
            }

            // Process all children recursively
            foreach (var child in markdownObject.Descendants())
            {
                await ProcessDocumentAsync(child, replacements);
            }
        }
    }
}
