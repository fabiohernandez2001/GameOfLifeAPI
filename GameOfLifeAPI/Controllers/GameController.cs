using GameOfLifeAPI.Persistance;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameOfLifeAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        JSONUtilities JSON = new JSONUtilities();
        
        // POST api/<GameController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Post([FromBody] bool[][] board) {
            GameOfLife Game = new GameOfLife(board);
            int id = JSON.CreateFile(Game);
            if ( id>=0){ return Created(nameof(Game), id); }

            return BadRequest();

        }

        // PUT api/<GameController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Put(int id, [FromBody] bool[][] board) {

            return Ok();
        }

    }
}
