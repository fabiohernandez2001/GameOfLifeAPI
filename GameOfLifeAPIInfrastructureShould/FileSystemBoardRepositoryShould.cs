using System.Text.Json;
using FluentAssertions;
using GameOfLifeInfrastructure.DTO;
using GameOfLifeInfrastructure.Extensions;
using GameOfLifePersistance;
using KataGameOfLife;
using NUnit.Framework;


namespace GameOfLifeAPIInfrastructureShould
{
    
    internal class FileSystemBoardRepositoryShould
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "assets");
        private FileSystemBoardRepository fileSystem;

        [SetUp]
        public void Setup() {
            Directory.CreateDirectory(path);
            fileSystem = new FileSystemBoardRepository(path);
        }
        [Test]
        public void save_the_given_board() {
            var initialState = new bool[1][];
            initialState[0] = new bool[1];
            Board board = Board.Create(initialState);
            

            fileSystem.Save(board);

            var filepath = Path.Combine(path, $"{board.id}.json");
            File.Exists(filepath).Should().BeTrue();
            BoardDTO boardDto = JsonSerializer.Deserialize<BoardDTO>(File.ReadAllText(filepath));
            boardDto.Should().BeEquivalentTo(board.ToDTO());
            File.Delete(filepath);
        }

        [Test]
        public void get_a_board_from_file() {
            var id = Guid.Parse("4a669889-399f-4854-a5b8-409ec5566b71");

            var board = fileSystem.Get(id);

            var initialState = new bool[1][];
            initialState[0] = new bool[1];
            Board expected_board = Board.Create(initialState);
            board.Should().BeEquivalentTo(expected_board);
        }

        [Test]
        public void throw_an_exception_when_the_given_id_not_exist() {
            var id = Guid.NewGuid();

            var action = () => fileSystem.Get(id);

            action.Should().Throw<IOException>();
        }
    }
}
