using System;
using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D
{
    public class CpuMonitor : MonoBehaviour
    {

        public Action<int> OnCpu;
        
        public int CPU { get; private set; }
        public int Memory { get; private set; }
        
        private ScreepsAPI _api;
        private Queue<JSONObject> queue = new Queue<JSONObject>();

        internal void Init(ScreepsAPI screepsApi)
        {
            _api = screepsApi;
            _api.OnConnectionStatusChange += SubscribeCpu;
        }

        private void SubscribeCpu(bool connected)
        {
            if (connected)
            {
                _api.Socket.Subscribe(string.Format("user:{0}/cpu", _api.Me.userId), RecieveData);
            }
        }

        private void RecieveData(JSONObject data)
        {
            queue.Enqueue(data);
        }

        private void Update()
        {
            if (queue.Count == 0)
                return;
            UnpackCpu(queue.Dequeue());
        }

        private void UnpackCpu(JSONObject data)
        {
            var cpuData = data["cpu"];
            if (cpuData != null)
            {
                CPU = (int) cpuData.n;
            }
            
            var memoryData = data["memory"];
            if (memoryData != null)
            {
                Memory = (int) memoryData.n;
            }

            if (OnCpu != null)
                OnCpu(CPU);
        }
    }
}