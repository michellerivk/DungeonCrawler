using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class PlayerView
    {
        public void ShowPlayer(Player player)
        {
            Console.WriteLine(player);
        }

        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
