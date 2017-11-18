using System;
using System.Collections;
using System.Collections.Generic;
using SocketIO;
using UnityEngine;
using WebSocketSharp;

namespace Screeps3D {
    [RequireComponent(typeof(ScreepsHTTP))]
	[RequireComponent(typeof(ScreepsSocket))]
	public class ScreepsAPI : MonoBehaviour {

	    public static ScreepsAPI Instance { get; private set; }
	    
		public Address Address { get; private set; }
		public Credentials Credentials { get; private set; }
		public ScreepsHTTP Http { get; private set; }
		public ScreepsSocket Socket { get; private set; }
		public ScreepsUser Me { get; private set; }
	    public BadgeGenerator Badges { get; private set; }
	    public UserManager UserManager { get; private set; }
	    public Action<bool> OnConnectionStatusChange;

		private string token;
	
		public void Awake() {
			Instance = this;
			
			Http = GetComponent<ScreepsHTTP>();
			Http.Init(this);
			Socket = GetComponent<ScreepsSocket>();
			Socket.Init(this);
			Badges = new BadgeGenerator(this);
			UserManager = new UserManager(this);
		}


	    // Use this for initialization
		public void Connect (Credentials credentials, Address address) {
			Credentials = credentials;
			Address = address;
			// configure HTTP
			Http.Auth(o => {
				Debug.Log("login successful");
				Socket.Connect();
				Http.GetUser(AssignUser);
			}, () => {
				Debug.Log("login failed");
			});
		}

	    private void AssignUser(string str) {
		    var obj = new JSONObject(str);
		    Me = new ScreepsUser {
			    _id = obj["_id"].str,
			    username = obj["username"].str,
			    cpu = obj["cpu"].n,
		    };
		    
		    Me.badge = Badges.Generate(obj["badge"]);
		    
		    if (OnConnectionStatusChange != null) OnConnectionStatusChange.Invoke(true);
	    }
    }
}
