using English.Model;
using English.Model.CERF;
using English.Model.Requests;
using English.Records;
using Microsoft.AspNetCore.Mvc;

namespace English.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CERFController : ControllerBase
    {
        public CERFService _CERFService;

        public CERFController(CERFService CERFService)
        {
            _CERFService = CERFService;
        }

        [HttpGet("GetAllWordsByCERF/{CERF}")]
        public async Task<ActionResult<List<CERF>>> GetAllWordsByCERF(string CERF)
        {
            var result = await _CERFService.GetAllWordsByCERF(CERF);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetWord/{Word}")]
        public async Task<ActionResult<CERF>> GetWord(string Word)
        {
            var result = await _CERFService.GetWord(Word);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetWordById/{Id}")]
        public async Task<ActionResult<CERF>> GetWordById([FromRoute] int Id)
        {
            var result = await _CERFService.GetWordById(Id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("GetRandomText/{CERF}/{WordsCount}")]
        public async Task<string> GetRandomText(string CERF, int WordsCount)
        {
            string[] Words = await _CERFService.GenerateRandomWords(CERF, WordsCount);
            return _CERFService.GenerateText(Words);
        }

        [HttpGet("GetResponseCERFOfText")]
        public async Task<ActionResult<CerfApiResponse>> GetResponseCERFOfText([FromBody] TextRequest request)
        {
            string[] Words = _CERFService.TokanizeText(request.Text);
            CerfApiResponse response = await _CERFService.GetPercentageOfText(Words);

            if (response == null)
            {
                return NotFound();
            }

            return response;
        }

        [HttpPost]
        public async Task<ActionResult<AnswerCerfApiResponse>> PostAnswerCERF([FromRoute] TextRequest answerCerf, [FromRoute] string[] Words)
        {
            if (answerCerf == null)
            {
                return BadRequest();
            }

            string wordsInText = _CERFService.GenerateText(Words);

            if (answerCerf.Text.ToLower().Equals(wordsInText.ToLower()))
            {
                return new AnswerCerfApiResponse(true);
            }
            else
            {
                return new AnswerCerfApiResponse(false);
            }
        }
    }
}
