using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class GameController
    {
        public void StartEventLoop()
        {
            while (true)
            {
                Player player = new Player();
                player.RegisterToLootSystem();

                // Player MVC
                PlayerView playerView = new PlayerView();
                PlayerController playerController = new PlayerController(player, playerView);

                // Dungeon MVC
                Dungeon dungeon = new Dungeon();
                DungeonView dungeonView = new DungeonView();
                DungeonController dungeonController = new DungeonController(dungeon, dungeonView);

                bool didWin = dungeonController.StartRun(player);

                if (didWin)
                    dungeonView.ShowWin();
                else
                    dungeonView.ShowLose();

                dungeonView.ShowPlayAgainQuestion();
                string answer = dungeonView.ReadPlayAgainAnswer();

                if (answer != "y" && answer != "yes")
                    break;
            }
        }
    }
}
