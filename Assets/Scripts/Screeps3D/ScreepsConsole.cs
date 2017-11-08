using System.IO;
using UnityEngine;
using Screeps3D;

namespace Screeps3D {
    
    [RequireComponent(typeof(ScreepsAPI))]
    public class ScreepsConsole : MonoBehaviour {
        [SerializeField] private UnityConsole console;
        [SerializeField] private ScreepsAPI api;

        private void Start() {
            api.OnConnectionStatusChange += OnConnectionStatusChange; 
            console.OnInput += OnInput;
            api.Socket.OnConsoleMessage += PrintMessage;
            console.panel.Show(false, true);
        }

        private void OnConnectionStatusChange(bool isConnected) {
            console.panel.Show(isConnected);
            api.Socket.Subscribe($"user:{api.User._id}/console", OnConsoleData);
        }

        private void OnConsoleData(JSONObject obj) {
            var messages = obj["messages"];
            if (messages != null) {
                var log = messages["log"];
                if (log != null) {
                    foreach (var jsonObject in log.list) {
                        PrintMessage(jsonObject.str, false);
                    }
                }
                return;
            }
            
            var error = obj["error"];
            if (error != null) {
                PrintMessage(error.str, true);
            }
        }

        private void OnInput(string obj) {
            api.Http.ConsoleInput(obj);
        }

        private void PrintMessage(string message, bool isError) {
            var reader = new StringReader(message);
            string line;
            while ((line = reader.ReadLine()) != null) {
                line = line.Replace("\\n", "\n");
                line = line.Replace("\\\\", "\\");
                if (isError) {
                    console.AddMessage(line, Color.red);
                } else {
                    console.AddMessage(line, Color.white);
                }
            }
        }
    }
}