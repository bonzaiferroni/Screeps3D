using System;
using TMPro;
using UnityEngine;

namespace Screeps3D {
    public class RoomChooser : MonoBehaviour {
        
        [SerializeField] private TMP_InputField roomInput;
        [SerializeField] private ScreepsAPI api;
        [SerializeField] private FadePanel panel;
        public Action<string> OnChooseRoom;

        private void Start() {
            roomInput.onSubmit.AddListener(ChooseRoom);
            api.OnConnectionStatusChange += OnConnectionStatusChange;
            panel.Show(false, true);
        }

        private void ChooseRoom(string arg0) {
            OnChooseRoom?.Invoke(arg0);
        }

        private void OnConnectionStatusChange(bool isConnected) {
            panel.Show(isConnected);
        }
    }
}