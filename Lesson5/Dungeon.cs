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

        public Dungeon ()
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

        public bool RunDungeon(Player player)
        {
            int round;
            int tries = 0;

            int[] nextRoom = new int[] { 0, 0 };
            int y = 0;
            int x = 0;

            Console.WriteLine($"The size of the room is: " +
                  $"[{_rows}][{_colums}]");

            while (y < _rows - 1 || x < _colums - 1)
            {
                bool wasMonsterFaught = true;

                Room room = Rooms[y, x];

                PrintRoom(x, y);

                if (tries > 9)
                    break;

                player.RestoreHP();

                Monster roomMonster;

                if (_roomsBeat.Contains((y, x)))
                {
                    roomMonster = new Monster(); // Spawn dead monster
                    wasMonsterFaught = false;
                }
                else
                {
                    roomMonster = room.RoomMonster;
                    roomMonster.TryRestoreHpAfterRound();
                }

                string monsterPrint = (room.Type != "Regular Room") ? "No Monster Here" : roomMonster.ToString();

                Console.WriteLine(monsterPrint);
                Console.WriteLine(player);

                round = 0;

                if (roomMonster.IsDead)
                    room.OnEnter(player);

                while (roomMonster.Hp > 0)
                {

                    Console.WriteLine($"\nRound: {round}");

                    Console.WriteLine($"Monster's HP: {roomMonster.Hp}");

                    if (player.Hp >= 0)
                        Console.WriteLine($"Player's HP: {player.Hp}\n");
                    else
                        Console.WriteLine($"Player's HP: 0\n");

                    wasMonsterFaught = true;

                    if (player.Hp <= 0)
                    {
                        tries++;
                        nextRoom = new int[] { 0, 0 };
                        ResetRoomsState();

                        break;
                    }

                    // Player Attacks
                    if (roomMonster.Shields != 0)
                        roomMonster.TryReduceShields();

                    else
                        player.AttackMonster(roomMonster);


                    if (roomMonster.Hp <= 0)
                        break;

                    // Monster attacks
                    if (player.Shields != 0)
                        player.TryReduceShields();

                    else
                        roomMonster.AttackPlayer(player);

                    round++;
                }

                if (player.Hp > 0)
                {
                    if (!wasMonsterFaught)
                    {
                        nextRoom = ChooseNextRoom(x, y, _colums, _rows);
                        y = nextRoom[0];
                        x = nextRoom[1];
                        continue;
                    }

                    Console.WriteLine($"XP gained from fight: {roomMonster.XPgain}");
                    player.BoostStats(roomMonster.XPgain);

                    bool isCombatRoom = !(room is TrainingRoom) && !(room is TreasureRoom); // Only combat rooms drop loot
                    if (isCombatRoom && roomMonster.Hp <= 0)
                    {
                        LootSystem.Instance.TryGenerateLoot();
                    }

                    MarkRoomCleared(y, x);

                    nextRoom = ChooseNextRoom(x, y, _colums, _rows);
                }

                y = nextRoom[0];
                x = nextRoom[1];
            }

            if (tries > 9)
            {
                return false;
            }

            return true;
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

        private void MarkRoomCleared(int y, int x)
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

        private void PrintRoom(int x, int y)
        {
            Console.WriteLine(Rooms[y, x]);
        }

        private int[] ChooseNextRoom(int x, int y, int xLength, int yLength)
        {
            PrintRoomsLeft(); // Print the amount of rooms and each room type that are left

            Console.WriteLine($"\nPlease choose your next room:\n");

            Dictionary<string, int[]> possibleOptions = ListOptionsForMoving(x, y, xLength, yLength);

            string choice = Console.ReadLine().ToLower();

            while (!possibleOptions.ContainsKey(choice))
            {
                Console.WriteLine($"Please enter one of the following options:\n");

                possibleOptions = ListOptionsForMoving(x, y, xLength, yLength);

                choice = Console.ReadLine().ToLower();
            }

            return possibleOptions[choice];

        }

        private Dictionary<string, int[]> ListOptionsForMoving(int x, int y, int xLength, int yLength)
        {
            Dictionary<string, int[]> options = new Dictionary<string, int[]>();

            if (y - 1 >= 0)
            {
                Room target = Rooms[y - 1, x];
                if (target is DoorRoom door && door.IsLocked)
                {
                    Console.WriteLine($"Up -> [{y - 1}, {x}] (LOCKED DOOR)\n");
                }
                else
                {
                    Console.WriteLine($"Up -> [{y - 1}, {x}]\n");
                    options.Add("up", new int[] { y - 1, x });
                }
            }

            if (y + 1 < yLength)
            {
                Room target = Rooms[y + 1, x];
                if (target is DoorRoom door && door.IsLocked)
                {
                    Console.WriteLine($"Down -> [{y + 1}, {x}] (LOCKED DOOR)\n");
                }
                else
                {
                    Console.WriteLine($"Down -> [{y + 1}, {x}]\n");
                    options.Add("down", new int[] { y + 1, x });
                }
            }

            if (x - 1 >= 0)
            {
                Room target = Rooms[y, x - 1];
                if (target is DoorRoom door && door.IsLocked)
                {
                    Console.WriteLine($"Left -> [{y}, {x - 1}] (LOCKED DOOR)\n");
                }
                else
                {
                    Console.WriteLine($"Left -> [{y}, {x - 1}]\n");
                    options.Add("left", new int[] { y, x - 1 });
                }
            }

            if (x + 1 < xLength)
            {
                Room target = Rooms[y, x + 1];
                if (target is DoorRoom door && door.IsLocked)
                {
                    Console.WriteLine($"Right -> [{y}, {x + 1}] (LOCKED DOOR)\n");
                }
                else
                {
                    Console.WriteLine($"Right -> [{y}, {x + 1}]\n");
                    options.Add("right", new int[] { y, x + 1 });
                }
            }

            return options;
        }

        private void PrintRoomsLeft()
        {
            Console.WriteLine($"\nRooms left: {_roomsLeft}");
            Console.WriteLine($"\nRegular:  {_regularLeft}");
            Console.WriteLine($"\nTraining: {_trainingLeft}");
            Console.WriteLine($"\nTreasure: {_treasureLeft}");
            Console.WriteLine($"\nDoor:     {_doorLeft}");
        }

        private void ResetRoomsState()
        {
            _roomsBeat.Clear();

            _roomsLeft = 0;
            _regularLeft = 0;
            _trainingLeft = 0;
            _treasureLeft = 0;

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
    }
}
