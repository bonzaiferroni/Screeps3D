using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

namespace Screeps3D
{
    public class RoomManager : BaseSingleton<RoomManager>
    {
        [SerializeField] private GameObject _roomPrefab;
        
        private RoomFactory _factory = new RoomFactory();
        private Dictionary<string, Room> _rooms = new Dictionary<string,Room>();
        private Stack<RoomView> _preloadStack = new Stack<RoomView>();

        private void Start()
        {
            AddPreloads(30);
            Preload();
        }

        public Room Get(string roomName, string shardName)
        {
            var key = roomName + shardName;
            if (_rooms.ContainsKey(key))
            {
                return _rooms[key];
            }

            var room = _factory.Generate(roomName, shardName);
            if (room != null)
            {
                var view = GenerateView(room);
                room.Init(view);
            }
            
            _rooms[key] = room;
            return room;
        }

        public Room GetNeighbor(Room origin, int xDelta, int yDelta)
        {
            var xDir = origin.xDir;
            var yDir = origin.yDir;
            var xCoord = origin.xCoord;
            var yCoord = origin.yCoord;

            if (origin.xDir == "E")
            {
                xCoord += xDelta;
            } else
            {
                xCoord -= xDelta;
            }
            if (xCoord < 0)
            {
                xDir = origin.xDir == "E" ? "W" : "E";
                xCoord = -xCoord - 1;
            }

            if (origin.yDir == "N")
            {
                yCoord += yDelta;
            } else
            {
                yCoord -= yDelta;
            }
            if (yCoord < 0)
            {
                yDir = origin.yDir == "N" ? "S" : "N";
                yCoord = -yCoord - 1;
            }

            var roomName = string.Format("{0}{1}{2}{3}", xDir, xCoord, yDir, yCoord);
            var shardName = string.Format("shard{0}", origin.shardNumber);
            
            return Get(roomName, shardName);
        }

        private RoomView GenerateView(Room room)
        {
            if (_preloadStack.Count == 0)
            {
                AddPreloads(1);
            }

            var view = _preloadStack.Pop();
            view.Show();
            view.gameObject.name = room.name;
            view.transform.localPosition = room.position;
            view.Init(room);
            return view;
        }

        private void AddPreloads(int count)
        {
            for (var i = 0; i < count; i++)
            {
                var go = Instantiate(_roomPrefab);
                var view = go.GetComponent<RoomView>();
                view.transform.SetParent(transform);
                _preloadStack.Push(view);
            }
        }

        private void Preload()
        {
            if (_preloadStack.Count < 30)
            {
                AddPreloads(1);
                Scheduler.Instance.Add(Preload);
            } else
            {
                Scheduler.Instance.Delay(Preload, 10);
            }
        }
    }
}