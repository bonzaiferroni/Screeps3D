using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;
using Utils;

namespace Screeps3D {
    [DisallowMultipleComponent]
    public class ObjectManager : BaseSingleton<ObjectManager> {
        public Dictionary<string, RoomObject> RoomObjects { get; private set; }
        private ObjectFactory factory = new ObjectFactory();

        private void Start() {
            RoomObjects = new Dictionary<string, RoomObject>();
        }

        internal RoomObject GetInstance(string id, JSONObject data) {
            if (RoomObjects.ContainsKey(id)) {
                return RoomObjects[id]; 
            }

            var typeData = data["type"];
            if (typeData == null) {
                throw new Exception("type data was not found for new roomObject, there may be a caching problem");
            }
            
            var type = typeData.str;
            
            var roomObject = factory.Get(type);
            roomObject.Init(data);
            RoomObjects[id] = roomObject;
            
            return roomObject;
        }
    }
}
