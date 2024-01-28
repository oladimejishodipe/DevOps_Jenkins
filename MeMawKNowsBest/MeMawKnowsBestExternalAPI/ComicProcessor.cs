using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeMawKnowsBestExternalAPI
{
    public class ComicProcessor
    {
        public int MaxComicNumber { get; set; }
        public async Task <ComicModel> LoadComic(int comicNumber = 0)
        {
            string url = "";
            if (comicNumber > 0)
            {
                url = $"https://xkcd.com/{comicNumber}/info.0.json";
            }
            else
            {
                url= "https://xkcd.com/info.0.json";
            }
            using (HttpResponseMessage reponse = await ApiHelper.ApiClient.GetAsync(url))
            {
            if (reponse.IsSuccessStatusCode)
                {
                    ComicModel comic = await reponse.Content.ReadAsAsync<ComicModel>();

                    if(comicNumber == 0)
                    {
                        MaxComicNumber = comic.Num;
                    }

                    return comic;
                
                }
                else
                {
                    throw new(reponse.ReasonPhrase);
                }

            }
        }
    }
}
