using GameOfLifeAPI.Persistance;
using KataGameOfLife;
public class GameOfLife
{
    private Board board;
    private int Id;
    private JSONUtilities JSONUtilities = new JSONUtilities();
    public GameOfLife(bool[][] ecosystem, int Id=0)
    {
        if (Id == 0) {
            this.Id = JSONUtilities.FindIdJSON();
            board = new Board(ecosystem);
            JSONUtilities.CreateJSON(this);
        }
        else {
            this.Id = Id;
            board = new Board(JSONUtilities.ReadJSON(Id));
        }
    }
    public void Next()
    {
        board.Next();
        JSONUtilities.UpdateJSON(this);
    }

    internal int GetId() {
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
