namespace KataGameOfLife
{
    public class Board
    {
        private List<Cell> cells;
        public Board(bool[][] initialState) 
        {
            cells = new List<Cell>();
            for (int i = 0; i < initialState.Length; i++)
            {
                for (int j = 0; j < initialState[0].Length; j++)
                {
                    cells.Add(new Cell(initialState[i][j], i, j));
                }
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Board)) return false;
            return Equals(obj as Board);
        }

        public string GetBoard() 
        {
            string board = string.Empty;
            foreach (var cell in cells) {
                board += cell.ToString() + ", ";
            }

            return board.SkipLast(1).ToString();
        }

        public void Next()
        {
            var nextGen = cells.Select(cell => (Cell) cell.Clone()).ToList();
            for (int i = 0; i < cells.Count; i++)
            {
                int population = Neighborhood(cells[i]);
                nextGen[i].UpdateState(population);
            }
            cells = nextGen;
        }
        private int Neighborhood(Cell cell)
        {
            int NeighbohrdsAlive = 0;

            foreach (Cell possible_neighborh in cells)
            {
                if (possible_neighborh == cell) continue;
                if (possible_neighborh.IsAlive())
                {
                    if (cell.IsNeighbohr(possible_neighborh)) { NeighbohrdsAlive++; }
                }
            }
            return NeighbohrdsAlive;
        }
        private bool Equals(Board other)
        {
            return cells.SequenceEqual(other.cells);
        }
    }
}
