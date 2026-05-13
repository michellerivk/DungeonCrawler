using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public enum LootType
    {
        Shield,
        Power,
        Heal,
        MaxHealthIncrease,
        Key
    }

    public class LootSystem
    {

        private static LootSystem _instance;

        private LootSystem() { }

        public static LootSystem Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new LootSystem();
                }
                return _instance;
            }
        }

        public event Action<LootType, int> LootGranted;

        public void TryGenerateLoot() // Called after a monster dies
        {
            int dropChance = RandomUtils.NumberRandomizer(1, 100);
            if (dropChance > 40) // 40% chance to drop loot
            {
                return;
            }

            // Random loot type
            int lootIndex = RandomUtils.NumberRandomizer(0, Enum.GetValues(typeof(LootType)).Length - 1);
            LootType lootType = (LootType)lootIndex;

            int amount = 0;

            switch (lootType)
            {
                case LootType.Shield:
                    amount = RandomUtils.NumberRandomizer(1, 3);
                    break;
                case LootType.Power:
                    amount = RandomUtils.NumberRandomizer(1, 5);
                    break;
                case LootType.Heal:
                    amount = RandomUtils.NumberRandomizer(15, 35);
                    break;
                case LootType.MaxHealthIncrease:
                    amount = RandomUtils.NumberRandomizer(5, 15);
                    break;
                case LootType.Key:
                    amount = 1;
                    break;
            }

            Console.WriteLine($"\nLoot dropped: {lootType} (+{amount})");

            LootGranted?.Invoke(lootType, amount);
        }
    }
}
