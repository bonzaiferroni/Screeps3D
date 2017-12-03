using System;
using System.Collections.Generic;

namespace Screeps3D
{
    public class RoomObject
    {
        public JSONObject Data { get; private set; }
        public string Id { get; private set; }
        public string Type { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public string RoomName { get; private set; }

        internal ObjectView View { get; private set; }

        public Action<ObjectView> OnShow;
        public Action<JSONObject> OnDelta;

        internal void Init(JSONObject data)
        {
            if (Data == null)
            {
                Data = data;
            }

            Unpack(data);
        }

        internal void Delta(JSONObject delta)
        {
            Unpack(delta);
            if (View != null)
                View.Delta(delta);
            if (OnDelta != null)
            {
                OnDelta(delta);
            }
        }

        internal virtual void Unpack(JSONObject data)
        {
            var idObj = data["_id"];
            if (idObj != null)
                Id = idObj.str;

            var typeObj = data["type"];
            if (typeObj != null)
                Type = typeObj.str;

            var xObj = data["x"];
            if (xObj != null)
                X = (int) xObj.n;

            var yObj = data["y"];
            if (yObj != null)
                Y = (int) yObj.n;

            var roomNameObj = data["room"];
            if (roomNameObj != null)
                RoomName = roomNameObj.str;
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