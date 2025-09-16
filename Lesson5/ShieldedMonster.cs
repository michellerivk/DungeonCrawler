using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class ShieldedMonster : Monster
    {
        private int _shieldAmount;
    
        public ShieldedMonster(int PowerMagnitute) : base(PowerMagnitute)
        {
            _shieldAmount = RandomUtils.DefaultNumberRandomizer();
        }
    }
}
