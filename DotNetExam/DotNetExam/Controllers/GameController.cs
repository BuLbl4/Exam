using DotNetExam.Services;
using GameLibrary;
using Microsoft.AspNetCore.Mvc;

namespace DotNetExam.Controllers;

public class GameController : ControllerBase
{
    private readonly IGameLogicService _gameLogic;

    
    public GameController(IGameLogicService gameLogic)
    {
        _gameLogic = gameLogic;
    }
 
    [HttpPost]
    [Route("Game")]
    public List<Round> Game([FromBody]Fight fight)
    {
        return _gameLogic.ProcessGame(fight.Player!, fight.Monster!);
    }
}