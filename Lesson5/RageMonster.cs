using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class RageMonster : Monster
    {
        public RageMonster(int PowerMagnitute) : base(PowerMagnitute)
        {
            Power += RandomUtils.DefaultNumberRandomizer();
        }
    }
}
