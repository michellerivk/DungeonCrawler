using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class RageMonster : Monster
    {
        public new readonly string Type = "Rage Monster";
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

    }
}
