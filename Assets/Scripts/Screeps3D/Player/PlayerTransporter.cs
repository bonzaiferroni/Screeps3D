using System.Collections.Generic;
using Common;
using Screeps3D.Rooms;
using UnityEngine;

namespace Screeps3D.Player
{
    public class PlayerTransporter : BaseSingleton<PlayerTransporter>
    {
        [SerializeField] private RoomChooser _chooser;

        private Stack<Room> _loadStack = new Stack<Room>();

        private void Start()
        {
            _chooser.OnChooseRoom += OnChoose;
        }

        private void OnChoose(Room room)
        {
            _loadStack.Push(room);
            TransportPlayer(room.Position);
            room.Show(true);
        }

        private void TransportPlayer(Vector3 pos)
        {
            CameraRig.Position = pos + new Vector3(25, 0, 25);
        }
    }
}