using FluentAssertions;
using GameOfLifePersistance;
using KataGameOfLife;
using NUnit.Framework;
using NUnit.Framework.Internal;

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

            File.Exists(Path.Combine(path, $"{board.id}.json")).Should().BeTrue();
        }
    }
}
