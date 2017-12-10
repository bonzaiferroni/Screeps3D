using System;
using System.Collections.Generic;
using Common;
using Screeps3D.RoomObjects;
using Screeps3D.Rooms.Views;
using UnityEngine;

namespace Screeps3D.Rooms
{
    public class Room
    {
        public readonly string name;
        public readonly string roomName;
        public readonly string shardName;
        public readonly string xDir;
        public readonly string yDir;
        public readonly int shardNumber;
        public readonly int xCoord;
        public readonly int yCoord;
        public readonly Vector3 position;

        public Dictionary<string, RoomObject> Objects { get; private set; }
        
        public RoomView View { get; private set; }
        public RoomUnpacker RoomUnpacker { get; set; }
        public RoomObjectStream ObjectStream { get; private set; }
        public RoomMapStream MapStream { get; set; }
        
        public bool InitializedView { get; private set; }
        public bool ShowingObjects { get; private set; }
        public bool ShowingMap { get; private set; }
        public bool Shown { get; private set; }
        
        public Action<bool> OnShowObjects;
        public Action<bool> OnShowMap;
        public Action<bool> OnShow;

        public Room(string roomName, string shardName, string xDir, string yDir, int shardNumber, int xCoord, int yCoord)
        {
            name = roomName + shardName;
            this.roomName = roomName;
            this.shardName = shardName;
            this.xDir = xDir;
            this.yDir = yDir;
            this.shardNumber = shardNumber;
            this.xCoord = xCoord;
            this.yCoord = yCoord;
            position = new Vector3(xDir == "E" ? xCoord * 50 : (-xCoord - 1) * 50, shardNumber * 25,
                yDir == "N" ? yCoord * 50 : (-yCoord - 1) * 50);
            Objects = new Dictionary<string, RoomObject>();
        }

        public void ShowObjects(bool show)
        {
            if (show == ShowingObjects)
                return;
            ShowingObjects = show;
            
            if (OnShowObjects != null)
                OnShowObjects(show);
        }

        public void ShowMap(bool show)
        {
            if (show == ShowingMap)
                return;
            ShowingMap = show;
            
            if (OnShowMap != null)
                OnShowMap(show);
        }

        public void Show(bool show)
        {
            if (show == Shown)
                return;
            Shown = show;
            
            if (show && !InitializedView)
            {
                InitializeView();
                InitializedView = true;
            }
            
            if (OnShow != null)
                OnShow(show);
        }

        private void InitializeView()
        {
            ObjectStream = new RoomObjectStream();
            ObjectStream.Init(this);
            MapStream = new RoomMapStream();
            MapStream.Init(this);
            RoomUnpacker = new RoomUnpacker();
            RoomUnpacker.Init(this);
            Scheduler.Instance.Add(AssignView);
        }

        private void AssignView()
        {
            View = RoomViewFactory.Instance.GenerateView(this);
        }
    }
}