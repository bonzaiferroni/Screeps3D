using System;
using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D
{
    public class RoomUnpacker
    {
        private Room _room;
        private List<string> _removeList = new List<string>();

        public Action<JSONObject> OnUnpack;

        public void Init(Room room)
        {
            _room = room;
            _room.ObjectStream.OnData += Unpack;
            _room.OnShowObjects += ControlVisibility;
        }

        private void Unpack(JSONObject roomData)
        {
            UnpackUsers(roomData);
            var objectsData = roomData["objects"];

            // add new objects
            foreach (var id in objectsData.keys)
            {
                var objectData = objectsData[id];
                if (objectData.IsNull)
                    continue;
                
                if (!_room.Objects.ContainsKey(id))
                {
                    var roomObject = ObjectManager.Instance.GetInstance(id, objectData);
                    _room.Objects[id] = roomObject;
                }
            }

            // process existing object deltas
            _removeList.Clear();
            foreach (var kvp in _room.Objects)
            {
                var id = kvp.Key;
                var roomObject = kvp.Value;

                JSONObject objectData;
                if (objectsData.HasField(id))
                {
                    objectData = objectsData[id];
                }
                else if (roomObject.Room != _room)
                {
                    _removeList.Add(id);
                    continue;
                }
                else
                {
                    objectData = JSONObject.obj;
                }

                if (objectData.IsNull)
                {
                    roomObject.HideObject(_room);
                    _removeList.Add(id);
                }
                else
                {
                    roomObject.Delta(objectData, _room);
                }
            }

            foreach (var id in _removeList)
            {
                _room.Objects.Remove(id);
            }

            if (OnUnpack != null)
                OnUnpack(roomData);
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

        private void ControlVisibility(bool showObjects)
        {
            if (!showObjects)
            {
                foreach (var kvp in _room.Objects)
                {
                    var roomObject = kvp.Value;
                    roomObject.HideObject(_room);
                }
            }
        }
    }
}