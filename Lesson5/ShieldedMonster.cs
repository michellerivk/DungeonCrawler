using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class ShieldedMonster : Monster
    {
        public new readonly string Type = "Shielded Monster";

        public ShieldedMonster(int PowerMagnitute) : base(PowerMagnitute)
        {
            Shields += RandomUtils.OneToFiveNumberRandomizer();
        }
    }
}
