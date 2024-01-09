using DotNetExam.Services;
using GameLibrary;
using Microsoft.AspNetCore.Mvc;

namespace DotNetExam.Controllers;

[ApiController]

public class MonsterController : ControllerBase
{
    private readonly IMonsterService _monsterService;

   
    public MonsterController( IMonsterService monsterService)
    {
        _monsterService = monsterService;
    }

    [HttpGet]
    [Route("GetRandomMonster")]
    public Monster GetRandomMonster()
    {
        var result =  _monsterService.GetRandomMonster();
        return result;
    }
}