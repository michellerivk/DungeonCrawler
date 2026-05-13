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
        public override string Type => "Training Room";
        public TrainingRoom(int roomNumber, int x, int y) : base(roomNumber, x, y)
        {
            _powerIncrease = RandomUtils.NumberRandomizer(5,15);
            RoomMonster.KillMonster();
        }

        public override void OnEnter(Player player) 
        {
            player.IncreasePower(_powerIncrease);
            Console.WriteLine($"OMG! Player's power increased by: {_powerIncrease} and is now: {player.Power}");
        }
    }
}
