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
        public new readonly string Type = "Elite Monster";

        public EliteMonster(int PowerMagnitute) : base(PowerMagnitute)
        {
            _resurrections = RandomUtils.OneToFiveNumberRandomizer();
        }

        public override void AfterDeath()
        {
            if (_resurrections > 0)
            {
                Hp = 100;
                _resurrections--;

                Console.WriteLine($"The {Type} resurrects! ({_resurrections} resurrections left)");
            }
            else
            {
                Console.WriteLine($"The {Type} is finally dead! (No resurrections left)");
            }
        }
    }
}
