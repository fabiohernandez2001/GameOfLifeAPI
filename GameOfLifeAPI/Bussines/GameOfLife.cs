
using GameOfLifeAPI.Model;
using KataGameOfLife;
public class GameOfLife
{

    private Board board;
    private int Id;
    private BoardRepository repository;
    public GameOfLife(bool[][] ecosystem, BoardRepository repository, int Id=0) {
        this.repository = repository;
        this.Id= repository.GetIdJSON(Id);
        this.board = new Board(repository.GetGameOfLife(Id, ecosystem));
    }
    public void Next()
    {
        board.Next();
        repository.UpdateGameOfLife(Id, board.GetTable());
    }

    public int GetId() {
        return Id;
    }
    internal bool[][] GetBoard() {
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
