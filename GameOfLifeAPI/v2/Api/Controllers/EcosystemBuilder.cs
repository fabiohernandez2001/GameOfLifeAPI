namespace KataGameOfLifeTests;

public class EcosystemBuilder
{
    private bool[][] ecosystem;
    public EcosystemBuilder(int x, int y, bool areAlive = false)
    {
        ecosystem = new bool[x][];

        if(areAlive) {
            for(int i = 0; i < x; i++) {
                ecosystem[i] = new bool[y];
                for(int j = 0; j < y; j++)
                {
                    ecosystem[i][j] = true;
                }
            }
        }
        else {
            for (int i = 0; i < x; i++) {
                ecosystem[i]= new bool[y];
            }
        }
    }
    public EcosystemBuilder WithAliveCell(int x, int y) 
    {
        ecosystem[x][y] = true;
        return this;
    }
    public EcosystemBuilder WithDeadCell(int x, int y)
    {
        ecosystem[x][y] = false;
        return this;
    }
    public bool[][] Build() 
    {
        return ecosystem;
    }
}