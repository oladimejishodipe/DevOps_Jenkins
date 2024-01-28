using Microsoft.Extensions.Options;
using OpenAIApp.Configurations;
using OpenAI_API.Completions;
using OpenAI_API;
using OpenAI_API.Models;
using MeMawKnowsBestCrimeAPI.MeMawKnowsBestCrimeData;
using Microsoft.AspNetCore.Mvc;
using MeMawKNowsBestCrime.MeMawKnowsBestCrimeData;
using OpenAIApp.Controllers;

namespace OpenAIApp.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly OpenAiConfig _openAiConfig;
        //private readonly MeMawKnowsBestCrimeDataContext _dbContext;
        // MeMawKnowsBestCrimeDataContext dbContext
        public OpenAiService(IOptionsMonitor<OpenAiConfig> optionsMonitor)
        {
            _openAiConfig = optionsMonitor.CurrentValue;
           // _dbContext = dbContext;
        }


        public async Task<string> CheckProgramingLanguage(string language)
        {
            //api instance
            var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
            var chat = api.Chat.CreateConversation();
            chat.AppendSystemMessage("Your are a teacher who help new programmers understand things are programming language or not.If the user tells you a programming language  respond with yes, if a user tells you something which is not a programming language repond with a NO. You will only repond with Yes or No and nothing else ");
            chat.AppendUserInput(language);
            var response = await chat.GetResponseFromChatbotAsync();
            return response;
        }

        public async Task<string> CompleteSentence(string text)
        {
           //api instance
            var api=new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
            var result = await api.Completions.GetCompletion(text);
            return result;
        }

        public async Task<string> CompleteSentenceAdvance(string text)
        {
            //api instance
            var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
            var result = await api.Completions.CreateCompletionAsync(
                new CompletionRequest(text, model: Model.CurieText, temperature: 0.1));
            return result.Completions[0].Text;
        }
       /*
        public async Task<string> RankCityBaseOffCrimeBusiness(string stateNames)
        {

            // Call StatesLessCrimeCities to get the list of cities
            var lessCrimeCitiesResult = StatesLessCrimeCities(stateNames);

            if (lessCrimeCitiesResult.Result is OkObjectResult okResult && okResult.Value is IEnumerable<LessCrimCitySelected> lessCrimeCities)
            {
                // Extract the list of cities from the result
                var citiesList = lessCrimeCities.Select(city => city.AgencyName).ToList();

                // Use the list of cities in the chat conversation
                var api = new OpenAI_API.OpenAIAPI(_openAiConfig.Key);
                var chat = api.Chat.CreateConversation();
                chat.AppendSystemMessage("You are a geography teacher. You need to list out which city has the highest crime in any country selected. Just list ten cities with lesser crime and nothing else");
                chat.AppendUserInput(string.Join(", ", citiesList));
                var response = await chat.GetResponseFromChatbotAsync();
                return response;
            }

            // Handle the case where the result from StatesLessCrimeCities is not as expected
            return "Error: Unable to retrieve the list of cities";
        }

     */

      
    }


}


    


