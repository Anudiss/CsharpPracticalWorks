using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleDrawing.Types
{
    public static class Text
    {
        public static string PutIntoBox(this string text, Box box)
        {
            string pattern = @"(?<fullword>\s*(?<word>[^\s]+))";
            string[] paragraph = text.Split('\n');
            for (int i = 0; i < paragraph.Length; i++)
            {
                MatchCollection matchCollection = Regex.Matches(paragraph[i], pattern);
                paragraph[i] = string.Empty;
                string newParagraph = string.Empty;
                foreach (Match match in matchCollection.Cast<Match>())
                {
                    Group word = match.Groups["fullword"];
                    if (newParagraph.Length + word.Value.Length <= box.Width)
                        newParagraph += word.Value;
                    else
                    {
                        paragraph[i] += newParagraph + $"\n{match.Groups["word"].Value}";
                        newParagraph = string.Empty;
                    }
                }

                paragraph[i] += newParagraph;
            }
            return string.Join("\n", paragraph);
        }
    }
}
