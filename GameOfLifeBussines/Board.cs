namespace KataGameOfLife
{
    public class Board
    {
        public List<Cell> cells { get; private set; }
        public int x{ get; }
        public int y{ get; }
        public Guid id { get; }

        public static Board Create(bool[][] initialState) {
            var id = Guid.NewGuid();
            var cells = new List<Cell>();
            var x = initialState.Length;
            var y = initialState[0].Length;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    cells.Add(new Cell(initialState[i][j], i, j));
                }
            }

            return new Board(id, x, y, cells);
        }
        

        public Board(Guid id, int x, int y, List<Cell> cells) {
            this.id = id;
            this.x = x;
            this.y = y;
            this.cells = cells;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Board)) return false;
            return Equals(obj as Board);
        }

        public bool[][] GetTable() 
        {
            bool[][] board= new bool[x][];
            for (int i = 0; i < x; i++) {
                board[i] = new bool[y];}

            foreach (var cell in cells) {
                board[cell.getX()][cell.getY()] = cell.IsAlive();
            }

            return board;
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
