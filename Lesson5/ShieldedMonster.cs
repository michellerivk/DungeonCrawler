using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class ShieldedMonster : Monster
    {
        public override string Type => "Shielded Monster";

        public ShieldedMonster(int PowerMagnitute) : base(PowerMagnitute)
        {
            Shields += RandomUtils.OneToFiveNumberRandomizer();
        }

        public override string ToString()
        {
            if (Hp == 0)
                return $"Dead Monster";

            return $"Monster : {Type} \nHP: {Hp} \tPower: {Power}\t Shields: {Shields}";
        }
    }
}
