using System;
using Screeps_API;

namespace Screeps3D.Rooms
{
    public class RoomObjectStream
    {
        private Room _room;
        private string _path;
        private bool _subscribed;

        public Action<JSONObject> OnData;

        public void Init(Room room)
        {
            _room = room;
            
            if (ScreepsAPI.Instance.Address.hostName.ToLowerInvariant() == "screeps.com")
            {
                _path = string.Format("room:{0}/{1}", room.shardName, room.roomName);
            } else
            {
                _path = string.Format("room:{0}", room.roomName);
            }
            room.OnShowObjects += ManageSubscription;
        }

        private void ManageSubscription(bool showObjects)
        {
            _subscribed = showObjects;
            if (showObjects)
            {
                Subscribe();
            } else
            {
                Unsubscribe();
            }
        }

        private void Subscribe()
        {
            ScreepsAPI.Instance.Socket.Subscribe(_path, ReceiveData);
        }

        private void Unsubscribe()
        {
            ScreepsAPI.Instance.Socket.Unsub(_path);
        }

        private void ReceiveData(JSONObject Data)
        {
            if (OnData != null)
                OnData(Data);
        }
    }
}