

using GameOfLifePersistance;

namespace GameOfLifeInfrastructure.DTO
{
    public class BoardDTO
    {
        public List<CellDTO> Cells { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public Guid Id { get; set; }
    }
}
