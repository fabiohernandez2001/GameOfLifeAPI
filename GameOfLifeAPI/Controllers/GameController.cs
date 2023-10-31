using GameOfLifeAPI.Persistance;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameOfLifeAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        
        // POST api/<GameController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] bool[][] board) {
            GameOfLife Game = new GameOfLife(board);
            if ( Game.GetId()>=0){ return Created(nameof(Game), Game.GetId()); }

            return BadRequest();

        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Put(int id, [FromBody] bool[][] dummy) {
            if (id == 0) {
                return BadRequest(); }
            GameOfLife game = new GameOfLife(dummy, id);
            game.Next();
            return Ok();
        }

    }
}
