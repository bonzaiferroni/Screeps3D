using System;
using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D {
    public class ObjectManager : MonoBehaviour {
        
        private Dictionary<string, ObjectView> prototypes = new Dictionary<string, ObjectView>();
        private Dictionary<string, ObjectView> viewCache = new Dictionary<string, ObjectView>();
        private Dictionary<string, RoomObject> cache = new Dictionary<string, RoomObject>();
        private ObjectFactory factory = new ObjectFactory();
        
        private void Start() {
            for (var i = 0; i < transform.childCount; i++) {
                var prototype = transform.GetChild(i).gameObject.GetComponent<ObjectView>();
                if (prototype == null || !prototype.gameObject.activeInHierarchy) continue; 
                prototypes[prototype.name] = prototype;
                prototype.gameObject.SetActive(false);
            }
        }

        internal RoomObject Get(string id, JSONObject data) {
            if (cache.ContainsKey(id)) {
                return cache[id];
            }

            var type = data["type"].str;
            
            var view = GetView(id, type);
            var roomObject = factory.Get(type);
            roomObject.Init(data, view);
            cache[id] = roomObject;
            
            return roomObject;
        }
        
        private ObjectView GetView(string id, string type) {
            if (viewCache.ContainsKey(id)) 
                return viewCache[id];
            if (!prototypes.ContainsKey(type))
                return null;
            
            var so = Instantiate(prototypes[type].gameObject).GetComponent<ObjectView>();
            viewCache[id] = so;
            return so;
        }

        public void Remove(string id) {
            if (!cache.ContainsKey(id)) {
                return;
            }
            var roomObject = cache[id];
            roomObject.Remove();
            cache.Remove(id);
        }
    }
}
