using MeMawKNowsBestCrime.MeMawKnowsBestCrimeData;
using MeMawKnowsBestCrimeAPI.MeMawKnowsBestCrimeData;
using Microsoft.AspNetCore.Mvc;

namespace MeMawKnowsBestCrimeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeMawKnowBestCrimeAPIController : Controller
    {
        private readonly ILogger<MeMawKnowBestCrimeAPIController> logger;
        private readonly MeMawKnowsBestCrimeDataContext dbContext;


        public MeMawKnowBestCrimeAPIController(MeMawKnowsBestCrimeDataContext dbContext, ILogger<MeMawKnowBestCrimeAPIController> logger)
        {

            this.dbContext = dbContext;
            this.logger = logger;

        }


        public class MeMawKnowBestCrimeAPIParameter
        {
            public string? StateNames { get; set; }
            public double? CitySizeNotLessThan { get; set; }

        }


        [HttpGet(Name = "StatesLessCrimeCities")]

        public ActionResult<IEnumerable<MeMawKnowBestThreeWeightedCrimeDatum>> StatesLessCrimeCities(string StateNames, double CitySizeNotLessThan)
        {

            var LessCrimeCity = dbContext.MeMawKnowBestThreeWeightedCrimeData
            .Where(c => c.States == StateNames)
            .Where(c => c.WRelTotalMajorCrimeWeighted != 0)
            .Where(c => c.CitySize>= CitySizeNotLessThan)
            .OrderBy(c => c.WRelTotalMajorCrimeWeighted)
            .Take(10)
            .Select(group => new LessCrimCitySelected
            {

                AgencyName = group.AgencyName
              
            })
            .ToArray();
            return Ok(LessCrimeCity);



        }



    }
}


/*
 * To dockerize
 docker build -t mewdocker:v1 -f MeMawKnowsBestCrimeAPI/Dockerfile .
where mewdocker is the name and v1 is the version deployed
*/

/*
 To Run 
docker run -it --rm -p 8080:80 --name MeMaknowv2 mewdocker:v2
*/

/*
 To check all the images
docker images*/


//docker push oladimejishodipe/tomiwa:v5
//https://localhost:8086/api/MeMawKnowBestCrimeAPI?StateNames=Arizona&CitySizeNotLessThan=20000