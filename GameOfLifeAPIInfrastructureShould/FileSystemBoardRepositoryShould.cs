using System.Text.Json;
using FluentAssertions;
using GameOfLifePersistance;
using KataGameOfLife;
using NUnit.Framework;


namespace GameOfLifeAPIInfrastructureShould
{
    
    internal class FileSystemBoardRepositoryShould
    {
        string path = Path.Combine(TestContext.CurrentContext.TestDirectory, "Saved");

        [SetUp]
        public void Setup() {
            Directory.CreateDirectory(path);
        }
        [Test]
        public void save_the_given_board() {
            var initialState = new bool[1][];
            initialState[0] = new bool[1];
            Board board = new Board(initialState);
            FileSystemBoardRepository fileSystem = new FileSystemBoardRepository(path);

            fileSystem.Save(board);

            var filepath = Path.Combine(path, $"{board.id}.json");
            File.Exists(filepath).Should().BeTrue();
            BoardDTO boardDto = JsonSerializer.Deserialize<BoardDTO>(File.ReadAllText(filepath));
            boardDto.Should().BeEquivalentTo(board.ToDTO());
        }
    }
}
