using System;
using Common;
using Screeps3D.RoomObjects.Views;
using Screeps3D.Rooms;
using UnityEngine;

namespace Screeps3D.RoomObjects
{
    public class RoomObject: IRoomObject
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string RoomName { get; set; }
        public Room Room { get; protected set; }
        public Vector3 Position { get; protected set; }

        public ObjectView View { get; protected set; }

        public bool Initialized { get; protected set; }
        public bool Shown { get; protected set; }
        
        public Action<bool> OnShow;
        public Action<JSONObject> OnDelta;

        internal virtual void Delta(JSONObject delta, Room room)
        {
            if (!Initialized)
            {
                Unpack(delta, true);
            }
            else
            {
                Unpack(delta, false);
            }
            
            if (Room != room || !Shown)
            {
                EnterRoom(room);
            }

            SetPosition();
            
            if (View != null)
                View.Delta(delta);
            
            if (OnDelta != null)
            {
                OnDelta(delta);
            }
        }

        internal virtual void Unpack(JSONObject data, bool initial)
        {
            if (initial)
            {
                UnpackUtility.Id(this, data);
                UnpackUtility.Type(this, data);
            }
            
            UnpackUtility.Position(this, data);
        }

        protected void EnterRoom(Room room)
        {
            Room = room;
            
            if (View == null)
            {
                Scheduler.Instance.Add(AssignView);
            }

            Shown = true;
            if (OnShow != null)
                OnShow(true);
        }

        private void AssignView()
        {
            if (Shown && View == null)
            {
                View = ObjectViewFactory.Instance.NewView(this);
                if (View)
                    View.Load(this);
            }
        }

        public void HideObject(Room room)
        {
            if (room != Room)
                return;

            Shown = false;
            if (OnShow != null)
                OnShow(false);
        }

        protected void SetPosition()
        {
            Position = PosUtility.Convert(X, Y, Room);
        }

        public void DetachView()
        {
            View = null;
        }
    }
}