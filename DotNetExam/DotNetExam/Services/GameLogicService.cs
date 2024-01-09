using DotNetExam.Models;
using DotNetExam.Services;
using GameLibrary;

namespace Bl.Services;

public class GameLogicService : IGameLogicService
{
     public List<Round> ProcessGame(Player player, Monster enemy)
    {
        var rounds = new List<Round>();
        var roundId = 1;

        while (player.HitPoints > 0 && enemy.HitPoints > 0)
        {
            var round = new Round
            {
                Id = roundId++,
                Rounds = new List<FightResult>()
            };

            round.Rounds.Add(new FightResult { Message = $"Раунд {round.Id}", IsRoundEnd = false });
            Console.WriteLine($"Раунд {round.Id}");
            for (int i = 0; i < player.AttackPerRound; i++)
            {
                var attackRoll = RollAttack();
                Console.WriteLine($"вы выбили {attackRoll}");
                if (attackRoll == 20)
                {
                    // Критическое попадание
                    var damage = RollDamage(player.DamageModifier, player.Damage) * 2;
                    enemy.HitPoints -= damage;
                    round.Rounds.Add(new FightResult { Message = $"Вы нанесли Критическое попадание!" +
                    $" Нанесено урона:({player.DamageModifier} + {damage / 2 - player.DamageModifier}) * 2." +
                    $" У противника осталось {enemy.HitPoints} HP.", IsRoundEnd = false });
                   
                }
                else if (attackRoll == 1)
                {
                    // Критический промах
                    round.Rounds.Add(new FightResult { Message = "Вы Критически промахнулись!", IsRoundEnd = false });
                }
                else if (attackRoll + player.AttackModifier > enemy.Ac)
                {
                    // Обычное попадание
                    var damage = RollDamage(player.DamageModifier, player.Damage);
                    enemy.HitPoints -= damage;
                    round.Rounds.Add(new FightResult { Message = $"({player.AttackModifier} + {attackRoll}) больше {enemy.Ac}, " + 
                    $"Вы попали!  Нанесено урона: {damage - player.DamageModifier} + {player.DamageModifier}. У противника осталось " +
                    $"{enemy.HitPoints} HP.", IsRoundEnd = false });
                 
                }
                else
                {
                    // Промах
                    round.Rounds.Add(new FightResult { Message = "Вы промахнулись!", IsRoundEnd = false });
                }
            }


            for (int i = 0; i < enemy.AttackPerRound; i++)
            {
                if (enemy.HitPoints > 0)
                {
                    var enemyAttackRoll = RollAttack();

                    if (enemyAttackRoll == 20)
                    {
                        // Критическое попадание противника
                        var damage = RollDamage(enemy.DamageModifier, enemy.Damage) * 2;
                        player.HitPoints -= damage;
                        round.Rounds.Add(new FightResult { Message = $"Противник наносит критический урон! Получено урона:" +
                                                        $" ({enemy.DamageModifier} + {damage / 2 - enemy.DamageModifier}) * 2." +
                                                        $" У вас осталось {player.HitPoints} HP.", IsRoundEnd = false });
                     
                    }
                    else if (enemyAttackRoll == 1)
                    {
                        // Критический промах противника
                        round.Rounds.Add(new FightResult { Message = "Противник критически промахивается!", IsRoundEnd = false });
                    }
                    else if (enemyAttackRoll + enemy.AttackModifier > player.Ac)
                    {
                        // Обычное попадание противника
                        var damage = RollDamage(enemy.DamageModifier, enemy.Damage);
                        player.HitPoints -= damage;
                        round.Rounds.Add(new FightResult { Message = $"({enemy.AttackModifier} + {enemyAttackRoll})" +
                        $" больше {player.Ac} Противник попадает! Получено урона: {enemy.DamageModifier} + {damage - enemy.DamageModifier} . " +
                        $"У вас осталось {player.HitPoints} HP.", IsRoundEnd = false });
                    }
                    else
                    {
                        // Промах противника
                        round.Rounds.Add(new FightResult { Message = "Противник промахивается!", IsRoundEnd = false });
                    }
                }
            }
            

            rounds.Add(round);

            if (player.HitPoints <= 0 || enemy.HitPoints <= 0)
            {
                // Победа или поражение, добавим финальное сообщение
                round.Rounds.Add(new FightResult { Message = player.HitPoints <= 0 ? "Вы проиграли. Ваш персонаж мертв." : "Вы победили! Противник повержен.", IsRoundEnd = true });
            }
        }

        return rounds;
    }
    private int RollAttack()
    {
        return Dice.DiceTwenty.Roll();
    }
    private int  RollDamage(int damageModifier, string? damage)
    {
        if (string.IsNullOrEmpty(damage))
        {
            return 0;
        }

        var parts = damage.Split('d');
    
        if (parts.Length == 2 && int.TryParse(parts[0], out var numberOfRolls) && int.TryParse(parts[1], out var diceSides))
        {
            var totalDamage = 0;

            for (var i = 0; i < numberOfRolls; i++)
            {
                totalDamage += new Dice(diceSides).Roll();
            }

            return totalDamage + damageModifier;
        }

        return 0;
    }
}