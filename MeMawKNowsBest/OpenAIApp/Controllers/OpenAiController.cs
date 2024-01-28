using Azure;
using MeMawKNowsBestCrime.MeMawKnowsBestCrimeData;
using MeMawKnowsBestCrimeAPI.MeMawKnowsBestCrimeData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenAI_API;
using OpenAI_API.Moderation;
using OpenAIApp.Configurations;
using OpenAIApp.Services;

namespace OpenAIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpenAiController : ControllerBase
    {

        private readonly ILogger<OpenAiController> _logger;
        private readonly IOpenAiService _openAiService;
        private readonly MeMawKnowsBestCrimeDataContext _dbContext;
        private readonly OpenAiConfig _openAiConfig;


        public OpenAiController(ILogger<OpenAiController> logger, IOpenAiService openAiService, IOptionsMonitor<OpenAiConfig> optionsMonitor, MeMawKnowsBestCrimeDataContext dbContext)
        {
            _logger = logger;
            _openAiService = openAiService;
            _dbContext = dbContext;
            _openAiConfig = optionsMonitor.CurrentValue;
        }

        /*
        [HttpPost]
        [Route("CompleteSentence")]
        public async Task<IActionResult> CompleteSentence(string text)
        {
            var result = await _openAiService.CompleteSentence(text);
            return Ok(result);
        }

        [HttpPost]
        [Route("CompleteSentenceAdvance")]
        public async Task<IActionResult> CompleteSentenceAdvance(string text)
        {
            var result = await _openAiService.CompleteSentence(text);
            return Ok(result);
        }

        [HttpPost]
        [Route("AskQuestion")]
        public async Task<IActionResult> AskQuestion(string text)
        {
            var result = await _openAiService.CheckProgramingLanguage(text);
            return Ok(result);
        }
*/

        [HttpPost]
        [Route("RankCityBaseOffCrimeBusiness")]
        public async Task<IActionResult> RankCityBaseOffCrimeBusiness(string stateNames)
        {
            try
            {
                // Call StatesLessCrimeCities to get the list of cities
                var lessCrimeCitiesResult = StatesLessCrimeCities(stateNames);

                if (lessCrimeCitiesResult.Result is OkObjectResult okResult && okResult.Value is IEnumerable<LessCrimCitySelected> lessCrimeCities)
                {
                    // Extract the list of cities from the result
                    var citiesList = lessCrimeCities.Select(city => city.AgencyName).ToList();
                    //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
                    // Use the list of cities in the chat conversation
                    var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key); // Assuming _openAiConfig is defined somewhere
                    var chat = api.Chat.CreateConversation();
                    chat.AppendSystemMessage("Imagine you are a researcher analyzing economic and social impacts for a study these different cities in CitiList. The findings would used by people who wants to relocate to any of these areas but need to find out if there are economic opportunities and good air quality before moving to these areas in the US. Your goal is to compare these cities based on various factors such as the level of air quality per population such a good quality air is where there is less carbon emissions. Also compared by the cities by economic activities such as level of economic activities, e.g., major industries, business sectors.  Rank these cities, whereby cities with higher air quality and higher economic activity rank highest, and cities with cities with lower air quality and lower economic activity rank lowest. Your ranking does not have to use recent data. Also list out some of the neighbourhood in these cities. Dont provide information, just provide the ranking");
                    chat.AppendUserInput(string.Join(", ", citiesList));
                    var response = await chat.GetResponseFromChatbotAsync();

                    return Ok(response);
          
                }


                return BadRequest("Error: Unable to retrieve the list of cities");
            }
            catch (Exception ex)
            {
                // Exceptions handling 
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
        

        private ActionResult<IEnumerable<MeMawKnowBestThreeWeightedCrimeDatum>> StatesLessCrimeCities(string stateNames)
        {
            var lessCrimeCity = _dbContext.MeMawKnowBestThreeWeightedCrimeData
                .Where(c => c.States == stateNames)
                .Where(c => c.WRelTotalMajorCrimeWeighted != 0)
                .OrderBy(c => c.WRelTotalMajorCrimeWeighted)
                .Take(10)
                .Select(group => new LessCrimCitySelected
                {
                    AgencyName = group.AgencyName
                })
                .ToArray();

            return Ok(lessCrimeCity);
        }



    }
}




/* Want to convert the result to JSON but not working yet
                  var jsonObject = JObject.Parse(response);
                  var gptResponseModel = jsonObject.ToObject<GptResponseModel>();

                  // Convert the model to JSON format
                  var jsonResponse = JsonConvert.SerializeObject(gptResponseModel);
                  return Ok(jsonResponse);
    */
