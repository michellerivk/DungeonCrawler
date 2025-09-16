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
            const int startRandNum = 1;
            const int endRandNum = 10;

            Player player = new Player();
            int tries = 0;

            Room[,] dungeon = new Room[RandomUtils.NumberRandomizer(startRandNum, endRandNum), 
                                       RandomUtils.NumberRandomizer(startRandNum, endRandNum)];
            int roomNumber;

            Console.WriteLine($"The size of the room is: " +
                              $"[{dungeon.GetLength(0) - 1}][{dungeon.GetLength(1) - 1}]");

            var roomsBeat= new HashSet<(int y, int x)>();

            int[] nextRoom = new int[] { 0, 0 };
            int y = 0;
            int x = 0;

            while (y < dungeon.GetLength(0) - 1 || x < dungeon.GetLength(1) - 1)
            {
                    bool wasMonsterFaught = true;
                    roomNumber = y * 2 + (x + y + 1);
                    dungeon[y, x] = new Room(roomNumber, x, y);

                    Console.WriteLine(dungeon[y, x]);

                    if (tries > 9)
                        break;

                    player.RestoreHP();

                    Monster roomMonster;

                    if (roomsBeat.Contains((y,x)))
                    {
                        roomMonster = new Monster(); // Spawn dead monster
                        wasMonsterFaught = false;
                    }
                    else
                    {
                        roomMonster = new Monster(roomNumber);
                        roomMonster.RestoreHpAfterRound();
                    }

                    Console.WriteLine(roomMonster);
                    Console.WriteLine(player);

                    while (roomMonster.Hp > 0)
                    {

                        Console.WriteLine("Monster's HP: " + roomMonster.Hp);
                        Console.WriteLine("Player's HP: " + player.Hp);

                        wasMonsterFaught = true;

                        if (player.Hp <= 0)
                        {
                            tries++;
                            nextRoom = new int[] { 0, 0 };
                            roomsBeat.Clear();
                            
                            break;
                        }

                        player.AttackMonster(roomMonster);

                        if (roomMonster.Hp <= 0)
                            break;

                        roomMonster.AttackPlayer(player);
                    }

                    if (player.Hp > 0)
                    {
                        if (!wasMonsterFaught)
                        {
                            nextRoom = ChooseNextRoom(x, y, dungeon.GetLength(1), dungeon.GetLength(0));
                            y = nextRoom[0];
                            x = nextRoom[1];
                            continue;
                        }

                        Console.WriteLine($"XP gained from fight: {roomMonster.XPgain}");
                        player.BoostStats(roomMonster.XPgain);
                        roomsBeat.Add((y,x));
                        nextRoom = ChooseNextRoom(x,y, dungeon.GetLength(1), dungeon.GetLength(0));
                    }

                    y = nextRoom[0];
                    x = nextRoom[1];
                }

                if (tries > 9)
                {
                    Console.WriteLine("The Player Lost 10 times :(");
                    return;
                }
            

            Console.WriteLine("The Player Won!!");
        }

        public static int[] ChooseNextRoom(int x, int y, int xLength, int yLength)
        {          
            Dictionary<string, int[]> possibleOptions = new Dictionary<string, int[]>();

            Console.WriteLine($"Please choose your next room:\n");

            ListOptionsForMoving(x, y, xLength, yLength, possibleOptions);

            string choice = Console.ReadLine().ToLower();

            while (!possibleOptions.ContainsKey(choice))
            {
                Console.WriteLine($"Please enter one of the following options:\n");

                ListOptionsForMoving(x, y, xLength, yLength, possibleOptions);

                choice = Console.ReadLine().ToLower();
            }

            return possibleOptions[choice];

        }

        public static void ListOptionsForMoving(int x, int y, int xLength, int yLength, 
                                                Dictionary<string, int[]> options)
        {
            options.Clear();
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
        } 
    }
}
