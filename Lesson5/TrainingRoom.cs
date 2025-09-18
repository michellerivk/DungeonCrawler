using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class TrainingRoom : Room
    {
        private readonly int _powerIncrease;
        public new readonly string Type = "Training Room";
        public TrainingRoom(int roomNumber, int x, int y) : base(roomNumber, x, y)
        {
            _powerIncrease = RandomUtils.NumberRandomizer(50,70);
        }

        public override void OnEnter(Player player) 
        {
            player.IncreasePower(_powerIncrease);
        }
    }
}
