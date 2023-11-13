using GameOfLifeAPI.Model;
using System.Text.Json;
using GameOfLifeInfrastructure.DTO;
using GameOfLifeInfrastructure.Extensions;
using KataGameOfLife;

namespace GameOfLifePersistance; 

public class FileSystemBoardRepository : BoardRepository
{
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
}





