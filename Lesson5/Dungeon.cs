using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class Dungeon
    {
        private const int minRooms = 1;
        private const int maxRooms = 10;

        private readonly int _rows;
        private readonly int _colums;

        private int _roomsLeft;
        private int _regularLeft;
        private int _trainingLeft;
        private int _treasureLeft;
        private int _doorLeft;

        public readonly Room[,] Rooms;

        private readonly HashSet<(int y, int x)> _roomsBeat = new HashSet<(int y, int x)>();

        // Expose state for the controller / view
        public int Rows => _rows;
        public int Columns => _colums;

        public int RoomsLeft => _roomsLeft;
        public int RegularLeft => _regularLeft;
        public int TrainingLeft => _trainingLeft;
        public int TreasureLeft => _treasureLeft;
        public int DoorLeft => _doorLeft;

        public Dungeon()
        {
            Rooms = new Room[RandomUtils.NumberRandomizer(minRooms, maxRooms),
                             RandomUtils.NumberRandomizer(minRooms, maxRooms)];

            _rows = Rooms.GetLength(0);
            _colums = Rooms.GetLength(1);

            for (int y = 0; y < _rows; y++)
            {
                for (int x = 0; x < _colums; x++)
                {
                    int roomNumber = y * _colums + x;
                    Rooms[y, x] = CreateRoom(roomNumber, x, y);

                    _roomsLeft++;

                    if (Rooms[y, x] is TrainingRoom)
                        _trainingLeft++;
                    else if (Rooms[y, x] is TreasureRoom)
                        _treasureLeft++;
                    else if (Rooms[y, x] is DoorRoom)
                        _doorLeft++;
                    else
                        _regularLeft++;
                }
            }
        }

        public bool WasRoomCleared(int y, int x)
        {
            return _roomsBeat.Contains((y, x));
        }

        public void MarkRoomCleared(int y, int x)
        {
            if (_roomsBeat.Add((y, x)))
            {
                _roomsLeft--;

                Room r = Rooms[y, x];
                if (r is TrainingRoom) _trainingLeft--;
                else if (r is TreasureRoom) _treasureLeft--;
                else if (r is DoorRoom) _doorLeft--;
                else _regularLeft--;
            }
        }

        public void ResetRoomsState()
        {
            _roomsBeat.Clear();

            _roomsLeft = 0;
            _regularLeft = 0;
            _trainingLeft = 0;
            _treasureLeft = 0;
            _doorLeft = 0;

            for (int y = 0; y < _rows; y++)
            {
                for (int x = 0; x < _colums; x++)
                {
                    Room r = Rooms[y, x];
                    _roomsLeft++;

                    if (r is TrainingRoom) _trainingLeft++;
                    else if (r is TreasureRoom) _treasureLeft++;
                    else if (r is DoorRoom) _doorLeft++;
                    else _regularLeft++;
                }
            }
        }

        private Room CreateRoom(int roomNumber, int x, int y)
        {
            // 1-3 = regular, 4 = training, 5 = treasure, 6 = door room
            int chance = RandomUtils.NumberRandomizer(1, 6);

            if (chance <= 3)
                return new Room(roomNumber, x, y);

            if (chance == 4)
                return new TrainingRoom(roomNumber, x, y);

            if (chance == 5)
                return new TreasureRoom(roomNumber, x, y);

            return new DoorRoom(roomNumber, x, y);
        }
    }
}
