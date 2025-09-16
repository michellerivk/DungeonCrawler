using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lesson5
{
    public class Player
    {
        const int powerBoost = 3; const int hpBoost = 4;

        public int Hp { get; private set; }
        public int Level { get; private set; }
        public int Power { get; private set; }
        public int CurrentXP { get; private set; }
        public int NeededXPforlevel { get; private set; }

        public Player()
        {
            Hp = 70;
            Level = 0;
            Power = 10;
            CurrentXP = 0;
            NeededXPforlevel = 100;
        }

        public void RestoreHP()
        {
            Hp = 70;
        }

        public void AttackMonster(Monster monster)
        {
            monster.TakeDamage(Power);
        }

        public void TakeDamage(int attack)
        {
            Hp -= attack;
        }

        public void BoostStats(int xp)
        {
            TryIncreaseLevel(xp);

            Power += powerBoost;

            RestoreHP();
            Hp += hpBoost;
        }

        public void TryIncreaseLevel(int xp)
        {
            CurrentXP += xp;

            while (CurrentXP >= NeededXPforlevel)
            {
                CurrentXP -= NeededXPforlevel;
                Level++;
            }
        }

        public override string ToString()
        {
            int hp = Hp;
            if (hp < 0) hp = 0;

            return $"Player: \nHP: {hp} \tPower: {Power} \tLevel: {Level} \t " +
                $"Needed XP for next level: {NeededXPforlevel - CurrentXP}";
        }
    }
}
