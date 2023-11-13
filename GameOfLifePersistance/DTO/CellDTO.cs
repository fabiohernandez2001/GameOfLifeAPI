using KataGameOfLife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLifeInfrastructure.DTO
{
    public class CellDTO
    {
        public State State { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
