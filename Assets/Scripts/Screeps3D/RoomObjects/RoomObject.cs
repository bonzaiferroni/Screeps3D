using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Screeps3D
{
    public class RoomObject
    {
        public JSONObject Data { get; private set; }
        public string Id { get; set; }
        public string Type { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public string RoomName { get; set; }
        public Room Room { get; protected set; }

        public ObjectView View { get; protected set; }

        public Action<ObjectView> OnShow;
        public Action<JSONObject> OnDelta;

        internal void Init(JSONObject data)
        {
            Data = data;
            Unpack(data, true);
        }

        internal void Delta(JSONObject delta)
        {
            Unpack(delta, false);
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
                UnpackUtility.Position(this, data);
            }
        }

        public virtual void EnterRoom(EntityView entityView)
        {
            if (View != null)
            {
                View.Hide();
            }

            View = ObjectViewer.Instance.NewView(this);
            if (View != null)
            {
                View.Init(this);
                View.transform.SetParent(entityView.transform, false);
                View.Show();
                if (OnShow != null) OnShow(View);
            }
            
            Room = entityView.Room;
        }

        public virtual void LeaveRoom(EntityView entityView)
        {
            if (View == null)
                return;

            View.Hide();
            View = null;
        }
    }
}