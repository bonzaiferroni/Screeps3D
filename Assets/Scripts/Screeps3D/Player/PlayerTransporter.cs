using System.Collections.Generic;
using Common;
using Screeps3D.Rooms;
using UnityEngine;

namespace Screeps3D.Player
{
    public class PlayerTransporter : BaseSingleton<PlayerTransporter>
    {
        [SerializeField] private RoomChooser _chooser;
        [SerializeField] private PlayerGaze _playerGaze;

        private Stack<Room> _loadStack = new Stack<Room>();

        private void Start()
        {
            _chooser.OnChooseRoom += OnChoose;
        }

        private void OnChoose(Room room)
        {
            _loadStack.Push(room);
            TransportPlayer(room.position);
            room.Show(true);
        }

        private void TransportPlayer(Vector3 pos)
        {
            _playerGaze.transform.position = new Vector3(pos.x + 25, pos.y, pos.z + 25);
        }
    }
}