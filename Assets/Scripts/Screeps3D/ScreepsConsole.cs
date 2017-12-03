using System;
using System.IO;
using UnityEngine;
using Screeps3D;

namespace Screeps3D
{
    [RequireComponent(typeof(ScreepsAPI))]
    public class ScreepsConsole : MonoBehaviour
    {
        [SerializeField] private UnityConsole _console;
        private ScreepsAPI _api;

        private void Start()
        {
            _api = ScreepsAPI.Instance;
            _api.OnConnectionStatusChange += OnConnectionStatusChange;
            _console.OnInput += OnInput;
            _api.Socket.OnConsoleMessage += PrintMessage;
            _console._panel.Show(false, true);
        }

        private void OnConnectionStatusChange(bool isConnected)
        {
            _console._panel.Show(isConnected);
            _api.Socket.Subscribe(string.Format("user:{0}/console", _api.Me.userId), OnConsoleData);
        }

        private void OnConsoleData(JSONObject obj)
        {
            var messages = obj["messages"];
            if (messages != null)
            {
                var log = messages["log"];
                if (log != null)
                {
                    foreach (var jsonObject in log.list)
                    {
                        PrintMessage(jsonObject.str, false);
                    }
                }
                return;
            }

            var error = obj["error"];
            if (error != null)
            {
                PrintMessage(error.str, true);
            }
        }

        private void OnInput(string obj)
        {
            _api.Http.ConsoleInput(obj);
        }

        private void PrintMessage(string message, bool isError)
        {
            var reader = new StringReader(message);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Replace("\\n", "\n");
                line = line.Replace("\\\\", "\\");
                if (isError)
                {
                    _console.AddMessage(line, Color.red);
                } else
                {
                    _console.AddMessage(line, Color.white);
                }
            }
        }
    }
}