using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson5
{
    public class RoomController
    {
        private readonly Room _room;
        private readonly RoomView _view;

        public RoomController(Room room, RoomView view)
        {
            _room = room;
            _view = view;
        }

        public void ShowInfo()
        {
            _view.ShowRoom(_room);
        }

        public void Enter(Player player)
        {
            _room.OnEnter(player);
        }

        public Room Model => _room;
    }
}
