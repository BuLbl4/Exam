using GameLibrary;
using Microsoft.EntityFrameworkCore;

namespace DotNetExam;

public class AppDBContext : DbContext
{
    public DbSet<Monster>? Monsters { get; set; }

    public AppDBContext()
    {
    }

    public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var lizardfolk = new Monster()
        {
            Id = 1, 
            Name = "Lizardfolk",
            HitPoints = 40,
            AttackModifier = 5,
            AttackPerRound = 1,
            Damage = "1d6",
            DamageModifier = 6,
            Ac = 15
        };
        var goblin = new Monster()
        {
            Id = 2,
            Name = "Goblin",
            HitPoints = 40,
            AttackModifier = 6,
            AttackPerRound = 2,
            Damage = "1d4",
            DamageModifier = 7,
            Ac = 10 
        };
        var skeleton = new Monster()
        {
            Id = 3,
            Name = "Skeleton",
            HitPoints = 40,
            AttackModifier = 6, 
            AttackPerRound = 1, 
            Damage = "1d6", 
            DamageModifier = 8, 
            Ac = 13 
        };
        modelBuilder.Entity<Monster>().HasData(lizardfolk, goblin, skeleton);
        base.OnModelCreating(modelBuilder);
    }
}