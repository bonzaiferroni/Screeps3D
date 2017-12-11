using System.Collections;
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
        private const int SUBSCRIBE_LIMIT = 2;
        private const float MAP_DISTANCE = 100;
        
        private Dictionary<Room, bool> _loadedNeighbors = new Dictionary<Room, bool>();
        private Queue<Room> queue = new Queue<Room>();
        private List<Room> _mapRooms = new List<Room>();
        private double _nextMap;

        private void Update()
        {
            DisplayObjects();
            DisplayMap();
        }

        private void DisplayMap()
        {
            if (_nextMap > Time.time)
                return;
            _nextMap = Time.time + 1;

            DisableOutsideRange();
            EnableInRange();
        }

        private void DisableOutsideRange()
        {
            for (var i = 0; i < _mapRooms.Count; i++)
            {
                var room = _mapRooms[i];
                if (Vector3.Distance(transform.position, room.Position) <= MAP_DISTANCE)
                    continue;

                room.ShowMap(false);
                _mapRooms.RemoveAt(i);
                i--;
            }
        }

        private void EnableInRange()
        {
            foreach (var collider in Physics.OverlapSphere(transform.position, MAP_DISTANCE, 1 << 10))
            {
                var roomView = collider.GetComponent<RoomView>();
                if (!roomView || Vector3.Distance(transform.position, roomView.Room.Position) > MAP_DISTANCE)
                    continue;
                if (_mapRooms.Contains(roomView.Room))
                    continue;
                
                roomView.Room.ShowMap(true);
                _mapRooms.Add(roomView.Room);
            } 
        }

        private void DisplayObjects()
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
            if (queue.Count >= SUBSCRIBE_LIMIT)
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