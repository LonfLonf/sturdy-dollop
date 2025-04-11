using BlazorApp1.Model;
using BlazorApp1.Model.Requests;
using BlazorApp1.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp1.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordsController : ControllerBase
    {
        public WordsService _service { get; set; }

        public WordsController(WordsService service)
        {
            _service = service;
        }

        [HttpGet("{count}")]
        public async Task<ActionResult<Words[]>> getMultipleWords([FromRoute] int count)
        {
            Words[] words = await _service.getMultipleWords(count);

            if (words == null)
            {
                return BadRequest();
            }

            if (words.Length == 0)
            {
                return NotFound();
            }

            return Ok(words);
        }

        [HttpPost]
        public async Task<ActionResult<bool[]>> getResponseEachWord([FromBody] WordsRequest request)
        {
            bool[] responses = await _service.getResponseEachWord(request.Words, request.Answers);

            if (responses == null)
            {
                return BadRequest();
            }

            return responses;
        }
    }
}
