using System;
using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D
{
    public class ScreepsConsole : MonoBehaviour
    {

        public Action<string> OnConsoleMessage;
        public Action<string> OnConsoleError;
        
        private ScreepsAPI _api;
        private Queue<JSONObject> queue = new Queue<JSONObject>();

        public void Init(ScreepsAPI screepsApi)
        {
            _api = screepsApi;
            _api.OnConnectionStatusChange += OnConnectionStatusChange;
        }

        public void Input(string javascript)
        {
            _api.Http.ConsoleInput(javascript);
        }

        private void OnConnectionStatusChange(bool connected)
        {
            if (connected)
            {
                _api.Socket.Subscribe(string.Format("user:{0}/console", _api.Me.userId), RecieveData);
            }
        }

        private void RecieveData(JSONObject obj)
        {
            queue.Enqueue(obj);
        }

        private void Update()
        {
            if (queue.Count == 0)
                return;
            UnpackData(queue.Dequeue());
        }

        private void UnpackData(JSONObject obj)
        {
            var messages = obj["messages"];
            if (messages != null && OnConsoleMessage != null)
            {
                var log = messages["log"];
                if (log != null)
                {
                    foreach (var msgData in log.list)
                    {
                        OnConsoleMessage(msgData.str);
                    }
                }
            }

            var errorData = obj["error"];
            if (errorData != null && OnConsoleError != null)
            {
                OnConsoleError(errorData.str);
            }

            _api.Time++;
            if (_api.OnTick != null)
                _api.OnTick(_api.Time);
        }
    }
}