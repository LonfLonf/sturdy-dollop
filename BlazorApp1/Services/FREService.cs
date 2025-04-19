using English.Data;
using English.Model.FRE;
using English.Model.Requests;
using Microsoft.EntityFrameworkCore;

namespace English.Services
{
    public class FREService
    {
        private AppDbContext _dbContext { get; set; }

        public FREService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task PostFRP(TextRequestWithTraduction textRequest)
        {
            FRE fre = new FRE();
            FREHelper FREHelper = new FREHelper(textRequest.Text);

            fre.Text = textRequest.Text;
            fre.Traduction = textRequest.Traduction;
            fre.WordsCount = FREHelper.WordsCount;
            fre.SentenceCount = FREHelper.SentenceCount;
            fre.SyllablesCount = FREHelper.SyllablesCount;
            fre.FleschReadingEase = FREHelper.FleschReadingEaseFormula();
            fre.Ranking = FREHelper.RankingDifficulty(fre.FleschReadingEase);

            await _dbContext.FREs.AddAsync(fre);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<FRE>> GetAllFreByRanking(int Ranking)
        {
            return await _dbContext.FREs.Where(p => p.Ranking == Ranking).ToListAsync();
        }

        public async Task<FRE> GetRandomFreByRanking(int Ranking)
        {
            var query = @"
                SELECT * 
                FROM fres 
                WHERE Ranking = {0} 
                OFFSET floor(random() * (SELECT COUNT(*) FROM FREs WHERE Ranking = {0})) 
                LIMIT 1
                ";

            var randomFre = await _dbContext.FREs
                .FromSqlRaw(query, 5)
                .FirstOrDefaultAsync();

            return randomFre;
        }
        
        /*
        public async Task<FRE> GetRandomFreByRanking(int Ranking)
        {
            List<FRE> listFre = await GetAllFreByRanking(Ranking);
            Random random = new Random();
            int index = random.Next(listFre.Count());

            return listFre[index];
        }
        */

        public async Task<FRE> GetFreById(int Id)
        {
            return await _dbContext.FREs.FindAsync(Id);
        }

    }
}
