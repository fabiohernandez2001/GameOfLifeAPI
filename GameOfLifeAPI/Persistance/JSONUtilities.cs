using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
namespace GameOfLifeAPI.Persistance
{
    public class JSONUtilities {
        private string path = @"C:\Users\fahernandez\source\repos\GameOfLifeAPI\GameOfLifeAPI\Persistance\GamesJSON\";
        private Random random = new Random();
        public  JSONUtilities() {

        }

        public int FindIdJSON() {
            int Id = random.Next();
            return Id;
        }

        public int CreateJSON(GameOfLife game) {
            int Id = game.GetId();
            string pathfile = Path.Combine(path, Id.ToString()+".json");
            if (File.Exists(pathfile)) {
                return -1;
            }

            WriteJSON(pathfile, game);
            return Id;
        }

        public bool UpdateJSON(GameOfLife game) {
            string[] JSON = Directory.GetFiles(path,game.GetId().ToString()+ ".json");
            string pathfile = Path.Combine(path, game.GetId().ToString() + ".json");
            if (JSON.Length == 0) 
            {
                return false;
            }
            
            WriteJSON(pathfile, game);
            return true;
        }

        public bool[][] ReadJSON(int id) {
            string[] JSON = Directory.GetFiles(path, id.ToString() + ".json");
            string content = File.ReadAllText(JSON[0]);
            bool[][] ecosystem = JsonSerializer.Deserialize<bool[][]>(content);
            if (ecosystem != null) {
                return ecosystem;
            }
            else {
                throw new Exception();
            }
        }
        private void WriteJSON(string pathfile, GameOfLife game) {
            var SerializedGame = JsonSerializer.Serialize(game.GetBoard());
            File.WriteAllText(pathfile, SerializedGame);
        }
        
    }
}
