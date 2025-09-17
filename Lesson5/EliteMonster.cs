using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class EliteMonster : Monster
    {
        private readonly int _resurrections;

        public EliteMonster(int PowerMagnitute) : base(PowerMagnitute)
        {
            _resurrections = RandomUtils.OneToFiveNumberRandomizer();
        }

        public override string ReturnMonsterType()
        {
            return $"Elite Monster ♔";
        }
    }
}
