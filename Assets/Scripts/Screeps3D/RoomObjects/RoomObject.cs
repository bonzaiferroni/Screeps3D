using System;
using System.Collections.Generic;

namespace Screeps3D {
    public class RoomObject {
        
        public JSONObject Data { get; private set; }
        public string Id { get; private set; }
        public string Type { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        public string RoomName { get; private set; }
        
        internal ObjectView View { get; private set; }

        internal void Init(JSONObject data, ObjectView view) {
            
            Data = data;
            View = view;
            Unpack(data);

            if (View != null) {
                View.Init(this);
            }
        }

        internal void Delta(JSONObject delta) {
            Unpack(delta);
            if (View != null)
                View.Delta(delta);
        }

        internal virtual void Unpack(JSONObject data) {
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

        internal virtual void Remove() {
            if (View != null)
                View.Hide();
        }
    }

    internal interface IEnergyObject {
        float Energy { get; }
        float EnergyCapacity { get; }
    }
}