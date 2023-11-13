

using GameOfLifeInfrastructure.DTO;
using GameOfLifePersistance;
using KataGameOfLife;

namespace GameOfLifeInfrastructure.Extensions
{
    public static class BoardExtensions
    {
        public static BoardDTO ToDTO(this Board board)
        {
            var cellDtos = new List<CellDTO>();
            cellDtos.AddRange(board.cells.Select(c => c.ToDTO()));
            return new BoardDTO
            {
                Id = board.id,
                X = board.x,
                Y = board.y,
                Cells = cellDtos
            };
        }
        public static Board ToBussines(this BoardDTO boardDto)
        {
            var cells = new List<Cell>();
            cells.AddRange(boardDto.Cells.Select(c => c.ToBussines()));
            return new Board(boardDto.Id, boardDto.X, boardDto.Y, cells);

        }
    }
}
