using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class TreasureRoom : Room
    {
        private const int minShieldBonusAmount = 1; private const int minXpBonusAmount = 100;
        private const int maxShieldBonusAmount = 5; private const int maxXpBonusAmount = 300;

        private enum BonusName { Shield, XP }
        public override string Type => "Treasure Room";
        private readonly int _givenBonus;
        private readonly int _givenAmount;

        public TreasureRoom(int roomNumber, int x, int y) : base(roomNumber, x, y)
        {
            _givenBonus = (int)((RandomUtils.NumberRandomizer(0, 1) == 0) ? BonusName.Shield : BonusName.XP);

            _givenAmount = (_givenBonus == 0) ? RandomUtils.NumberRandomizer(minShieldBonusAmount, maxShieldBonusAmount) 
                                              : RandomUtils.NumberRandomizer(minXpBonusAmount, maxXpBonusAmount);

            RoomMonster.KillMonster();
        }

        public override void OnEnter(Player player) 
        {
            if(_givenBonus == 0) // Player gets shields
            {
                player.IncreaseShields(_givenAmount);
                Console.WriteLine($"WOOHOO!! The player gained {_givenAmount} shields");
            }
            else // Player gets XP
            {
                player.TryIncreaseLevel(_givenAmount);
                Console.WriteLine($"WOOHOO!! The player gained {_givenAmount} XP");
            }
        }
    }
}
