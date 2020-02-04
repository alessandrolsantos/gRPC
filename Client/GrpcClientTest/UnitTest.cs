using NUnit.Framework;
using System.Threading.Tasks;
using GrpcClient.Model;

namespace GrpcClientTest
{
    public class Tests
    {
        Search search = new Search();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string reply = "{ \"message\": \"{\\\"moviesByYear\\\":[{\\\"year\\\":2008,\\\"movies\\\":1}],\\\"total\\\":1}\" }";
            Task.Run(async () =>
            {
                try
                {
                    reply = await search.SearchMovie("Spiderman 5");
                }
                catch (Grpc.Core.RpcException e)
                {
                    StringAssert.Contains(reply, e.Message);
                }
            }).GetAwaiter().GetResult();            
                
        }
    }
}