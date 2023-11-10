
using FluentAssertions;
using NUnit.Framework;

namespace GameOfLifeAPITest.Bussines
{
    [TestClass]
    public class GameOfLifeShould
    {
        [TearDown]
        public void Delete()
        {
            string path = @"C:\Users\fahernandez\source\repos\GameOfLifeAPI\GameOfLifePersistance\GamesJSON\";
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            if (files.Length > 0)
            {
                var recentFile = files.OrderByDescending(f => f.LastWriteTime).FirstOrDefault();
                string pathfile = Path.Combine(path, recentFile.Name);
                File.Delete(pathfile);
            }
        }
        [Test]
        public void given_neigborhood_should_return_more()
        {
            
            bool[][] ecosystem = new bool[3][];
            bool[] row1 = { false, true, false };
            bool[] row2 = { true, true, true };
            bool[] row3 = { false, true, false };
            ecosystem[0] = row1;
            ecosystem[1] = row2;
            ecosystem[2] = row3;

            GameOfLife gameOfLife = new GameOfLife(ecosystem);

            gameOfLife.Next();
            bool[][] expected_ecosystem = new bool[3][];
            bool[] row4 = { true, true, true };
            bool[] row5 = { true, false, true };
            bool[] row6 = { true, true, true };
            expected_ecosystem[0] = row4;
            expected_ecosystem[1] = row5;
            expected_ecosystem[2] = row6;

            var expectedGame = new GameOfLife(expected_ecosystem);
            gameOfLife.Equals(expectedGame).Should().BeTrue();
        }
        [Test]
        public void given_empty_neigborhood_should_return_same()
        {
            
            bool[][] ecosystem = new bool[3][];
            bool[] row1 = { false, false, false };
            bool[] row2 = { false, false, false };
            bool[] row3 = { false, false, false };
            ecosystem[0] = row1;
            ecosystem[1] = row2;
            ecosystem[2] = row3;

            GameOfLife gameOfLife = new GameOfLife(ecosystem);

            gameOfLife.Next();
            bool[][] expected_ecosystem = new bool[3][];
            bool[] row4 = { false, false, false };
            bool[] row5 = { false, false, false };
            bool[] row6 = { false, false, false };
            expected_ecosystem[0] = row4;
            expected_ecosystem[1] = row5;
            expected_ecosystem[2] = row6;

            var expectedGame = new GameOfLife(expected_ecosystem);
            gameOfLife.Should().BeEquivalentTo(expectedGame);
        }

        [Test]
        public void given_neigborhood_should_return_lower()
        {
            
            bool[][] ecosystem = new bool[3][];
            bool[] row1 = { true, true, true };
            bool[] row2 = { true, false, true };
            bool[] row3 = { true, true, true };
            ecosystem[0] = row1;
            ecosystem[1] = row2;
            ecosystem[2] = row3;

            GameOfLife gameOfLife = new GameOfLife(ecosystem);

            gameOfLife.Next();
            bool[][] expected_ecosystem = new bool[3][];
            bool[] row4 = { true, false, true };
            bool[] row5 = { false, false, false };
            bool[] row6 = { true, false, true };
            expected_ecosystem[0] = row4;
            expected_ecosystem[1] = row5;
            expected_ecosystem[2] = row6;

            var expectedGame = new GameOfLife(expected_ecosystem);
            gameOfLife.Should().BeEquivalentTo(expectedGame);
        }
    }
}
