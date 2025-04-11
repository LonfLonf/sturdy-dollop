using BlazorApp1.Model;
using English.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Services
{
    public class WordsService
    {
        public AppDbContext _dbContext;
        public List<Words> Words { get; set; }

        public WordsService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task CacheWords()
        {
            if (Words == null || !Words.Any())
            {
                Words = await _dbContext.Words.ToListAsync();
            }
        }

        public int getRandomWord(List<Words> words)
        {
            Random random = new Random();
            int index = random.Next(words.Count);

            return words[index].Id;
        }

        public async Task<Words[]?> getMultipleWords(int count)
        {
            if (count > 5)
            {
                return null;
            }

            await CacheWords();

            Words[] multipleWords = new Words[count];

            for (int i = 0; i < count; i++)
            {
                multipleWords[i] = await _dbContext.Words.FindAsync(getRandomWord(Words));
            }

            return multipleWords;
        }

        public async Task<bool[]?> getResponseEachWord(Words[] wordsQuestions, string[] wordsAnswer)
        {
            if (wordsQuestions.Length != wordsAnswer.Length)
            {
                return null;
            }

            bool[] response = new bool[wordsQuestions.Length];

            for (int i = 0; i < wordsQuestions.Length; i++)
            {
                if (wordsQuestions[i].PortugueseTranslation.Equals(wordsAnswer[i].ToLower()))
                {
                    response[i] = true;
                }
                else
                {
                    response[i] = false;
                }
            }

            return response;
        }
    }
}
