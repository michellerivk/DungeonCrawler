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
        private static readonly Random rnd = new Random();
        public int Hp { get; private set; }
        public int Power { get; private set; }
        public int XPgain { get; private set; }

        public Monster(int roomNum) 
        {
            Hp = 100;
            Power = GetPowerValue(roomNum);
            XPgain = GetXPGainValue();
        }

        public Monster()
        {
            Hp = 0;
            Power = 0;
        }

        private int GetPowerValue(int roomNum)
        {
            return rnd.Next(roomNum + 5, roomNum + 15);
        }

        private int GetXPGainValue()
        {
            return rnd.Next(10, 15) * Power;
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
    }
}
