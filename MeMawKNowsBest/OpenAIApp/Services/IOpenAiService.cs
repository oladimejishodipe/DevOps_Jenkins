namespace OpenAIApp.Services
{
    public interface IOpenAiService

    {
        Task<string> CompleteSentence(string text);
        Task<string> CompleteSentenceAdvance(string text);
        Task<string> CheckProgramingLanguage(string language);
       // Task RankCityBaseOffCrimeBusiness(string text);
      
    }
}
