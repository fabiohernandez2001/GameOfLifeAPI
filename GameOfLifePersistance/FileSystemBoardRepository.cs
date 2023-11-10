using GameOfLifeAPI.Model;
using System.Text.Json;
using KataGameOfLife;

namespace GameOfLifePersistance; 

public class FileSystemBoardRepository : BoardRepository
{
    private Random random = new Random();
    private string path;
    public FileSystemBoardRepository(string path) {
        this.path = path;
    }

    public void Save(Board board) {
        BoardDTO boardDto = board.ToDTO();
        string content = JsonSerializer.Serialize(boardDto);
        string filepath= Path.Combine(path, $"{boardDto.Id}.json");
        File.WriteAllText(filepath, content);
    }

    public Board Get(Guid id) {
        var filepath = Path.Combine(path, $"{id}.json");
        BoardDTO boardDto = JsonSerializer.Deserialize<BoardDTO>(File.ReadAllText(filepath));
        return boardDto.ToBussines();
    }

    /*
    public int FindNewIdJSON()
    {
        int Id = random.Next();
        string[] JSON = Directory.GetFiles(path, Id.ToString() + ".json");
        while (JSON.Length > 0)
        {
            Id = random.Next();
            JSON = Directory.GetFiles(path, Id.ToString() + ".json");
        }
        return Id;
    }
    public string GetPath() {
        return path;
    }

    public bool FindJSON(string Id) {
        string pathfile = Path.Combine(path, Id + ".json");
        return File.Exists(pathfile);
    }
    public void CreateJSON<T>(string Id, Object boardDto) {
        string pathfile = Path.Combine(path, Id +".json");
        WriteJSON<T>(pathfile, boardDto);
    }
    public Object ReadJSON<T>(int id)
    {
        string[] JSON = Directory.GetFiles(path, id.ToString() + ".json");
        if (JSON.Length == 0) {
            return null;

        }
        string content = File.ReadAllText(JSON[0]);
        var deserialize = JsonSerializer.Deserialize<T>(content);
        if (deserialize != null)
        {
            return deserialize;
        }
        else
        {
            throw new Exception();
        }
    }
    public bool UpdateJSON<T>(int Id, Object boardDto) {
        string[] JSON = Directory.GetFiles(path,Id.ToString()+ ".json");
        string pathfile = Path.Combine(path,Id.ToString() + ".json");
        if (JSON.Length == 0)
        {
            return false;
        }

        WriteJSON<T>(pathfile, boardDto);
        return true;
    }


    public void DeleteJSON(int id)
    {
        string[] JSON = Directory.GetFiles(path, id.ToString() + ".json");
        File.Delete(JSON[0]);
    }
    private void WriteJSON<T>(string pathfile, Object content) {
        var SerializedGame = JsonSerializer.Serialize(content);
        File.WriteAllText(pathfile, SerializedGame);
    }
    */

}

public static class BoardExtensions {
    public static BoardDTO ToDTO(this Board board) {
        var cellDtos = new List<CellDTO>();
        cellDtos.AddRange(board.cells.Select(c=>c.ToDTO()));
        return new BoardDTO {
            Id=board.id,
            X=board.x,
            Y=board.y,
            Cells= cellDtos
        };
    }
    public static Board ToBussines(this BoardDTO boardDto)
    {
        var cells = new List<Cell>();
        cells.AddRange(boardDto.Cells.Select(c => c.ToBussines()));
        return new Board(boardDto.Id, boardDto.X, boardDto.Y, cells);

    }
}
public static class CellExtensions
{
    public static CellDTO ToDTO(this Cell cell) {
        return new CellDTO {
            State=cell.state,
            X=cell.x,
            Y=cell.y,
        };
    }
    public static Cell ToBussines(this CellDTO cellDto)
    {
        return new Cell(cellDto.State==State.Alive, cellDto.X, cellDto.Y);
    }
}
public class BoardDTO {
    public List<CellDTO> Cells { get; set; }
    public int X{ get; set; }
    public int Y { get; set; }
    public Guid Id { get; set; }
}

public class CellDTO {
    public State State { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}