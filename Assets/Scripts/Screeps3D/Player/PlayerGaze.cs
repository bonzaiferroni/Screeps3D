using System.Collections.Generic;
using Screeps3D.Rooms;
using Screeps3D.Rooms.Views;
using Screeps_API;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Screeps3D.Player
{
    public class PlayerGaze : MonoBehaviour
    {
        private const int VIEW_DISTANCE = 2;
        
        private Dictionary<Room, bool> _loadedNeighbors = new Dictionary<Room, bool>();
        private Queue<Room> queue = new Queue<Room>();
        
        private void Update()
        {
            if (!ScreepsAPI.Instance || !ScreepsAPI.Instance.IsConnected)
            {
                return;
            }

            var ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200, 1 << 10))
            {
                var roomView = hit.collider.GetComponent<RoomView>();
                if (roomView == null || roomView.Room.ShowingObjects)
                {
                    return;
                }

                ShowObjects(roomView.Room);
            }
        }

        private void ShowObjects(Room room)
        {
            if (queue.Count >= 2)
            {
                var otherRoom = queue.Dequeue();
                otherRoom.ShowObjects(false);
            }
            queue.Enqueue(room);

            room.ShowObjects(true);
            LoadNeighbors(room);
        }
        
        public void LoadNeighbors(Room room)
        {
            if (_loadedNeighbors.ContainsKey(room))
                return;
            _loadedNeighbors[room] = true;
            
            for (var xDelta = -VIEW_DISTANCE; xDelta <= VIEW_DISTANCE; xDelta++)
            {
                for (var yDelta = -VIEW_DISTANCE; yDelta <= VIEW_DISTANCE; yDelta++)
                {
                    if (xDelta == 0 && yDelta == 0) continue;
                    var neighbor = RoomManager.Instance.GetNeighbor(room, xDelta, yDelta);
                    neighbor.Show(true);
                }
            }
        }
    }
}