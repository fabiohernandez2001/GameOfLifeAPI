using GameOfLifeAPI.Persistance;

namespace GameOfLifeAPI.Model
{
    public class BoardRepository
    {
        private readonly FileSystemBoardRepository  boardRepository;
        public BoardRepository() {
            boardRepository=new FileSystemBoardRepository();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public int CreateGameOfLife(bool[][] board) {

            int Id=boardRepository.CreateJSON<bool[][]>(board);
            if (Id < 0) {
                return -1;
            }
            return Id;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool UpdateGameOfLife(int Id) {
            bool[][] board = (bool[][])boardRepository.ReadJSON<bool[][]>(Id);
            if (board == null) {
                return false;
            }

            GameOfLife game = new GameOfLife(board, Id);
            game.Next();
            boardRepository.UpdateJSON<bool[][]>(Id, game.GetBoard());
            return true;
        }
    }
}
