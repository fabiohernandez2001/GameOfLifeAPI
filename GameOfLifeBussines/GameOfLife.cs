
using GameOfLifeAPI.Model;
using KataGameOfLife;
public class GameOfLife
{
    private readonly BoardRepository repository;

    public GameOfLife(BoardRepository repository) {
        this.repository = repository;
        
    }

    public Guid NewGame(bool[][] ecosystem) {
        var board = Board.Create(ecosystem);
        repository.Save(board);
        return board.id;
    }
    public void Next(Guid id) {
        var board = repository.Get(id);
        board.Next();
        repository.Save(board);
    }

    public bool[][] GetBoard(Guid id) {
        var board= repository.Get(id);
        return board.GetTable();
    }


}
