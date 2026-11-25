using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lesson5
{
    public class DoorRoom : Room
    {
        public override string Type => IsLocked ? "Locked Door Room" : "Door Room (Unlocked)";
        public bool IsLocked { get; private set; } = true;

        public DoorRoom(int roomNumber, int x, int y) : base(roomNumber, x, y)
        {
            // No monster in door rooms
            RoomMonster.KillMonster();

            // Subscribe to loot events
            LootSystem.Instance.LootGranted += OnLootGranted;
        }

        private void OnLootGranted(LootType lootType, int amount)
        {
            if (lootType == LootType.Key && IsLocked)
            {
                IsLocked = false;
                Console.WriteLine($"{Name} has been unlocked by a mysterious key!");
            }
        }

        public override void OnEnter(Player player)
        {
            if (IsLocked)
            {
                Console.WriteLine("You somehow entered a locked door room. The door is still shut!");
                return;
            }

            Console.WriteLine("You pass through the unlocked door.");
        }
    }
}
