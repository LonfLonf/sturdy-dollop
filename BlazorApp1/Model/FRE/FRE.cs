using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace English.Model.FRE
{
    public class FRE
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("text")]
        public string Text { get; set; }

        [Column("traduction")]
        public string Traduction { get; set; }

        [Column("wordscount")]
        public int WordsCount { get; set; }

        [Column("sentencecount")]
        public int SentenceCount { get; set; }

        [Column("syllablescount")]
        public int SyllablesCount { get; set; }

        [Column("fleschreadingease")]
        public int FleschReadingEase { get; set; }

        [Column("ranking")]
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
