using GameOfLifePersistance;
using KataGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameOfLifeInfrastructure.DTO;

namespace GameOfLifeInfrastructure.Extensions
{
    public static class CellExtensions
    {
        public static CellDTO ToDTO(this Cell cell)
        {
            return new CellDTO
            {
                State = cell.state,
                X = cell.x,
                Y = cell.y,
            };
        }
        public static Cell ToBussines(this CellDTO cellDto)
        {
            return new Cell(cellDto.State == State.Alive, cellDto.X, cellDto.Y);
        }
    }
}
