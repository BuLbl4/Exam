using GameLibrary;

namespace DotNetExam.Services;

public class MonsterService : IMonsterService
{
    private readonly AppDBContext _context;

    public MonsterService(AppDBContext context)
    {
        _context = context;
    }
   
    public Monster GetRandomMonster()
    {
        var rnd = new Random();
        var randomIndex = rnd.Next(0, _context.Monsters!.Count() + 1);
        return _context.Monsters!.OrderBy(m => m.Id).Skip(randomIndex).FirstOrDefault()!;
    }
}