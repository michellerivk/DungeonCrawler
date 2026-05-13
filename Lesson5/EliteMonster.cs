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
        public override string Type => "Elite Monster";

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

                if (_resurrections > 1)
                    Console.WriteLine($"The {Type} resurrects! ({_resurrections} resurrections left)");
                if (_resurrections == 1)
                    Console.WriteLine($"The {Type} resurrects! (Last resurrection left)");
                if (_resurrections == 0)
                    Console.WriteLine($"The {Type} resurrects! (No resurrections left)");
            }
            else
            {
                Console.WriteLine($"The {Type} is finally dead!");
            }
        }

        public override string ToString()
        {
            if (Hp == 0)
                return $"Dead Monster";

            return $"Monster : {Type} \nHP: {Hp} \tPower: {Power}\t Number of Resurrections: {_resurrections}";
        }
    }
}
