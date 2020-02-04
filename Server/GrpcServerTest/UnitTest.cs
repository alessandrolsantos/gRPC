using NUnit.Framework;
using System.Threading.Tasks;
using GrpcServer.Model;
using System.Collections.Generic;
using GrpcSeviceMovies;

namespace GrpcServerTest
{
    public class Tests
    {
        MovieSvcGRPC movieSvcGRPC;

        [SetUp]
        public void Setup()
        {
            movieSvcGRPC = new MovieSvcGRPC();
        }

        [Test]
        public void TestCallUrl()
        {

            RootObject result;

            Task.Run(async () =>
            {
                result = await movieSvcGRPC.CallUrl("Title=Spiderman 5");
                Assert.AreEqual("tt3696826", result.data[0].imdbID);
            }).GetAwaiter().GetResult();

        }

        [Test]
        public void TestGroupMovies()
        {
            RootObject rootObject = new RootObject()
            {
                per_page = 10,
                total = 1,
                total_pages = 1,
                data = new List<Movie>() { new Movie { Title = "Spiderman 5",
                                                       Year = 2008,
                                                       imdbID = "tt3696826"}
                }
            };

            YearSearchList yearSearchList = new YearSearchList()
            {
                moviesByYear = new List<YearSearch>() { new YearSearch(2008, 1) }, total = 1
            };
          
            Assert.AreEqual(yearSearchList.moviesByYear[0].year, movieSvcGRPC.GroupMovies(rootObject).moviesByYear[0].year);
            Assert.AreEqual(yearSearchList.moviesByYear[0].movies, movieSvcGRPC.GroupMovies(rootObject).moviesByYear[0].movies);
        }
    }
}