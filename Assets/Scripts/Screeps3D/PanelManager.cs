using System;
using Common;
using Screeps_API;
using UnityEngine;

namespace Screeps3D
{
    public class PanelManager : BaseSingleton<PanelManager>
    {
        public static PanelMode CurrentMode { get; private set; }
        public static event Action<PanelMode> OnModeChange;
        
        private void Start()
        {
            ScreepsAPI.OnConnectionStatusChange += OnConnectionStatusChange;
        }

        private void Update()
        {
            ChangeMode(PanelMode.Login);
            enabled = false;
        }

        private void OnConnectionStatusChange(bool isConnected)
        {
            if (isConnected)
            {
                ChangeMode(PanelMode.Room);
            }
            else
            {
                ChangeMode(PanelMode.Login);
            }
        }

        private void ChangeMode(PanelMode mode)
        {
            CurrentMode = mode;
            if (OnModeChange != null)
                OnModeChange(mode);
        }
    }
    
    public enum PanelMode
    {
        Login,
        Options,
        Room,
        Map,
    }
}