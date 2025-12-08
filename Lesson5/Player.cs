using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lesson5
{
    public class Player
    {
        const int powerBoost = 3; const int hpBoost = 4;

        public int Hp { get; private set; }
        public int Level { get; private set; }
        public int Power { get; private set; }
        public int CurrentXP { get; private set; }
        public int NeededXPforlevel { get; private set; }
        public int Shields { get; private set; }
        public bool HasKey { get; private set; }
        public int MaxHp { get; private set; } = 100;

        public Player()
        {
            Hp = MaxHp;
            Level = 0;
            Power = 10;
            CurrentXP = 0;
            Shields = 0;
            NeededXPforlevel = 100;
            HasKey = false;
        }

        public void RestoreHP()
        {
            Hp = 100;
        }

        public void AttackMonster(Monster monster)
        {
            monster.TakeDamage(Power);
        }

        public void TakeDamage(int attack)
        {
            Hp -= attack;
        }

        public void BoostStats(int xp)
        {
            TryIncreaseLevel(xp);

            Power += powerBoost;

            IncreaseMaxHealth(hpBoost);
        }

        public void Heal(int amount)
        {
            Hp += amount;
            if (Hp > MaxHp)
                Hp = MaxHp;
        }

        public void IncreaseMaxHealth(int amount)
        {
            MaxHp += amount;
            Hp = MaxHp;
        }

        public void TryIncreaseLevel(int xp)
        {
            bool didLevelUp = false;
            CurrentXP += xp;

            while (CurrentXP >= NeededXPforlevel)
            {
                CurrentXP -= NeededXPforlevel;
                Level++;
                didLevelUp = true;
            }

            if (didLevelUp) 
                Console.WriteLine($"WOHOOO!! The player leveled up and is now level: {Level}");

        }

        public void IncreaseShields(int shieldAmount)
        {
            Shields += shieldAmount;
        }

        public void TryReduceShields()
        {
            if (Shields > 0)
                Shields--;
        }

        public void IncreasePower(int powerIncrease)
        { 
            Power += powerIncrease;
        }

        public override string ToString()
        {
            int hp = Hp;
            if (hp < 0) hp = 0;

            return $"Player: \nHP: {hp} \tPower: {Power} \tLevel: {Level} \tShields: {Shields} \t " +
                $"Needed XP for next level: {NeededXPforlevel - CurrentXP}";
        }
        public void RegisterToLootSystem()
        {
            LootSystem.Instance.LootGranted += OnLootGranted;
        }
        private void OnLootGranted(LootType lootType, int amount)
        {
            switch (lootType)
            {
                case LootType.Shield:
                    IncreaseShields(amount);
                    Console.WriteLine($"Loot: Player received {amount} shields.");
                    break;

                case LootType.Power:
                    IncreasePower(amount);
                    Console.WriteLine($"Loot: Player's power increased by {amount}.");
                    break;

                case LootType.Heal:
                    Heal(amount);
                    Console.WriteLine($"Loot: Player healed {amount} HP. (HP: {Hp}/{MaxHp})");
                    break;

                case LootType.MaxHealthIncrease:
                    IncreaseMaxHealth(amount);
                    Console.WriteLine($"Loot: Player's max HP increased by {amount}. (Max HP: {MaxHp})");
                    break;

                case LootType.Key:
                    if (!HasKey)
                    {
                        HasKey = true;
                        Console.WriteLine("Loot: Player found a key! Some doors might now be unlocked.");
                    }
                    else
                    {
                        Console.WriteLine("Loot: Another key found, but you already have one.");
                    }
                    break;
            }
        }
    }
}
