﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

namespace Screeps3D {
    public class EntityView : MonoBehaviour {
        
        [SerializeField] private PlainsView plains;
        
        private Dictionary<string, RoomObject> roomObjects = new Dictionary<string, RoomObject>();
        private Queue<JSONObject> roomData = new Queue<JSONObject>();
        private List<string> removeList = new List<string>();
        private WorldCoord coord;
        private string path;
        private bool awake;
        
        private static Queue<EntityView> queue = new Queue<EntityView>();

        public void Load(WorldCoord coord) {
            this.coord = coord;
            
            if (ScreepsAPI.Instance.Address.hostName.ToLowerInvariant() == "screeps.com") {
                path = string.Format("room:{0}/{1}", coord.shardName, coord.roomName);
            } else {
                path = string.Format("room:{0}", coord.roomName);
            }
        }

        public void Wake() {
            if (awake)
                return;

            if (queue.Count >= 2) {
                var otherView = queue.Dequeue();
                otherView.Sleep();
            }
            queue.Enqueue(this);
            
            Debug.Log("subscribing: " + path);
            ScreepsAPI.Instance.Socket.Subscribe(path, OnRoomData);
            plains.Highlight();
            awake = true;
        }

        private void Sleep() {
            ScreepsAPI.Instance.Socket.Unsub(path);
            plains.Dim();
            awake = false;
        }

        private void OnDestroy() {
            if (ScreepsAPI.Instance.Socket != null && coord != null) {
                ScreepsAPI.Instance.Socket.Unsub(path);
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
            UnpackBadges(data);
            var objects = data["objects"];
            foreach (var id in objects.keys) {
                var datum = objects[id];

                if (!roomObjects.ContainsKey(id)) {
                    roomObjects[id] = ObjectManager.Instance.GetInstance(id, datum, this);
                } 
            }

            removeList.Clear();
            foreach (var kvp in roomObjects) {
                var id = kvp.Key;
                var roomObject = kvp.Value;
                
                var datum = JSONObject.obj; // will generate lots of garbage
                if (objects.HasField(id)) {
                    datum = objects[id];
                }
                if (datum != null && datum.IsNull) {
                    ObjectManager.Instance.Remove(id, coord.roomName);
                    removeList.Add(id);
                } else {
                    roomObject.Delta(datum);   
                }
            }

            foreach (var id in removeList) {
                roomObjects.Remove(id);
            }
        }

        private void UnpackBadges(JSONObject data) {
            var userObj = data["users"];
            if (userObj == null) {
                return;
            }

            foreach (var id in userObj.keys) {
                var datum = userObj[id];
                ScreepsAPI.Instance.Badges.CacheBadge(id, datum);
            } 
        }
    }
}