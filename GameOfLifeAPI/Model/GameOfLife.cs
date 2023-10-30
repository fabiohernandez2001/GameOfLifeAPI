using KataGameOfLife;
public class GameOfLife
{
    private Board board;
    private int Id;

    public GameOfLife(bool[][] ecosystem)
	{
        board = new Board(ecosystem);
        this.Id = Id;
    }
    public void Next()
    {
        board.Next();
    }

    public string GetBoard() {
        return board.GetBoard();
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
