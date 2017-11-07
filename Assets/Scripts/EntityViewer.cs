using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Screeps3D {
    public class EntityViewer : MonoBehaviour {
        
        [SerializeField] private ScreepsAPI api;
        [SerializeField] private RoomChooser chooser;

        [SerializeField] private Dictionary<string, ScreepsObject> prototypes = new Dictionary<string, ScreepsObject>();
        [SerializeField] private Dictionary<string, ScreepsObject> objects = new Dictionary<string, ScreepsObject>();
        
        private JSONObject currentData;

        private void Start() {
            chooser.OnChooseRoom += ViewRoom;
            for (var i = 0; i < transform.childCount; i++) {
                var prototype = transform.GetChild(i).gameObject.GetComponent<ScreepsObject>();
                if (!prototype.gameObject.activeInHierarchy) continue; 
                prototypes[prototype.name] = prototype;
                prototype.gameObject.SetActive(false);
            }
        }

        private void ViewRoom(string roomName) {
            // // this.screeps.subscribe(`room:${roomName}`, this.onRoomUpdate);
            api.Socket.Subscribe($"room:shard0/{roomName}", OnRoomData);
        }

        private void OnRoomData(JSONObject data) {
            currentData = data;
           
        }

        private void Update() {
            if (currentData != null) {
                RenderEntities(currentData);
                currentData = null;
            }
        }

        private void RenderEntities(JSONObject jsonObject) {
            var objects = currentData["objects"];
            foreach (var id in objects.keys) {
                var obj = objects[id];
                if (!this.objects.ContainsKey(id)) {
                    var type = obj["type"];
                    if (type == null || !prototypes.ContainsKey(type.str)) continue;
                    var newSo = Instantiate(prototypes[type.str].gameObject).GetComponent<ScreepsObject>();
                    newSo.LoadObject(obj);
                    newSo.gameObject.SetActive(true);
                    newSo.transform.SetParent(transform);
                    this.objects[id] = newSo;
                }
                var so = this.objects[id];
                so.UpdateObject(obj);
            }
        }
    }
}