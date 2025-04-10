namespace English.Model.FRE
{
    public class FREHelper
    {
        private const float const1 = 206.835f;
        private const float const2 = 1.015f;
        private const float const3 = 84.6f;

        public int WordsCount {  get; set; }
        public int SentenceCount { get; set; }
        public int SyllablesCount { get; set; }

        private string Text { get; set; }

        public FREHelper(string Text)
        {
            this.Text = Text;
            WordsCount = GetWordsCount(Text);
            SentenceCount = GetSentenceCount(Text);
            SyllablesCount = GetSyllableCount(Text);
        }

        public FREHelper()
        {

        }

        public int GetSyllableCount(string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return 0;

            word = word.ToLower().Trim();
            char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'y' };
            int syllableCount = 0;
            bool lastWasVowel = false;

            foreach (char c in word)
            {
                if (vowels.Contains(c))
                {
                    if (!lastWasVowel)
                    {
                        syllableCount++;
                    }
                    lastWasVowel = true;
                }
                else
                {
                    lastWasVowel = false;
                }
            }

            if (word.EndsWith("e") && syllableCount > 1)
                syllableCount--;

            return Math.Max(1, syllableCount);
        }

        public int GetSyllableCountFromText(string text)
        {
            var words = text.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            int totalSyllables = 0;

            foreach (var word in words)
            {
                totalSyllables += GetSyllableCount(word);
            }

            return totalSyllables;
        }


        public int GetSentenceCount(string Text)
        {
            char[] sentenceDelimiters = { '.', '!', '?' };

            return Text.Split(sentenceDelimiters, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public int GetWordsCount(string Text)
        {
            return Text.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public int FleschReadingEaseFormula()
        {
            float wordsPerSentence = (float)WordsCount / SentenceCount;
            float syllablesPerWord = (float)SyllablesCount / WordsCount;

            float result = const1 - const2 * wordsPerSentence - const3 * syllablesPerWord;
            return (int)Math.Round(result);
        }

        public int RankingDifficulty(int Result)
        {
            if (Result >= 90)
            {
                return 0; // Muito fácil
            }
            else if (Result >= 80 && Result <= 89)
            {
                return 1; // Fácil
            }
            else if (Result >= 70 && Result <= 79)
            {
                return 2; // Razoavelmente fácil
            }
            else if (Result >= 60 && Result <= 69)
            {
                return 3; // Normal
            }
            else if (Result >= 50 && Result <= 59)
            {
                return 4; // Razoavelmente difícil
            }
            else if (Result >= 30 && Result <= 49)
            {
                return 5; // Difícil
            }
            else
            {
                return 6; // Muito difícil
            }
        }

    }
}
