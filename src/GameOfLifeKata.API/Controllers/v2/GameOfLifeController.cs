using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameOfLifeKata.API.Controllers.v2; 

/// <summary>
/// API to play game of life
/// </summary>
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion(2)]
[ApiController]
public class GameOfLifeController : ControllerBase
{
    private readonly GameOfLife game;

    public GameOfLifeController(GameOfLife game)
    {
        this.game = game;
    }
    /// <summary>
    /// Create a new game of life
    /// </summary>
    /// <remarks>;
    ///  Sample request:
    /// 
    ///      POST /board
    ///      [[3,3],[0,0],[0,1],[0,2],
    ///      [1,0],[1,2],[2,0],[2,1],[2,2]]
    /// </remarks>
    /// <param name="board">the first pair, indicates the dimension of the board, the others pairs, indicates de coordinates of alive cells</param>
    /// <response code="201">When the game is created</response>
    /// <response code="400">When the board is not well-defined</response>
    // POST api/<GameOfLifeController>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult Post([FromBody] int[][] board)
    {

        EcosystemBuilder builder = new EcosystemBuilder(board[0][0], board[0][1]);
        for (int i = 1; i < board.Length; i++)
        {
            builder.WithAliveCell(board[i][0], board[i][1]);
        }
        var id = game.NewGame(builder.Build());

        return Created("Game", id);
    }
    /// <summary>
    /// Next Generation of an existing game life
    /// </summary>
    /// <param name="id">the id of the game you want to update</param>
    /// <returns></returns>
    /// <response code="200">The next generation is complete</response>
    /// <response code="404">When the Id does not belong to any game</response>
    // PUT api/<GameOfLifeController>/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult Put(Guid id)
    {
        try
        {
            game.Next(id);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }

}