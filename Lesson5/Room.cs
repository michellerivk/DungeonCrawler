using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class Room
    {
        private enum MonsterKind { eliteMonster, rageMonster, shieldedMonster }

        public string Name { get; private set; }
        public Monster RoomMonster { get; private set; }
        public virtual string Type => "Regular Room";

        public Room(int roomNumber, int x, int y)
        {
            Name = $"~ Room {roomNumber}: \t[{y},{x}] ~";

            var monsterType = RandomUtils.NumberRandomizer(0, 4);

            RoomMonster = (monsterType == (int)MonsterKind.eliteMonster) ? (Monster)new EliteMonster(roomNumber) 
                        : (monsterType == (int)MonsterKind.rageMonster) ? (Monster)new RageMonster(roomNumber)
                        : (monsterType == (int)MonsterKind.shieldedMonster) ? (Monster)new ShieldedMonster(roomNumber)
                        : new Monster(roomNumber);
        }

        public virtual void OnEnter(Player player) { }

        public override string ToString()
        {
            return $"{Name}: {Type}";
        }

        // Adding line for PR
    }
}
