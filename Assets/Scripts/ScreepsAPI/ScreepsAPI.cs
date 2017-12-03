using System;
using System.Collections;
using System.Collections.Generic;
using Common;
using SocketIO;
using UnityEngine;
using WebSocketSharp;

namespace Screeps3D
{
    [RequireComponent(typeof(ScreepsHTTP))]
    [RequireComponent(typeof(ScreepsSocket))]
    public class ScreepsAPI : BaseSingleton<ScreepsAPI>
    {
        public static ScreepsAPI Instance { get; private set; }

        public Address Address { get; private set; }
        public Credentials Credentials { get; private set; }
        public ScreepsHTTP Http { get; private set; }
        public ScreepsSocket Socket { get; private set; }
        public ScreepsUser Me { get; private set; }
        public BadgeManager Badges { get; private set; }
        public UserManager UserManager { get; private set; }
        public Action<bool> OnConnectionStatusChange;

        private string _token;

        public bool IsConnected { get; private set; }

        public override void Awake()
        {
            base.Awake();
            Instance = this;

            Http = GetComponent<ScreepsHTTP>();
            Http.Init(this);
            Socket = GetComponent<ScreepsSocket>();
            Socket.Init(this);
            Badges = GetComponent<BadgeManager>();
            Badges.Init(this);
            UserManager = new UserManager(this);
        }


        // Use this for initialization
        public void Connect(Credentials credentials, Address address)
        {
            Credentials = credentials;
            Address = address;
            // configure HTTP
            Http.Auth(o =>
            {
                Debug.Log("login successful");
                Socket.Connect();
                Http.GetUser(AssignUser);
            }, () => { Debug.Log("login failed"); });
        }

        private void AssignUser(string str)
        {
            var obj = new JSONObject(str);
            Me = UserManager.CacheUser(obj);

            if (OnConnectionStatusChange != null) OnConnectionStatusChange.Invoke(true);
            IsConnected = true;
        }
    }
}