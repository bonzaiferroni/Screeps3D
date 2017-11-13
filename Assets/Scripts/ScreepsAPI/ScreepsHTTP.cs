using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking;

namespace Screeps3D {
	public class ScreepsHTTP : MonoBehaviour {

		public string Token { get; private set; }
		
		private ScreepsAPI api;

		public void Init(ScreepsAPI api) {
			this.api = api;
		}

		private void Request(string requestMethod, string path, RequestBody body = null, 
			Action<JSONObject> onSuccess = null, Action onError = null) {

			Debug.Log($"HTTP: attempting {requestMethod} to {path}");
			UnityWebRequest www;
			var fullPath = api.Address.Http(path);
			if (requestMethod == UnityWebRequest.kHttpVerbGET) {
				if (body != null) {
					fullPath = fullPath + body.GetQueryString();
				}
				www = UnityWebRequest.Get(fullPath);
			} else if (requestMethod == UnityWebRequest.kHttpVerbPOST) {
				www = UnityWebRequest.Post(fullPath, body);
			} else {
				Debug.Log($"HTTP: request method {requestMethod} unrecognized");
				return;
			}

			Action<UnityWebRequest> onComplete = (UnityWebRequest outcome) => {
				if (outcome.isNetworkError || outcome.isHttpError) {
					Debug.Log($"HTTP: network error, reason: {outcome.error}");
					if (onError != null) {
						onError();
					} else {
						Auth((reply) => {
							Request(requestMethod, path, body, onSuccess);
						}, () => {
							api.OnConnectionStatusChange?.Invoke(false);
						});
					}
				} else {
					Debug.Log($"HTTP: success, data: \n{outcome.downloadHandler.text}");
					var reply = new JSONObject(outcome.downloadHandler.text);
					var token = reply["token"];
					if (token != null) {
						Token = token.str;
						Debug.Log($"HTTP: found a token! {Token}");
					}
					var status = reply["ok"];
					if (status != null && status.n == 1) {
						onSuccess?.Invoke(reply);
					}
				}
			};

			StartCoroutine(SendRequest(www, onComplete));
		}

		private IEnumerator SendRequest(UnityWebRequest www, Action<UnityWebRequest> onComplete) {
			if (Token != null) {
				www.SetRequestHeader("X-Token", Token);
				www.SetRequestHeader("X-Username", Token);	
			}
			yield return www.Send();
			onComplete(www);
		}
		
		public void Auth(Action<JSONObject> onSuccess, Action onError = null) {
			var body = new RequestBody {
				["email"] = api.Credentials.email,
				["password"] = api.Credentials.password
			};
		
			Request("POST", "/api/auth/signin", body, onSuccess, onError);
		}
		
		public void GetUser(Action<JSONObject> onSuccess) {
			Request("GET", "/api/auth/me", null, onSuccess);
		}
		
		public void ConsoleInput(string message) {
			var body = new RequestBody {
				["expression"] = message,
				["shard"] = "shard0"
			};
			Request("POST", "/api/user/console", body);
		}

		public void GetRoom(string roomName, string shard, Action<JSONObject> callback) {
			//return self.req('GET', '/api/game/room-terrain', { room, encoded, shard })
			//self.req('GET', '/api/user/rooms', { id }).then(this.mapToShard)
			var body = new RequestBody {
				["room"] = roomName,
				["encoded"] = "0",
				["shard"] = shard,
			};
			Request("GET", "/api/game/room-terrain", body, callback);
		}
	}
}
