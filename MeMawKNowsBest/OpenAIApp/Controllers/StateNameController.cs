using MeMawKNowsBestCrime.MeMawKnowsBestCrimeData;
using MeMawKnowsBestCrimeAPI.MeMawKnowsBestCrimeData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OpenAI_API;
using OpenAIApp.Configurations;
using OpenAIApp.Services;

namespace OpenAIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateNameController : ControllerBase
    {

        private readonly ILogger<StateNameController> _logger;
        private readonly MeMawKnowsBestCrimeDataContext _dbContext;


  

        public StateNameController(ILogger<StateNameController> logger,  MeMawKnowsBestCrimeDataContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;

        }
        [HttpGet]
        [Route("GetStateName")]
        public ActionResult<IEnumerable<MeMawKnowBest_StateName>> GetStateName()
        {
            var StatesName = _dbContext.MeMawKnowBest_StateNames;
            return Ok(StatesName);
        }





    }
}
