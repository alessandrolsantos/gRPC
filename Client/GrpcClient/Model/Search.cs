using Grpc.Net.Client;
using GrpcSeviceMovies;
using System.Threading.Tasks;

namespace GrpcClient.Model
{
    public class Search
    {


        public async Task<string> SearchMovie(string parameter)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new MovieSvc.MovieSvcClient(channel);

            var reply = await client.InPutAsync(new ParametersInPut
            { Name = parameter });


            return reply.ToString();
        }




    }
}
