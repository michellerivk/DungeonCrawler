using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class TreasureRoom : Room
    {
        const int shieldBonus = 1; const int xpBonus = 3;
        const int minShieldBonusAmount = 1; const int minXpBonusAmount = 100;
        const int maxShieldBonusAmount = 5; const int maxXpBonusAmount = 300;


        private readonly int _givenBonus;
        private readonly int _givenAmount;

        public TreasureRoom(int roomNumber, int x, int y) : base(roomNumber, x, y)
        {
            _givenBonus = RandomUtils.NumberRandomizer(shieldBonus, xpBonus);

            _givenAmount = (_givenBonus == 1) ? RandomUtils.NumberRandomizer(minShieldBonusAmount, maxShieldBonusAmount) 
                                              : RandomUtils.NumberRandomizer(minXpBonusAmount, maxXpBonusAmount);
        }
    }
}
