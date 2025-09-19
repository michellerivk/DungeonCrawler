using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class Room
    {
        const int eliteMonster = 1; const int rageMonster = 2;

        public string Name { get; private set; }
        public Monster RoomMonster { get; private set; }
        public virtual string Type => "Regular Room";

        public Room(int roomNumber, int x, int y)
        {
            Name = $"~ Room {roomNumber}: \t[{y},{x}] ~";

            var monsterType = RandomUtils.NumberRandomizer(1, 4);

            RoomMonster = (monsterType == eliteMonster) ? (Monster)new EliteMonster(roomNumber) 
                    : (monsterType == rageMonster) ? (Monster)new RageMonster(roomNumber)
                    : (Monster)new ShieldedMonster(roomNumber);
        }

        public virtual void OnEnter(Player player) { }

        public override string ToString()
        {
            return $"{Name}: {Type}";
        }
    }
}
