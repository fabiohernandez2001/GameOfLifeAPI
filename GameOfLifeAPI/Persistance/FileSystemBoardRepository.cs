
using System.Text.Json;
namespace GameOfLifeAPI.Persistance
{
    public class FileSystemBoardRepository
    {
        private string path = @"C:\Users\fahernandez\source\repos\GameOfLifeAPI\GameOfLifeAPI\Persistance\GamesJSON\";
        private Random random = new Random();
        public FileSystemBoardRepository() {

        }

        public string GetPath() {
            return path;
        }
        public int CreateJSON<T>(Object board) {
            int Id = FindNewIdJSON();
            string pathfile = Path.Combine(path, Id.ToString()+".json");
            if (File.Exists(pathfile)) {
                return -1;
            }

            WriteJSON<T>(pathfile, board);
            return Id;
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
        private int FindNewIdJSON()
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
        
    }
}
