using FluentAssertions;
using GameOfLifePersistance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace GameOfLifeAPITest.Infrastructure
{
       [TestClass]
       public class PersistanceShould
       {
           FileSystemBoardRepository FileSystem =new FileSystemBoardRepository();
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
           public void given_new_json_when_reading_then_be_equal() {
               bool[][] bools = new bool[3][];
               bool[] row1 = { false, true, false };
               bool[] row2 = { true, true, true };
               bool[] row3 = { false, true, false };
               bools[0] = row1;
               bools[1] = row2;
               bools[2] = row3;
               int Id=FileSystem.save(0, bools);
               bool[][] resultedEcosystem=FileSystem.get(Id);
               bools.Should().BeEquivalentTo(resultedEcosystem);

           }

           [Test]
           public void given_json_when_writing_then_be_not_equal() {

               bool[][] bools = new bool[3][];
               bool[] row1 = { true, true, true };
               bool[] row2 = { true, true, true };
               bool[] row3 = { true, true, true };
               bools[0] = row1;
               bools[1] = row2;
               bools[2] = row3;

               bool[][] new_bools = new bool[3][];
               bool[] row4 = { true, false, true };
               bool[] row5 = { false, false, false };
               bool[] row6 = { true, false, true };
               new_bools[0] = row4; 
               new_bools[1] = row5; 
               new_bools[2] = row6;

               int Id = FileSystem.save(0, bools);
               FileSystem.save(Id, bools);
               bool[][] readBools = FileSystem.get(Id);
               readBools.Should().BeEquivalentTo(new_bools);
               readBools.Should().NotBeEquivalentTo(bools);
           }

       }
    
}
