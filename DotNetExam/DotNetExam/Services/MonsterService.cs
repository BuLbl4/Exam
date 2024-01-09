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
        IEnumerable<Monster> monsters = _context.Monsters!;
        var rnd = new Random();
        var id = rnd.Next(1,monsters.Count() + 1);
        return monsters.First(m => m.Id == id);
    }
}