using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using OpenAI_API.Completions;
using OpenAI_API;

namespace MeMawExampleOpenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeMawKnowExampleAPIController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> GetAIBasedResult(string SearchText)
        {
            string APIKey = "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";
            string answer = string.Empty;

            var openai = new OpenAIAPI(APIKey);
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = SearchText;
            completion.Model = OpenAI_API.Models.Model.Davinci;
            completion.MaxTokens = 200;

            var result = openai.Completions.CreateCompletionAsync(completion);
            foreach (var item in result.Result.Completions)
            {
                answer = item.Text;
            }

            return Ok(answer);


        }

    }
}
