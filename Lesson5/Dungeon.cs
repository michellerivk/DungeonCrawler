using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class Dungeon
    {
        const int startRandNum = 1;
        const int endRandNum = 10;

        private readonly Room[,] _dungeon;

        public Dungeon ()
        {
            _dungeon = new Room[RandomUtils.NumberRandomizer(startRandNum, endRandNum),
                                RandomUtils.NumberRandomizer(startRandNum, endRandNum)];
        }

        public int GetRows()
        {
            return _dungeon.GetLength(0);
        }

        public int GetColumns()
        {
            return _dungeon.GetLength (1);
        }

        public void AddRoom(int roomNumber, int x, int y)
        {
            _dungeon[y, x] = new Room(roomNumber, x, y);
        }

        public void PrintRoom(int x, int y)
        {
            Console.WriteLine(_dungeon[y, x]);
        }
    }
}
