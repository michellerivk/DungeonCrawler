using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class DungeonView
    {
        public void ShowDungeonSize(int rows, int columns)
        {
            Console.WriteLine($"The size of the dungeon is: [{rows}][{columns}]");
        }

        public void ShowRoom(Room room)
        {
            Console.WriteLine(room);
        }

        public void ShowMonsterInfo(string text)
        {
            Console.WriteLine(text);
        }

        public void ShowPlayerInfo(Player player)
        {
            Console.WriteLine(player);
        }

        public void ShowRoundHeader(int round)
        {
            Console.WriteLine($"\nRound: {round}");
        }

        public void ShowMonsterAndPlayerHp(Monster monster, Player player)
        {
            Console.WriteLine($"Monster's HP: {monster.Hp}");
            int hp = player.Hp >= 0 ? player.Hp : 0;
            Console.WriteLine($"Player's HP: {hp}\n");
        }

        public void ShowRoomsLeft(int roomsLeft, int regularLeft, int trainingLeft, int treasureLeft, int doorLeft)
        {
            Console.WriteLine($"\nRooms left: {roomsLeft}");
            Console.WriteLine($"\nRegular:  {regularLeft}");
            Console.WriteLine($"\nTraining: {trainingLeft}");
            Console.WriteLine($"\nTreasure: {treasureLeft}");
            Console.WriteLine($"\nDoor:     {doorLeft}");
        }

        public void ShowDirection(string name, int y, int x)
        {
            Console.WriteLine($"{name} -> [{y}, {x}]\n");
        }

        public void ShowLockedDirection(string name, int y, int x)
        {
            Console.WriteLine($"{name} -> [{y}, {x}] (LOCKED DOOR)\n");
        }

        public string AskNextRoom()
        {
            Console.WriteLine($"\nPlease choose your next room:\n");
            return Console.ReadLine()?.ToLower();
        }

        public void ShowInvalidDirection()
        {
            Console.WriteLine($"Please enter one of the following options:\n");
        }

        public void ShowWin()
        {
            Console.WriteLine("You Won!!");
        }

        public void ShowLose()
        {
            Console.WriteLine("You Lost :(");
        }

        public void ShowPlayAgainQuestion()
        {
            Console.WriteLine("Play again? (y/n)");
        }

        public string ReadPlayAgainAnswer()
        {
            return Console.ReadLine()?.ToLower();
        }
    }
}
