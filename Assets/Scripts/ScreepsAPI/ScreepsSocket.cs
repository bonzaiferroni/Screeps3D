using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;

namespace Screeps3D {
    public class ScreepsSocket : MonoBehaviour {
        private WebSocket Socket { get; set; }
        private ScreepsAPI api;
        private Dictionary<string, Action<JSONObject>> subscriptions = new Dictionary<string, Action<JSONObject>>();
        
        public Action<EventArgs> OnOpen;
        public Action<CloseEventArgs> OnClose;
        public Action<MessageEventArgs> OnMessage;
        public Action<ErrorEventArgs> OnError;
        public Action<string, bool> OnConsoleMessage;
	
        public void Init(ScreepsAPI screepsApi) {
            api = screepsApi;
        }
	
        private void OnDestroy() {
            UnsubAll();
            Socket?.Close();
        }

        public void Connect() {
            Socket?.Close();
            Socket = new WebSocket($"wss://{api.Address.hostName}:443/socket/websocket");
            Socket.OnOpen += Open;
            Socket.OnError += Error;
            Socket.OnMessage += Message;
            Socket.OnClose += Close;
            Socket.Connect();
        }

        private void Open(object sender, EventArgs e) {
            try {
                Debug.Log($"Socket Open");
                Socket.Send($"auth {api.Http.Token}");
                OnOpen?.Invoke(e);
            } catch (Exception exception) {
                Debug.Log($"Exception in ScreepSocket.OnOpen\n{exception}");
            }
        }

        private void Close(object sender, CloseEventArgs e) {
            try {
                Debug.Log($"Socket Closing: {e.Reason}");
                OnClose?.Invoke(e);
            } catch (Exception exception) {
                Debug.Log($"Exception in ScreepSocket.OnClose\n{exception}");
            }
            
        }

        private void Message(object sender, MessageEventArgs e) {
            try {
                Debug.Log($"Socket Message: {e.Data}");
                var parse = e.Data.Split(' ');
                if (parse.Length == 3 && parse[0] == "auth" && parse[1] == "ok") {
                    Debug.Log("socket auth success");
                }
                var json = new JSONObject(e.Data);
                FindSubscription(json);
                OnMessage?.Invoke(e);
            } catch (Exception exception) {
                Debug.Log($"Exception in ScreepSocket.OnMessage\n{exception}");
            }
        }

        private void Error(object sender, ErrorEventArgs e) {
            try {
                Debug.Log($"Socket Error: {e.Message}");
                OnError?.Invoke(e);
            } catch (Exception exception) {
                Debug.Log($"Exception in ScreepSocket.OnError\n{exception}");
            }
        }

        private void FindSubscription(JSONObject json) {
            var list = json.list;
            if (list == null || list.Count < 2 || !list[0].IsString || !subscriptions.ContainsKey(list[0].str)) {
                return;
            }

            subscriptions[list[0].str]?.Invoke(list[1]);
        }
	
        public void Subscribe(string path, Action<JSONObject> callback) {
            
            var command = $"subscribe {path}";
            Debug.Log(command);
            Socket.Send(command);
            subscriptions[path] = callback;
        }

        private void UnsubAll() {
            if (Socket == null) return;
            foreach (var kvp in subscriptions) {
                Socket.Send($"unsubscribe {kvp.Key}");
            }
        }
    }
}
