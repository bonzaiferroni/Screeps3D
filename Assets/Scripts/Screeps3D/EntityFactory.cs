using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D {
    public class EntityFactory : MonoBehaviour {
        
        private Dictionary<string, ScreepsObject> prototypes = new Dictionary<string, ScreepsObject>();
        private Dictionary<string, ScreepsObject> objects = new Dictionary<string, ScreepsObject>();
        
        private void Start() {
            for (var i = 0; i < transform.childCount; i++) {
                var prototype = transform.GetChild(i).gameObject.GetComponent<ScreepsObject>();
                if (!prototype.gameObject.activeInHierarchy) continue; 
                prototypes[prototype.name] = prototype;
                prototype.gameObject.SetActive(false);
            }
        }

        public ScreepsObject Get(string id, JSONObject jsonObject) {
            if (objects.ContainsKey(id)) return objects[id];
            var type = jsonObject["type"];
            if (type == null || !prototypes.ContainsKey(type.str)) return null;
            var so = Instantiate(prototypes[type.str].gameObject).GetComponent<ScreepsObject>();
            objects[id] = so;
            return so;
        }
    }
}