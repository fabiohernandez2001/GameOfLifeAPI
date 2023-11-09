using GameOfLifeAPI.Model;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameOfLifeAPI.Api.Controllers
{

    /// <summary>
    /// API to play game of life
    /// </summary>
    [Route("v1/api/[controller]")]
    [ApiController]
    public class GameOfLifeControllerV1 : ControllerBase
    {
        private BoardRepository repository = new BoardRepository();

        /// <summary>
        /// Create a new game of life
        /// </summary>
        /// <remarks>;
        ///  Sample request:
        /// 
        ///      POST /board
        ///      [[true,true,true],[true,false,true],[true,true,true]]
        /// </remarks>
        /// <param name="board">the initial generation of the game</param>
        /// <response code="201">When the game is created</response>
        /// <response code="400">When the board is not well-defined</response>
        // POST api/<GameOfLifeController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] bool[][] board) {
            
            for (int i = 0; i < board.Length; i++) {
                if (board[i]==null){ return BadRequest(); }
                for (int j = 0; j < board[i].Length; j++) {
                    if (board[i][j]==null){ return BadRequest(); }
                }
            }

            GameOfLife game = new GameOfLife(board, repository);
            var id = game.GetId();
            if (id < 0) {
                return BadRequest();

            }
            return Created("Game", id);
        }
        /// <summary>
        /// Next Generation of an existing game life
        /// </summary>
        /// <param name="id">the id of the game you want to update</param>
        /// <param name="dummy">not important</param>
        /// <returns></returns>
        /// <response code="200">The next generation is complete</response>
        /// <response code="400">When the Id is not valid</response>
        /// <response code="404">When the Id does not belong to any game</response>
        // PUT api/<GameOfLifeController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Put(int id, [FromBody] bool[][] dummy) {
            if (id <= 0) {
                return BadRequest();

            }
            GameOfLife game = new GameOfLife(dummy, repository, id);
            if (game.GetId()==0) {
                return NotFound();

            } 
            return Ok();
        }

    }
}
