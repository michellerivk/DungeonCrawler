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
        public readonly string Type = "Regular Monster";
        public int Shields { get; protected set; }

        public Monster(int PowerMagnitute) 
        {
            Hp = 100;
            Power = RandomUtils.NumberRandomizer(PowerMagnitute + 5, PowerMagnitute + 15);
            XPgain = RandomUtils.NumberRandomizer(10, 15) * Power;
            Shields = 0;
        }

        public Monster()
        {
            Hp = 0;
            Power = 0;
        }

        public virtual void AttackPlayer(Player player)
        {
            player.TakeDamage(Power);
            if (Hp <= 0)
                AfterDeath();
        }

        public void TakeDamage(int attack)
        {
            Hp -= attack;
        }

        public void RestoreHpAfterRound()
        {
            Hp = 100;
        }

        public void TryReduceShields()
        {
            if (Shields > 0)
                Shields--;
        }

        public virtual void AfterDeath() { }

        public override string ToString()
        {
            if (Hp == 0)
                return $"Dead Monster";

            return $"Monster : {Type} \nHP: {Hp} \tPower: {Power}";
        }
    }
}
