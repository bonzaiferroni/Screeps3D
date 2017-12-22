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
        
        private Queue<JSONObject> queue = new Queue<JSONObject>();

        private void Start()
        {
            ScreepsAPI.OnConnectionStatusChange += OnConnectionStatusChange;
        }

        public void Input(string javascript)
        {
            ScreepsAPI.Http.ConsoleInput(AddEscapes(javascript));
        }

        private void OnConnectionStatusChange(bool connected)
        {
            if (connected)
            {
                ScreepsAPI.Socket.Subscribe(string.Format("user:{0}/console", ScreepsAPI.Me.UserId), RecieveData);
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

            ScreepsAPI.Instance.IncrementTime();
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