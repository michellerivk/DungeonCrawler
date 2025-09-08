using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class Room
    {
        public string Name { get; private set; }
        public Monster Monster { get; private set; }

        public Room(int roomNumber, int x, int y)
        {
            Name = $"~ Room {roomNumber}: \t[{y},{x}] ~";
            Monster = new Monster(roomNumber);
        }

        public override string ToString()
        {
            return $"{Name}";
        }

        // Adding line for PR
    }
}
