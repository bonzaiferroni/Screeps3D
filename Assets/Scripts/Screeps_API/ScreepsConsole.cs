using System;
using System.Collections.Generic;
using UnityEngine;

namespace Screeps_API
{
    public class ScreepsConsole : MonoBehaviour
    {

        public Action<string> OnConsoleMessage;
        public Action<string> OnConsoleError;
        public Action<string> OnConsoleResult;
        
        private ScreepsAPI _api;
        private Queue<JSONObject> queue = new Queue<JSONObject>();

        public void Init(ScreepsAPI screepsApi)
        {
            _api = screepsApi;
            _api.OnConnectionStatusChange += OnConnectionStatusChange;
        }

        public void Input(string javascript)
        {
            _api.Http.ConsoleInput(AddEscapes(javascript));
        }

        private void OnConnectionStatusChange(bool connected)
        {
            if (connected)
            {
                _api.Socket.Subscribe(string.Format("user:{0}/console", _api.Me.UserId), RecieveData);
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
            if (messages != null)
            {
                var log = messages["log"];
                if (log != null && OnConsoleMessage != null)
                {
                    foreach (var msgData in log.list)
                    {
                        OnConsoleMessage(RemoveEscapes(msgData.str));
                    }
                }
                var results = messages["results"];
                if (results != null && OnConsoleResult != null)
                {
                    foreach (var resultsData in results.list)
                    {
                        OnConsoleResult(RemoveEscapes(resultsData.str));
                    }
                }
            }

            var errorData = obj["error"];
            if (errorData != null && OnConsoleError != null)
            {
                OnConsoleError(RemoveEscapes(errorData.str));
            }

            _api.Time++;
            if (_api.OnTick != null)
                _api.OnTick(_api.Time);
        }

        private string AddEscapes(string str)
        {
            str = str.Replace("\"", "\\\"");
            return str;
        }

        private string RemoveEscapes(string str)
        {
            str = str.Replace("\\n", "\n");
            str = str.Replace("\\\\", "\\");
            return str;
        }
    }
}