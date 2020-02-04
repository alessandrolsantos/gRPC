using System;
using System.IO;
using System.Threading.Tasks;
using GrpcClient.Model;

namespace GrpcClientMovies
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Search search = new Search();
            while (true)
            {
                try
                {
                    Console.WriteLine("\nType a Movie or hit Enter to show all");
                    
                    string title = Console.ReadLine();

                    if (title.Equals(""))
                        Console.Write("The return may take a while, please wait\n");

                    var reply = await search.SearchMovie(title);

                    Console.WriteLine(reply);
                }
                catch (Grpc.Core.RpcException e)
                {
                    Console.WriteLine(e.Message);                    
                }
            }
        }
    }
}