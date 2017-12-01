using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;

namespace Screeps3D {
    internal class MapView : MonoBehaviour {
        
        [SerializeField] private RoadView roadPrototype;
        private WorldCoord coord;
        private string path;
        private bool awake;
        public RoadView[,] roads = new RoadView[50,50];
        private Queue<JSONObject> queue = new Queue<JSONObject>();

        public void Load(WorldCoord coord) {
            this.coord = coord;
            
            if (ScreepsAPI.Instance.Address.hostName.ToLowerInvariant() == "screeps.com") {
                path = string.Format("roomMap2:{0}/{1}", coord.shardName, coord.roomName);
            } else {
                path = string.Format("roomMap2:{0}", coord.roomName);
            }
        }

        public void Wake() {
            if (awake)
                return;

            awake = true;
            ScreepsAPI.Instance.Socket.Subscribe(path, OnMapData);
        }

        private void OnMapData(JSONObject obj) {
            queue.Enqueue(obj);
        }

        private void Update() {
            if (queue.Count == 0)
                return;

            var data = queue.Dequeue();
            var roadObj = data["r"];
            if (roadObj != null) {
                UnpackRoads(roadObj);
            }
        }

        private void UnpackRoads(JSONObject roadObj) {
            foreach (var posArray in roadObj.list) {
                var x = (int) posArray.list[0].n;
                var y = (int) posArray.list[1].n;
                if (roads[x, y] == null) {
                    roads[x, y] = GetRoad();
                    roads[x, y].Init(this, x, y);
                    roads[x, y].transform.SetParent(transform, false);
                    roads[x, y].transform.localPosition = new Vector3(x, 0, 49 - y);
                }
            }
        }

        private RoadView GetRoad() {
            return Instantiate(roadPrototype.gameObject).GetComponent<RoadView>();
        }

        /*public static string Decompress(string strData) {
            var data = Encoding.UTF8.GetBytes(strData);
            var output = new MemoryStream();
            using(var compressedStream = new MemoryStream(data))
            using(var zipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
            {
                zipStream.CopyTo(output);
                zipStream.Close();
                output.Position = 0;
                return output.ToString();
            }
        }*/
    }
}