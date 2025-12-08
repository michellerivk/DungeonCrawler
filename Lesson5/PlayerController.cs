using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class PlayerController
    {
        private readonly Player _player;
        private readonly PlayerView _view;

        public PlayerController(Player player, PlayerView view)
        {
            _player = player;
            _view = view;

            _player.PlayerMessage += _view.ShowMessage;
        }

        public void ShowStatus()
        {
            _view.ShowPlayer(_player);
        }

        public void Attack(Monster monster)
        {
            _player.AttackMonster(monster);
        }
    }
}
