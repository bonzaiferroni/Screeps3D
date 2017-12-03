using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Screeps3D
{
    public class EntityView : MonoBehaviour
    {
        [SerializeField] private PlainsView _plains;

        public WorldCoord Coord { get; private set; }

        private Dictionary<string, RoomObject> _roomObjects = new Dictionary<string, RoomObject>();
        private Queue<JSONObject> _roomData = new Queue<JSONObject>();
        private List<string> _removeList = new List<string>();
        private string _path;
        private bool _awake;

        private static Queue<EntityView> queue = new Queue<EntityView>();

        public void Load(WorldCoord coord)
        {
            this.Coord = coord;

            if (ScreepsAPI.Instance.Address.hostName.ToLowerInvariant() == "screeps.com")
            {
                _path = string.Format("room:{0}/{1}", coord.shardName, coord.roomName);
            } else
            {
                _path = string.Format("room:{0}", coord.roomName);
            }
        }

        public void Wake()
        {
            if (_awake)
                return;

            if (queue.Count >= 2)
            {
                var otherView = queue.Dequeue();
                otherView.Sleep();
            }
            queue.Enqueue(this);

            Debug.Log("subscribing: " + _path);
            ScreepsAPI.Instance.Socket.Subscribe(_path, OnRoomData);
            _plains.Highlight();
            _awake = true;
        }

        private void Sleep()
        {
            ScreepsAPI.Instance.Socket.Unsub(_path);
            _plains.Dim();
            _awake = false;
        }

        private void OnDestroy()
        {
            if (ScreepsAPI.Instance.Socket != null && Coord != null)
            {
                ScreepsAPI.Instance.Socket.Unsub(_path);
            }
        }

        private void OnRoomData(JSONObject data)
        {
            _roomData.Enqueue(data);
        }

        private void Update()
        {
            if (_roomData.Count == 0)
                return;
            RenderEntities(_roomData.Dequeue());
        }

        private void RenderEntities(JSONObject data)
        {
            UnpackUsers(data);
            var objects = data["objects"];
            foreach (var id in objects.keys)
            {
                var datum = objects[id];

                if (!_roomObjects.ContainsKey(id))
                {
                    _roomObjects[id] = ObjectManager.Instance.GetInstance(id, datum);
                    _roomObjects[id].EnterRoom(this);
                }
            }

            _removeList.Clear();
            foreach (var kvp in _roomObjects)
            {
                var id = kvp.Key;
                var roomObject = kvp.Value;

                var datum = JSONObject.obj; // will generate lots of garbage
                if (objects.HasField(id))
                {
                    datum = objects[id];
                }
                if (datum != null && datum.IsNull)
                {
                    roomObject.LeaveRoom(this);
                    _removeList.Add(id);
                } else
                {
                    roomObject.Delta(datum);
                }
            }

            foreach (var id in _removeList)
            {
                _roomObjects.Remove(id);
            }
        }

        private void UnpackUsers(JSONObject data)
        {
            var usersData = data["users"];
            if (usersData == null)
            {
                return;
            }

            foreach (var id in usersData.keys)
            {
                var userData = usersData[id];
                ScreepsAPI.Instance.UserManager.CacheUser(userData);
            }
        }
    }
}