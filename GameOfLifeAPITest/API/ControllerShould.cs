

using NUnit.Framework;
using System.Text.Json;
using FluentAssertions;
using GameOfLifeAPI.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
namespace GameOfLifeAPITest.API
{
    [TestClass]
    public class ControllerShould
    {
        [TearDown]
        public void Delete() {
            string path = @"C:\Users\fahernandez\source\repos\GameOfLifeAPI\GameOfLifeAPI\Persistance\GamesJSON\"; 
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
            GameOfLifeController controller = new GameOfLifeController();
            bool[][] goodRequest = new bool[1][];
            goodRequest[0] = new bool[1];
            goodRequest[0][0] = true;
            ActionResult action = controller.Post(goodRequest);
            action.Should().BeEquivalentTo(controller.StatusCode(201));
        }
        [Test]
        public void given_bad_request_when_post_should_return_bad_request()
        {
            GameOfLifeController controller = new GameOfLifeController();
            bool[][] badrequest = new bool[1][];
            badrequest[0] = null;
            ActionResult action = controller.Post(badrequest);
            action.Should().BeEquivalentTo(controller.StatusCode(400));
        }
        [Test]
        public void given_good_request_when_put_should_return_ok()
        {
            GameOfLifeController controller = new GameOfLifeController();
            bool[][] goodRequest = new bool[1][];
            goodRequest[0] = new bool[1];
            goodRequest[0][0] = true;
            string path = @"C:\Users\fahernandez\source\repos\GameOfLifeAPI\GameOfLifeAPI\Persistance\GamesJSON\";
            string file = Path.Combine(path, "1.json");
            File.WriteAllText(file, JsonSerializer.Serialize(goodRequest));
            ActionResult action = controller.Put(1, null);
            action.Should().BeEquivalentTo(controller.StatusCode(200));
        }
        [Test]
        public void given_bad_request_when_put_return_bad_request() {
            int Id = -1;
            GameOfLifeController controller = new GameOfLifeController();
            ActionResult action =controller.Put(Id,null);
            action.Should().BeEquivalentTo(controller.StatusCode(400));
        }
        [Test]
        public void given_id_with_no_file_when_put_return_not_found() {
            GameOfLifeController controller = new GameOfLifeController();
            ActionResult action = controller.Put(1, null);
            action.Should().BeEquivalentTo(controller.StatusCode(404));
        }
    }
}
