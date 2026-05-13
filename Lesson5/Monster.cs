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
        public virtual string Type => "Regular Monster";
        public int Shields { get; protected set; }
        public bool IsDead { get; protected set; }

        public Monster(int PowerMagnitute) 
        {
            Hp = 100;
            Power = RandomUtils.NumberRandomizer(PowerMagnitute + 5, PowerMagnitute + 15);
            XPgain = RandomUtils.NumberRandomizer(10, 15) * Power;
            Shields = 0;
            IsDead = false;
        }

        public Monster()
        {
            Hp = 0;
            Power = 0;
        }

        public virtual void AttackPlayer(Player player)
        {
            player.TakeDamage(Power);
        }

        public void TakeDamage(int attack)
        {
            Hp -= attack;
            if (Hp <= 0)
                AfterDeath();
        }

        public void TryRestoreHpAfterRound()
        {
            if (!IsDead)
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

        public void KillMonster()
        {
            Hp = 0;
            Power = 0;
            XPgain = 0;
            IsDead = true;
        }

        // Adding line for PR
    }
}
