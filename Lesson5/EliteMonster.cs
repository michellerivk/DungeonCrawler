using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class EliteMonster : Monster
    {
        private int _resurrections;

        public EliteMonster(int PowerMagnitute) : base(PowerMagnitute)
        {
            _resurrections = RandomUtils.DefaultNumberRandomizer();
        }
    }
}
