using GameOfLifeAPI.Persistance;
using KataGameOfLife;

namespace GameOfLifeAPI.Model
{
    public class BoardRepository
    {
        private readonly FileSystemBoardRepository  fileSystem;
        public BoardRepository() {
            fileSystem=new FileSystemBoardRepository();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>

        public int GetIdJSON(int Id) {
            if (Id != 0) {
                if (fileSystem.FindJSON(Id.ToString()))
                {
                    return Id;
                }
                return 0;
            }

            int newId = fileSystem.FindNewIdJSON();
            return newId;
        }
        public bool[][] GetGameOfLife(int Id, bool[][] board) {
            if (Id != 0) {
                if (!fileSystem.FindJSON(Id.ToString())) {
                    bool[][] falseBoard = new bool[1][];
                    falseBoard[0] = new bool[1];
                    falseBoard[0][0] = false;
                    return falseBoard;
                }
                return (bool[][])fileSystem.ReadJSON<bool[][]>(Id);
            }
            bool[][] newBoard = board;
            fileSystem.CreateJSON<bool[][]>(Id.ToString(), board);
            return newBoard;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool UpdateGameOfLife(int Id, bool[][] newBoard) {
            bool[][] board = (bool[][])fileSystem.ReadJSON<bool[][]>(Id);
            if (board == null) {
                return false;
            }

            fileSystem.UpdateJSON<bool[][]>(Id, newBoard);
            return true;
        }
    }
}
