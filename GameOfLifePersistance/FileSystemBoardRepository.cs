using GameOfLifeAPI.Model;
using System.Text.Json;
using KataGameOfLife;

namespace GameOfLifePersistance
{
    public class FileSystemBoardRepository : BoardRepository
    {
        private Random random = new Random();
        private string path;
        public FileSystemBoardRepository(string path) {
        this.path = path;
    }

        public void Save(Board board) {

        }

        public Board Get(int id) {
            throw new NotImplementedException();
        }

        public int save(int id, bool[][] boardBools) {
            int newid;
            string serializedGame;
            if (id == 0) {
                newid = random.Next();
                serializedGame = JsonSerializer.Serialize(boardBools);
            }
            else {
                newid = id;
                bool[][] board = get(newid);
                GameOfLife game = new GameOfLife(board);
                game.Next();
                serializedGame = JsonSerializer.Serialize(game.GetBoard());
            }
            string pathfile = Path.Combine(path, newid.ToString() + ".json");
            File.WriteAllText(pathfile, serializedGame);
            return newid;
        }

        public bool[][]? get(int id) {
            string[] JSON = Directory.GetFiles(path, id.ToString() + ".json");
            if (JSON.Length == 0)
            {
                return null;

            }
            string content = File.ReadAllText(JSON[0]);
            var deserialize = JsonSerializer.Deserialize<bool[][]>(content);
            if (deserialize != null)
                return  deserialize;
            else
                return null;
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
        public void CreateJSON<T>(string Id, Object board) {
            string pathfile = Path.Combine(path, Id +".json");
            WriteJSON<T>(pathfile, board);
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
        public bool UpdateJSON<T>(int Id, Object board) {
            string[] JSON = Directory.GetFiles(path,Id.ToString()+ ".json");
            string pathfile = Path.Combine(path,Id.ToString() + ".json");
            if (JSON.Length == 0) 
            {
                return false;
            }
            
            WriteJSON<T>(pathfile, board);
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
}
