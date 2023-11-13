

using NUnit.Framework;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using GameOfLifeAPI.Model;
using NSubstitute;
using KataGameOfLife;
using NSubstitute.ExceptionExtensions;
using GameOfLifeKata.API.Controllers.v2;

namespace GameOfLifeAPITest.API
{
    [TestClass]
    public class ControllerV2Should
    {
        [TearDown]
        public void Delete() {
            string path = @"C:\Users\fahernandez\source\repos\GameOfLifeAPI\GameOfLifePersistance\GamesJSON\"; 
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            if(files.Length > 0){
                var recentFile = files.OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                string pathfile = Path.Combine(path, recentFile.Name);
                File.Delete(pathfile);
            }
        }
        [Test]
        public void given_good_request_when_post_should_return_created() {
            var mockService = Substitute.For<BoardRepository>();
            GameOfLife game = new GameOfLife(mockService);
            GameOfLifeController controller = new GameOfLifeController(game);
            int[][] goodRequest = new int[1][];
            goodRequest[0] = new int[2];
            goodRequest[0][0] = 1;
            goodRequest[0][1] = 1;

            ActionResult action = controller.Post(goodRequest);

            mockService.Received().Save(Arg.Any<Board>());
        }
        [Test]
        public void given_good_request_when_put_should_return_ok()
        {
            var mockService = Substitute.For<BoardRepository>();
            GameOfLife game = new GameOfLife(mockService);
            GameOfLifeController controller = new GameOfLifeController(game);
            bool[][] goodRequest = new bool[1][];
            goodRequest[0] = new bool[1];
            goodRequest[0][0] = true;
            Guid Id= Guid.NewGuid();
            string path = @"C:\Users\fahernandez\source\repos\GameOfLifeAPI\GameOfLifePersistance\GamesJSON\";
            string file = Path.Combine(path, $"{Id}.json");
            File.WriteAllText(file, JsonSerializer.Serialize(goodRequest));

            ActionResult action = controller.Put(Id);

            mockService.Received().Get(Arg.Any<Guid>());
        }
        [Test]
        public void given_id_with_no_file_when_put_return_not_found() {
            var mockService = Substitute.For<BoardRepository>();
            mockService.Get(Arg.Any<Guid>()).Throws(new Exception());
            GameOfLife game = new GameOfLife(mockService);
            Guid Id = Guid.NewGuid();
            GameOfLifeController controller = new GameOfLifeController(game);

            ActionResult action = controller.Put(Id);

            action.Should().BeEquivalentTo(controller.StatusCode(404));
        }
    }
}
