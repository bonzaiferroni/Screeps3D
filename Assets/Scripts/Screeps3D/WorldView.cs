using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Screeps3D
{
    public class WorldView : BaseSingleton<WorldView>
    {
        private const int VIEW_DISTANCE = 2;
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
        }

        private void TransportPlayer(Vector3 pos)
        {
            _playerGaze.transform.position = new Vector3(pos.x + 25, pos.y, pos.z + 25);
        }

        public void LoadNeighbors(Room room)
        {
            for (var xDelta = -VIEW_DISTANCE; xDelta <= VIEW_DISTANCE; xDelta++)
            {
                for (var yDelta = -VIEW_DISTANCE; yDelta <= VIEW_DISTANCE; yDelta++)
                {
                    if (xDelta == 0 && yDelta == 0) continue;
                    RoomManager.Instance.GetNeighbor(room, xDelta, yDelta);
                }
            }
        }
    }
}