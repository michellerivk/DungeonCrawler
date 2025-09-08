using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lesson5
{
    public class Player
    {
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
            IncreaseLevel(xp);

            Power += 3;

            RestoreHP();
            Hp += 4;
        }

        public void IncreaseLevel(int xp)
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

        // Adding line for PR
    }
}
