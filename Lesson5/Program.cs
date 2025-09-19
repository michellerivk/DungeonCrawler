    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Lesson5
    {
    public class Program
    {
        static void Main(string[] args)
        {
            int round;

            Player player = new Player();
            int tries = 0;

            Dungeon dungeon = new Dungeon();
            int roomNumber;

            Console.WriteLine($"The size of the room is: " +
                              $"[{dungeon.GetRows() - 1}][{dungeon.GetColumns() - 1}]");

            var roomsBeat= new HashSet<(int y, int x)>();

            int[] nextRoom = new int[] { 0, 0 };
            int y = 0;
            int x = 0;

            while (y < dungeon.GetRows() - 1 || x < dungeon.GetColumns() - 1)
            {
                bool wasMonsterFaught = true;

                roomNumber = y * 2 + (x + y + 1);

                dungeon.AddRoom(roomNumber, x, y);

                Room room = dungeon.DungeonObject[y, x];

                dungeon.PrintRoom(x, y);

                if (tries > 9)
                    break;

                player.RestoreHP();

                Monster roomMonster;

                if (roomsBeat.Contains((y, x)))
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
                    Console.WriteLine($"Player's HP: {player.Hp}\n");

                    wasMonsterFaught = true;

                    if (player.Hp <= 0)
                    {
                        tries++;
                        nextRoom = new int[] { 0, 0 };
                        roomsBeat.Clear();
                            
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
                        nextRoom = ChooseNextRoom(x, y, dungeon.GetColumns(), dungeon.GetRows());
                        y = nextRoom[0];
                        x = nextRoom[1];
                        continue;
                    }

                    Console.WriteLine($"XP gained from fight: {roomMonster.XPgain}");
                    player.BoostStats(roomMonster.XPgain);
                    roomsBeat.Add((y,x));
                    nextRoom = ChooseNextRoom(x,y, dungeon.GetColumns(), dungeon.GetRows());
                }

                y = nextRoom[0];
                x = nextRoom[1];
            }

            if (tries > 9)
            {
                Console.WriteLine("The Player Lost :(");
                return;
            }

            Console.WriteLine("The Player Won!!");
        }

        public static int[] ChooseNextRoom(int x, int y, int xLength, int yLength)
        {          

            Console.WriteLine($"Please choose your next room:\n");

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

        public static Dictionary<string, int[]> ListOptionsForMoving(int x, int y, int xLength, int yLength)
        {
            Dictionary<string, int[]>  options = new Dictionary<string, int[]>();
            if (y - 1 >= 0)
            {
                Console.WriteLine($"Up -> [{y - 1}, {x}]\n");
                options.Add("up", new int[] { y - 1, x });
            }
            if (y + 1 < yLength)
            {
                Console.WriteLine($"Down -> [{y + 1}, {x}]\n");
                options.Add("down", new int[] { y + 1, x  });
            }
            if (x - 1 >= 0)
            {
                Console.WriteLine($"Left -> [{y}, {x - 1}]\n");
                options.Add("left", new int[] { y, x - 1 });
            }
            if (x + 1 < xLength)
            {
                Console.WriteLine($"Right -> [{y}, {x + 1}]\n");
                options.Add("right", new int[] {y, x + 1 });
            }

            return options;
        } 
    }
}
