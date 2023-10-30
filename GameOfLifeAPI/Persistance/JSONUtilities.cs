using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.Json;
namespace GameOfLifeAPI.Persistance
{
    public class JSONUtilities {
        private string path = @"C:\Users\fahernandez\source\repos\GameOfLifeAPI\GameOfLifeAPI\Persistance\GamesJSON\";
        Random random = new Random();
        public  JSONUtilities() {

        }

        public int CreateFile(GameOfLife game) 
        {
            int Id = random.Next();
            string rutaArchivo = Path.Combine(path, Id.ToString());
            if (File.Exists(rutaArchivo)) {
                return -1;
            }

            File.WriteAllText(rutaArchivo, game.GetBoard());

            return Id;
        }
    }
}
