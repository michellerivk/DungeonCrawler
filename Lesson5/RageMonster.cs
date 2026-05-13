using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class RageMonster : Monster
    {
        public override string Type => "Rage Monster";
        private readonly int _increasePower;

        public RageMonster(int increasePower) : base(increasePower)
        {
            _increasePower = RandomUtils.OneToFiveNumberRandomizer();
        }

        public override void AttackPlayer(Player player)
        {
            player.TakeDamage(Power);
            Power += _increasePower;
        }

        public override string ToString()
        {
            if (Hp == 0)
                return $"Dead Monster";

            return $"Monster : {Type} \nHP: {Hp} \tPower: {Power}\t Power Increase: {_increasePower}";
        }
    }
}
