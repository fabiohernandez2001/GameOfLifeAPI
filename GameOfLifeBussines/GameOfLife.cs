
using KataGameOfLife;
public class GameOfLife
{

    private Board board;
    private int Id;
    public GameOfLife(bool[][] ecosystem, int Id=0) {
        this.Id = Id;
        this.board = Board.Create(ecosystem);
    }
    public void Next()
    {
        board.Next();
    }

    public int GetId() {
        return Id;
    }
    public bool[][] GetBoard() {
        return board.GetTable();
    }

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != typeof(GameOfLife)) return false;
        return Equals(obj as GameOfLife);
    }

    private bool Equals(GameOfLife other)
    {
        return this.board.Equals(other.board);
    }


}
