using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class MonsterController
    {
        private readonly Monster _monster;
        private readonly MonsterView _view;

        public MonsterController(Monster monster, MonsterView view)
        {
            _monster = monster;
            _view = view;
        }

        public void ShowStatus()
        {
            _view.ShowMonster(_monster);
        }

        public void AttackPlayer(Player player)
        {
            _monster.AttackPlayer(player);
        }

        public void TakeDamage(int amount)
        {
            _monster.TakeDamage(amount);
        }

        public Monster Model => _monster;
    }
}
