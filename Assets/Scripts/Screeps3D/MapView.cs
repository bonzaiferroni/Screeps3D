using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using UnityEngine;

namespace Screeps3D
{
    internal class MapView : MonoBehaviour
    {
        public RoadView[,] roads = new RoadView[50, 50];
        
        [SerializeField] private RoadView _roadPrototype;
        private Room _room;
        private string _path;
        private bool _awake;
        private Queue<JSONObject> _queue = new Queue<JSONObject>();

        public void Load(Room room)
        {
            _room = room;

            if (ScreepsAPI.Instance.Address.hostName.ToLowerInvariant() == "screeps.com")
            {
                _path = string.Format("roomMap2:{0}/{1}", room.shardName, room.roomName);
            } else
            {
                _path = string.Format("roomMap2:{0}", room.roomName);
            }
        }

        public void Wake()
        {
            if (_awake)
                return;

            _awake = true;
            ScreepsAPI.Instance.Socket.Subscribe(_path, OnMapData);
        }

        private void OnMapData(JSONObject obj)
        {
            _queue.Enqueue(obj);
        }

        private void Update()
        {
            if (_queue.Count == 0)
                return;

            var data = _queue.Dequeue();
            var roadObj = data["r"];
            if (roadObj != null)
            {
                UnpackRoads(roadObj);
            }
        }

        private void UnpackRoads(JSONObject roadObj)
        {
            foreach (var posArray in roadObj.list)
            {
                var x = (int) posArray.list[0].n;
                var y = (int) posArray.list[1].n;
                if (roads[x, y] == null)
                {
                    roads[x, y] = GetRoad();
                    roads[x, y].Init(this, x, y);
                    roads[x, y].transform.SetParent(transform, false);
                    roads[x, y].transform.localPosition = new Vector3(x, 0, 49 - y);
                }
            }
        }

        private RoadView GetRoad()
        {
            return Instantiate(_roadPrototype.gameObject).GetComponent<RoadView>();
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