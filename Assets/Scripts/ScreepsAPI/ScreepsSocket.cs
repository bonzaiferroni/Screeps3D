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
            if (Socket != null) Socket.Close();
        }

        public void Connect() {
            if (Socket != null) {
                Socket.Close();
            }
            
            var protocol = api.Address.ssl ? "wss" : "ws";
            var port = api.Address.ssl ? "443" : api.Address.port;
            Socket = new WebSocket(string.Format("{0}://{1}:{2}/socket/websocket", protocol, api.Address.hostName, port));
            Socket.OnOpen += Open;
            Socket.OnError += Error;
            Socket.OnMessage += Message;
            Socket.OnClose += Close;
            Socket.Connect();
        }

        private void Open(object sender, EventArgs e) {
            try {
                Debug.Log("Socket Open");
                Socket.Send(string.Format("auth {0}", api.Http.Token));
                if (OnOpen != null) OnOpen.Invoke(e);
            } catch (Exception exception) {
                Debug.Log(string.Format("Exception in ScreepSocket.OnOpen\n{0}", exception));
            }
        }

        private void Close(object sender, CloseEventArgs e) {
            try {
                Debug.Log(string.Format("Socket Closing: {0}", e.Reason));
                if (OnClose != null) OnClose.Invoke(e);
            } catch (Exception exception) {
                Debug.Log(string.Format("Exception in ScreepSocket.OnClose\n{0}", exception));
            }
            
        }

        private void Message(object sender, MessageEventArgs e) {
            try {
                Debug.Log(string.Format("Socket Message: {0}", e.Data));
                var parse = e.Data.Split(' ');
                if (parse.Length == 3 && parse[0] == "auth" && parse[1] == "ok") {
                    Debug.Log("socket auth success");
                }
                var json = new JSONObject(e.Data);
                FindSubscription(json);
                if (OnMessage != null) OnMessage.Invoke(e);
            } catch (Exception exception) {
                Debug.Log(string.Format("Exception in ScreepSocket.OnMessage\n{0}", exception));
            }
        }

        private void Error(object sender, ErrorEventArgs e) {
            try {
                Debug.Log(string.Format("Socket Error: {0}", e.Message));
                if (OnError != null) OnError.Invoke(e);
            } catch (Exception exception) {
                Debug.Log(string.Format("Exception in ScreepSocket.OnError\n{0}", exception));
            }
        }

        private void FindSubscription(JSONObject json) {
            var list = json.list;
            if (list == null || list.Count < 2 || !list[0].IsString || !subscriptions.ContainsKey(list[0].str)) {
                return;
            }

            subscriptions[list[0].str].Invoke(list[1]);
        }
	
        public void Subscribe(string path, Action<JSONObject> callback) {
            Socket.Send(string.Format("subscribe {0}", path));
            subscriptions[path] = callback;
        }
        
        public void Unsub(string path) {
            Socket.Send(string.Format("unsubscribe {0}", path));
        }

        private void UnsubAll() {
            if (Socket == null) return;
            foreach (var kvp in subscriptions) {
                Unsub(kvp.Key);
            }
        }
    }
}
