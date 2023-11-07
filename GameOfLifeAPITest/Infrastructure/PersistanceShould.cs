using FluentAssertions;
using GameOfLifeAPI.Persistance;
using NUnit.Framework;

namespace GameOfLifeAPITest.Infrastructure
{
       [TestClass]
       public class PersistanceShould
       {
           FileSystemBoardRepository FileSystem =new FileSystemBoardRepository();
           
           [Test]
           public void given_new_json_when_reading_then_be_equal() {
               bool[][] bools = new bool[3][];
               bool[] row1 = { false, true, false };
               bool[] row2 = { true, true, true };
               bool[] row3 = { false, true, false };
               bools[0] = row1;
               bools[1] = row2;
               bools[2] = row3;
               int Id=FileSystem.FindNewIdJSON();
               FileSystem.CreateJSON<bool[][]>(Id.ToString(),bools);
               bool[][] resultedEcosystem=(bool[][])FileSystem.ReadJSON<bool[][]>(Id);
               FileSystem.DeleteJSON(Id); 
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
               bool[] row4 = { false, true, false };
               bool[] row5 = { true, true, true };
               bool[] row6 = { false, true, false };
               new_bools[0] = row4; 
               new_bools[1] = row5; 
               new_bools[2] = row6;

               int Id = FileSystem.FindNewIdJSON();
               FileSystem.CreateJSON<bool[][]>(Id.ToString(), bools);
               FileSystem.UpdateJSON<bool[][]>(Id,new_bools);
               bool[][] readBools = (bool[][])FileSystem.ReadJSON<bool[][]> (Id);
               FileSystem.DeleteJSON(Id);
               readBools.Should().BeEquivalentTo(new_bools);
               readBools.Should().NotBeEquivalentTo(bools);
           }

       }
    
}
