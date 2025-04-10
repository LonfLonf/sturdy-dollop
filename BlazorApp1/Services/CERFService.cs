using CsvHelper;
using English.Data;
using English.Model.CERF;
using English.Records;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

public class CERFService
{
    private AppDbContext _dbContext;
    char[] specialChars = [' ', ',', '.', ':', '!', '?', '#', '$', '%', '¨', '*', ';', '\t',];

    public CERFService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<CERF> readCsv()
    {
        using (var reader = new StreamReader("C:\\Users\\firey\\Downloads\\archive\\ENGLISH_CERF_WORDS.csv"))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<CERF>();
            return records.ToList();
        }
    }

    public async Task<string[]> GenerateRandomWords(string CERF, int WordsCount)
    {
        Random random = new Random();
        string [] words = new string[WordsCount];
        List<CERF> listCERF = await GetAllWordsByCERF(CERF);

        for (int i = 0; i < WordsCount; i++)
        {
            words[i] = listCERF[random.Next(500)].Word;
        }

        return words;
    }

    public string GenerateText(string[] Words)
    {
        string text = string.Join(" ", Words);
        return text;
    }

    public async Task putEverythingInDatabase()
    {
        foreach (var record in readCsv())
        {
            await _dbContext.AddAsync(record);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<CERF>> GetAllWordsByCERF(string CERF)
    {
        return await _dbContext.CERFs.Where(p => p.CEFR.ToLower() == CERF.ToLower()).ToListAsync();
    }

    public async Task<CERF> GetWord(string word)
    {
        return await _dbContext.CERFs.FirstOrDefaultAsync(p => p.Word.ToLower() == word.ToLower());
    }

    public async Task<CERF> GetWordById(int Id)
    {
        return await _dbContext.CERFs.FindAsync(Id);
    }

    public string[] TokanizeText(string text)
    {
        return text.Split(specialChars);
    }

    public async Task<CerfApiResponse> GetPercentageOfText(string[] Words)
    {
        Dictionary<string, int> contageOfCERF = new Dictionary<string, int>()
        {
            { "A1", 0 },
            { "A2", 0 },
            { "B1", 0 },
            { "B2", 0 },
            { "C1", 0 },
            { "C2", 0 },
            { "UND", 0 },
        };

        int LengthOfArray = Words.Length;

        for (int i = 0; i < LengthOfArray; i++)
        {
            CERF cerf = await GetWord(Words[i]);
            if (cerf != null)
            {
                if (contageOfCERF.ContainsKey(cerf.CEFR))
                {
                    contageOfCERF[cerf.CEFR] += 1;
                }
                else
                {
                    contageOfCERF["UND"] += 1;
                }
            }
        }

        var TopPercentageAndKey = contageOfCERF.OrderByDescending(k => k.Value).First();
        CerfApiResponse response = new CerfApiResponse(TopPercentageAndKey.Value, LengthOfArray, TopPercentageAndKey.Key, contageOfCERF);
        return response;
    }
}

