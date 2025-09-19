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
            Player player = new Player();
            Dungeon dungeon = new Dungeon();

            bool didWin = dungeon.RunDungeon(player);

            if (didWin)
            {
                Console.WriteLine("The Player Won!!");
                return;
            }

            Console.WriteLine("The Player Won!!");
        }
    }
}
