using BlazorApp1.DTO;
using English.Model;
using English.Model.FRE;
using English.Model.Requests;
using English.Records;
using English.Services;
using Microsoft.AspNetCore.Mvc;

namespace English.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class FREController : ControllerBase
    {
        public FREService FREService { get; set; }

        public FREController(FREService FREService)
        {
            this.FREService = FREService;
        }

        /*
        [HttpGet("GetFRE")]
        public async Task<ActionResult<FreApiResponse>> GetFREResponse([FromBody] TextRequest request)
        {
            FREHelper fre = new FREHelper(request.Text);
            FREHelper freHelper = new FREHelper(request.Text);
            int valueOfFre = freHelper.FleschReadingEaseFormula();

            if (fre == null || freHelper == null)
            {
                return NotFound();
            }

            return new FreApiResponse(freHelper.WordsCount, freHelper.SentenceCount, freHelper.SyllablesCount, freHelper.FleschReadingEaseFormula(), freHelper.RankingDifficulty(valueOfFre));
        }
        */

        [HttpGet("GetFreByRanking/{Id}")]
        public async Task<ActionResult<List<FRE>>> GetAllFreByRanking([FromRoute] int Ranking)
        {
            var response = await FREService.GetAllFreByRanking(Ranking);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("GetRandomFreByRanking/{Ranking}")]
        public async Task<ActionResult<RandomFreDTO>> GetRandomFreByRanking(int Ranking)
        {
            var response = await FREService.GetRandomFreByRanking(Ranking);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(new RandomFreDTO(response.Id, response.Text, response.Traduction, response.Ranking));
        }

        
        /*
        [HttpGet("GetFreById/{Id}")]
        public async Task<ActionResult<FRE>> GetFreById(int Id)
        {
            var response = await FREService.GetFreById(Id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        */

        // Use to populate Database
        /*
        [HttpPost("postfre")]
        public async Task<ActionResult> PostFRE([FromBody] List<TextRequestWithTraduction> requests)
        {
            if (requests == null || requests.Count == 0)
            {
                return BadRequest("A lista não pode estar vazia.");
            }

            foreach (var request in requests)
            {
                if (string.IsNullOrWhiteSpace(request.Text) || string.IsNullOrWhiteSpace(request.Traduction))
                {
                    return BadRequest("O texto e a tradução não podem ser vazios.");
                }

                await FREService.PostFRP(request);
            }

            return Ok("Todos os itens foram processados!");
        }
        */

        /*
        [HttpPost("postfre")]
        public async Task<ActionResult> PostFRE([FromBody] TextRequestWithTraduction request)
        {
            if (request.Text == null || string.IsNullOrWhiteSpace(request.Text) || (request.Traduction == null || string.IsNullOrWhiteSpace(request.Traduction)))
            {
                return BadRequest("O texto não pode ser vazio ou nulo.");
            }

            await FREService.PostFRP(request);

            return Ok();    
        }
        */

        [HttpPost("postanswer")]
        public async Task<ActionResult<AnswerFreApiResponse>> PostAnswer([FromBody] AnswerFre Answer)
        {
            FRE Fre = await FREService.GetFreById(Answer.IdFre);

            if (Fre == null)
            {
                return NotFound();
            }

            string freRequestTextTraduction = Fre.RemoveAccents(Fre.RemoveSymbols(Fre.Traduction)).ToLower().Trim();
            string AnswerWithoutSymbols = Fre.RemoveAccents(Fre.RemoveSymbols(Fre.Traduction)).ToLower().Trim();

            if (freRequestTextTraduction.Equals(AnswerWithoutSymbols))
            {
                if (Answer.Text.Equals(Fre.Traduction))
                {
                    return new AnswerFreApiResponse(StatusCodes.Status200OK, "Perfect!", Enums.Correctness.Perfectly);
                }
                else
                {
                    return new AnswerFreApiResponse(StatusCodes.Status200OK, "Somethins is Missing!", Enums.Correctness.SomethingIsMissing);
                }
            }

            return new AnswerFreApiResponse(StatusCodes.Status200OK, "Wrong!", Enums.Correctness.Wrong);
        }
    }  
}
