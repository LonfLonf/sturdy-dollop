using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace English.Model.FRE
{
    public class FRE
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Traduction { get; set; }
        public int WordsCount { get; set; }
        public int SentenceCount { get; set; }
        public int SyllablesCount { get; set; }
        public int FleschReadingEase { get; set; }
        public int Ranking { get; set; }

        public string RemoveSymbols(string Text)
        {
            return Regex.Replace(Text, "[,.!?]", "");
        }

        public string RemoveAccents(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}
