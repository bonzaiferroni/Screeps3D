using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Screeps3D {
    public class EntityView : MonoBehaviour {
        
        [SerializeField] private ScreepsAPI api;
        [SerializeField] private EntityFactory factory;
        
        private Dictionary<string, ScreepsObject> objects = new Dictionary<string, ScreepsObject>();
        
        private JSONObject currentData;
        private WorldCoord coord;

        public void Load(WorldCoord coord) {
            this.coord = coord;
            api.Socket.Subscribe($"room:{coord.shardName}/{coord.roomName}", OnRoomData);
        }

        private void OnDestroy() {
            if (api.Socket != null) {
                api.Socket.Unsub($"room:{coord.shardName}/{coord.roomName}");
            }
        }

        private void OnRoomData(JSONObject data) {
            currentData = data;
        }

        private void Update() {
            if (currentData != null) {
                RenderEntities();
                currentData = null;
            }
        }

        private void RenderEntities() {
            var objects = currentData["objects"];
            foreach (var id in objects.keys) {
                var obj = objects[id];
                if (!this.objects.ContainsKey(id)) {
                    var newSo = factory.Get(id, obj);
                    if (newSo == null) 
                        continue;
                    newSo.transform.SetParent(transform, false);
                    newSo.gameObject.SetActive(true);
                    newSo.LoadObject(obj);
                    this.objects[id] = newSo;
                }
                var so = this.objects[id];
                so.UpdateObject(obj);
            }
        }
    }
}