using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class Monster
    {
        public int Hp { get; protected set; }
        public int Power { get; protected set; }
        public int XPgain { get; protected set; }

        public Monster(int PowerMagnitute) 
        {
            Hp = 100;
            Power = RandomUtils.NumberRandomizer(PowerMagnitute + 5, PowerMagnitute + 15);
            XPgain = RandomUtils.NumberRandomizer(10, 15) * Power;
        }

        public Monster()
        {
            Hp = 0;
            Power = 0;
        }

        public void AttackPlayer(Player player)
        {
            player.TakeDamage(Power);
        }

        public void TakeDamage(int attack)
        {
            Hp -= attack;
        }

        public void RestoreHpAfterRound()
        {
            Hp = 100;
        }

        public override string ToString()
        {
            return $"Monster : \nHP: {Hp} \tPower: {Power}";
        }

        public virtual string ReturnMonsterType()
        {
            return $"Regular Monster";
        }
    }
}
