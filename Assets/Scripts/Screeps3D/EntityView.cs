using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Screeps3D {
    public class EntityView : MonoBehaviour {
        
        [SerializeField] private ScreepsAPI api;
        [SerializeField] private EntityFactory factory;
        
        private Dictionary<string, ScreepsObject> objects = new Dictionary<string, ScreepsObject>();
        
        private Queue<JSONObject> roomData = new Queue<JSONObject>();
        private WorldCoord coord;
        private string path;
        
        public void Load(WorldCoord coord) {
            this.coord = coord;
            
            if (api.Address.hostName.ToLowerInvariant() == "screeps.com") {
                path = string.Format("room:{0}/{1}", coord.shardName, coord.roomName);
            } else {
                path = string.Format("room:{0}", coord.roomName);
            }
            
            api.Socket.Subscribe(path, OnRoomData);
        }

        private void OnDestroy() {
            if (api.Socket != null && coord != null) {
                api.Socket.Unsub(path);
            }
        }

        private void OnRoomData(JSONObject data) {
            roomData.Enqueue(data);
        }

        private void Update() {
            if (roomData.Count == 0)
                return;
            RenderEntities(roomData.Dequeue());
        }

        private void RenderEntities(JSONObject data) {
            var objects = data["objects"];
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