using GameLibrary;

namespace DotNetExam.Services;

public interface IGameLogicService
{
    public List<Round> ProcessGame(Player player, Monster enemy);
}