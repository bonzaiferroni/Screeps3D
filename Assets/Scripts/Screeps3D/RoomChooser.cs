using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Screeps3D {
    public class RoomChooser : MonoBehaviour {
        
        [SerializeField] private TMP_Dropdown shardInput;
        [SerializeField] private TMP_InputField roomInput;
        [SerializeField] private ScreepsAPI api;
        [SerializeField] private FadePanel panel;
        public Action<WorldCoord> OnChooseRoom;
        private List<string> shards = new List<string>();

        private void Start() {
            roomInput.onSubmit.AddListener(ChooseRoom);
            api.OnConnectionStatusChange += OnConnectionStatusChange;
            panel.Show(false, true);
        }

        private void ChooseRoom(string roomName) {
            var coord = WorldCoord.Get(roomInput.text, shards[shardInput.value]);
            if (coord == null) {
                Debug.Log("invalid room");
                return;
            }
            if (OnChooseRoom != null) OnChooseRoom.Invoke(coord);
        }

        private void OnConnectionStatusChange(bool isConnected) {
            panel.Show(isConnected);
            if (!isConnected)
                return;
            api.Http.GetRooms(api.Me.userId, InitializeChooser);
        }

        private void InitializeChooser(string str) {
            var obj = new JSONObject(str);

            var shardObj = obj["shards"];
            if (shardObj != null) {
                shardInput.gameObject.SetActive(true);
                
                shards.Clear();
                var count = 0;
                foreach (var shardName in shardObj.keys) {
                    shards.Add(shardName);
                    var roomList = shardObj[shardName].list;
                    if (roomList.Count > 0 && roomInput.text.Length == 0) {
                        shardInput.value = count;
                        roomInput.text = roomList[0].str;
                    }
                    count++;
                }
                
            } else {
                shardInput.gameObject.SetActive(false);
                shards.Clear();
                shards.Add("shard0");
                shardInput.value = 0;
                
                var roomObj = obj["rooms"];
                if (roomObj != null && roomObj.list.Count > 0)
                    roomInput.text = roomObj.list[0].str;
            }

            shardInput.options.Clear();
            foreach (var shardName in shards) {
                shardInput.options.Add(new TMP_Dropdown.OptionData(shardName));
            }

            ChooseRoom("");
        }
    }
}

/*{"ok":1,"shards":{"shard0":["W2S12","E22S24","E23S15"],"shard1":[],"shard2":[]}}*/