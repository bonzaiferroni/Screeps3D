using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D {
    public class EntityFactory : MonoBehaviour {
        
        private Dictionary<string, RoomObjectView> prototypes = new Dictionary<string, RoomObjectView>();
        private Dictionary<string, RoomObjectView> objects = new Dictionary<string, RoomObjectView>();
        
        private void Start() {
            for (var i = 0; i < transform.childCount; i++) {
                var prototype = transform.GetChild(i).gameObject.GetComponent<RoomObjectView>();
                if (prototype == null || !prototype.gameObject.activeInHierarchy) continue; 
                prototypes[prototype.name] = prototype;
                prototype.gameObject.SetActive(false);
            }
        }

        public RoomObjectView Get(string id, JSONObject jsonObject) {
            if (objects.ContainsKey(id)) return objects[id];
            var type = jsonObject["type"];
            if (type == null || !prototypes.ContainsKey(type.str)) return null;
            var so = Instantiate(prototypes[type.str].gameObject).GetComponent<RoomObjectView>();
            objects[id] = so;
            return so;
        }
    }
}