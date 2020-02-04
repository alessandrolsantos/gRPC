using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServer.Model;
using GrpcSeviceMovies;
using Microsoft.Extensions.Logging;

namespace GrpcSeviceMovies
{
    public class MovieSvcGRPC : MovieSvc.MovieSvcBase
    {
        private static readonly HttpClient client = new HttpClient();
        public MovieSvcGRPC()
        {
          
        }

        public override Task<ResultSearch> InPut(ParametersInPut request, ServerCallContext context)
        {
            string result = string.Empty;

            Task.Run(async () =>
            {
                result = await SearchForTitle(request.Name);
            }).GetAwaiter().GetResult();

            return Task.FromResult(new ResultSearch
            {
                Message = result
            }); ;
        }

        public async Task<string> SearchForTitle(string titlie)
        {
            RootObject rootObject;

            rootObject = CallUrl(string.Format("Title={0}", titlie)).Result;

            for (int i = 1; i < rootObject.total_pages; i++)
            {
                rootObject.data.AddRange(CallUrl(string.Format("Title={0}&page={1}", titlie, (i + 1))).Result.data);
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(GroupMovies(rootObject));
        }

        public YearSearchList GroupMovies(RootObject rootObject)
        {
            YearSearchList resultList = new YearSearchList();
            var groupedResult = rootObject.data.ToList().GroupBy(x => x.Year).Select(y => new { y.Key, count = y.Count() }).OrderBy(x => x.Key).ToList();

            foreach (var item in groupedResult)
            {
                resultList.moviesByYear.Add(new YearSearch(item.Key, item.count));
            }

            resultList.getTotalSum();

            return resultList;
        }

        public async Task<RootObject> CallUrl(string parameter)
        {
            var responseString = await client.GetStringAsync(string.Format("https://jsonmock.hackerrank.com/api/movies/search/?{0}", parameter));
            var result = System.Text.Json.JsonSerializer.Deserialize<RootObject>(responseString);

            return result;
        }
    }
}
