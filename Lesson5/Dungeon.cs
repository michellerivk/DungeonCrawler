using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class Dungeon
    {
        const int minRooms = 1;
        const int maxRooms = 10;

        public readonly Room[,] DungeonObject;

        public Dungeon ()
        {
            DungeonObject = new Room[RandomUtils.NumberRandomizer(minRooms, maxRooms),
                                RandomUtils.NumberRandomizer(minRooms, maxRooms)];
        }

        public int GetRows()
        {
            return DungeonObject.GetLength(0);
        }

        public int GetColumns()
        {
            return DungeonObject.GetLength (1);
        }

        public void AddRoom(int roomNumber, int x, int y)
        {
            int chance = RandomUtils.OneToFiveNumberRandomizer();

            // 20% to get a regular room, 20% to get a training room, 10% to get a treasure room
            DungeonObject[y, x] = (chance <= 2) ? new Room(roomNumber, x, y)
                           : (chance == 3 || chance == 4) ? (Room)new TrainingRoom(roomNumber, x, y)
                           : (Room)new TreasureRoom(roomNumber, x, y);
        }

        public void PrintRoom(int x, int y)
        {
            Console.WriteLine(DungeonObject[y, x]);
        }
    }
}
