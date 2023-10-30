
namespace KataGameOfLife;
public class Cell : ICloneable
{
	private State state;
	private int x;
	private int y;
	public Cell(bool isAlive, int x, int y)
	{
        this.state = isAlive? State.Alive: State.Dead;
        this.x = x;	
		this.y = y;
	}
	public bool IsAlive() {  return this.state == State.Alive;  }

	public bool IsNeighbohr(Cell otherCell)
	{
		int distx = Math.Abs(otherCell.x - this.x);
		int disty = Math.Abs(otherCell.y - this.y);
		if (distx <= 1 && disty <= 1) { return true; }
		return false;
	}
    public override bool Equals(object? obj)
    {
		if (obj == null || obj.GetType() != typeof(Cell)) return false;
		return Equals(obj as Cell);
    }

    public void UpdateState(int population)
    {
        if (IsAlive() && (population < 2 || population > 3))
        {
            this.state = State.Dead;
        }
        if (!IsAlive() && population == 3)
        {
            this.state = State.Alive;
        }
    }
    public override string ToString()
    {
        return $"[State: {state}, Position: {x} , {y} ]";
    }

    public object Clone()
    {
        return new Cell(this.IsAlive(), x, y);
    }
    private bool Equals(Cell otherCell)
	{
		return this.x == otherCell.x
			&& this.y == otherCell.y
			&& this.state == otherCell.state;
	}

    
}
