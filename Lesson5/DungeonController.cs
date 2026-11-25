using System.Collections.Generic;

namespace Lesson5
{
    public class DungeonController
    {
        private readonly Dungeon _dungeon;
        private readonly DungeonView _view;

        public DungeonController(Dungeon dungeon, DungeonView view)
        {
            _dungeon = dungeon;
            _view = view;
        }

        public bool StartRun(Player player)
        {
            int round;
            int tries = 0;

            int[] nextRoom = new int[] { 0, 0 };
            int y = 0;
            int x = 0;

            _view.ShowDungeonSize(_dungeon.Rows, _dungeon.Columns);

            while (true)
            {
                bool wasMonsterFought = false;

                Room room = _dungeon.Rooms[y, x];

                _view.ShowRoom(room);

                if (tries > 9)
                    break;

                player.RestoreHP();

                Monster roomMonster;

                if (_dungeon.WasRoomCleared(y, x))
                {
                    roomMonster = new Monster(); // Spawn dead monster
                }
                else
                {
                    roomMonster = room.RoomMonster;
                    roomMonster.TryRestoreHpAfterRound();
                }

                string monsterPrint = (room.Type != "Regular Room") ? "No Monster Here" : roomMonster.ToString();

                _view.ShowMonsterInfo(monsterPrint);
                _view.ShowPlayerInfo(player);

                round = 0;


                if (roomMonster.IsDead)
                {
                    room.OnEnter(player);
                }

                while (roomMonster.Hp > 0)
                {
                    wasMonsterFought = true;

                    _view.ShowRoundHeader(round);
                    _view.ShowMonsterAndPlayerHp(roomMonster, player);

                    wasMonsterFought = true;

                    if (player.Hp <= 0)
                    {
                        tries++;
                        nextRoom = new int[] { 0, 0 };
                        _dungeon.ResetRoomsState();
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
                    bool isCombatRoom = !(room is TrainingRoom) && !(room is TreasureRoom); // Only combat rooms give XP / loot

                    if (isCombatRoom && wasMonsterFought)
                    {
                        _view.ShowMonsterInfo($"XP gained from fight: {roomMonster.XPgain}");
                        player.BoostStats(roomMonster.XPgain);

                        if (roomMonster.Hp <= 0)
                        {
                            LootSystem.Instance.TryGenerateLoot();
                        }
                    }

                    _dungeon.MarkRoomCleared(y, x);

                    if (y == _dungeon.Rows - 1 && x == _dungeon.Columns - 1)
                    {
                        break;
                    }

                    nextRoom = ChooseNextRoom(x, y);
                }

                y = nextRoom[0];
                x = nextRoom[1];
            }

            return tries <= 9;
        }

        private int[] ChooseNextRoom(int x, int y)
        {
            _view.ShowRoomsLeft(
                _dungeon.RoomsLeft,
                _dungeon.RegularLeft,
                _dungeon.TrainingLeft,
                _dungeon.TreasureLeft,
                _dungeon.DoorLeft);

            Dictionary<string, int[]> possibleOptions = ListOptionsForMoving(x, y);

            string choice = _view.AskNextRoom();

            while (!possibleOptions.ContainsKey(choice))
            {
                _view.ShowInvalidDirection();
                possibleOptions = ListOptionsForMoving(x, y);
                choice = _view.AskNextRoom();
            }

            return possibleOptions[choice];
        }

        private Dictionary<string, int[]> ListOptionsForMoving(int x, int y)
        {
            Dictionary<string, int[]> options = new Dictionary<string, int[]>();
            int xLength = _dungeon.Columns;
            int yLength = _dungeon.Rows;

            if (y - 1 >= 0)
            {
                Room target = _dungeon.Rooms[y - 1, x];
                if (target is DoorRoom door && door.IsLocked)
                {
                    _view.ShowLockedDirection("Up", y - 1, x);
                }
                else
                {
                    _view.ShowDirection("Up", y - 1, x);
                    options.Add("up", new int[] { y - 1, x });
                }
            }

            if (y + 1 < yLength)
            {
                Room target = _dungeon.Rooms[y + 1, x];
                if (target is DoorRoom door && door.IsLocked)
                {
                    _view.ShowLockedDirection("Down", y + 1, x);
                }
                else
                {
                    _view.ShowDirection("Down", y + 1, x);
                    options.Add("down", new int[] { y + 1, x });
                }
            }

            if (x - 1 >= 0)
            {
                Room target = _dungeon.Rooms[y, x - 1];
                if (target is DoorRoom door && door.IsLocked)
                {
                    _view.ShowLockedDirection("Left", y, x - 1);
                }
                else
                {
                    _view.ShowDirection("Left", y, x - 1);
                    options.Add("left", new int[] { y, x - 1 });
                }
            }

            if (x + 1 < xLength)
            {
                Room target = _dungeon.Rooms[y, x + 1];
                if (target is DoorRoom door && door.IsLocked)
                {
                    _view.ShowLockedDirection("Right", y, x + 1);
                }
                else
                {
                    _view.ShowDirection("Right", y, x + 1);
                    options.Add("right", new int[] { y, x + 1 });
                }
            }

            return options;
        }
    }
}