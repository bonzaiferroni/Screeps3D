using System;
using TMPro;
using UnityEngine;

namespace Screeps3D {
    public class RoomChooser : MonoBehaviour {
        
        [SerializeField] private TMP_InputField shardInput;
        [SerializeField] private TMP_InputField roomInput;
        [SerializeField] private ScreepsAPI api;
        [SerializeField] private FadePanel panel;
        public Action<WorldCoord> OnChooseRoom;

        private void Start() {
            roomInput.onSubmit.AddListener(ChooseRoom);
            shardInput.onSubmit.AddListener(ChooseRoom);
            api.OnConnectionStatusChange += OnConnectionStatusChange;
            panel.Show(false, true);
        }

        private void ChooseRoom(string roomName) {
            var coord = WorldCoord.Get(roomInput.text, shardInput.text);
            if (coord == null) {
                Debug.Log("invalid room");
                return;
            }
            OnChooseRoom?.Invoke(coord);
        }

        private int ShardLevel(string shardName) {
            if (shardName.Length < 6)
                return 0;
            var shard = 0;
            int.TryParse(shardName.Substring(5), out shard);
            return shard;
        }

        private void OnConnectionStatusChange(bool isConnected) {
            panel.Show(isConnected);
        }
    }
}