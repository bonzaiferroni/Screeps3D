using System;
using System.Collections.Generic;
using Common;
using Screeps_API;
using TMPro;
using UnityEngine;

namespace Screeps3D.Rooms
{
    public class RoomChooser : MonoBehaviour
    {
        public Action<Room> OnChooseRoom;
        
        [SerializeField] private TMP_Dropdown _shardInput;
        [SerializeField] private TMP_InputField _roomInput;
        private List<string> _shards = new List<string>();

        private void Start()
        {
            _roomInput.onSubmit.AddListener(ChooseRoom);
            if (ScreepsAPI.IsConnected)
                ScreepsAPI.Http.GetRooms(ScreepsAPI.Me.UserId, InitializeChooser);
            else 
                throw new Exception("RoomChooser assumes ScreepsAPI.IsConnected == true at start of scene");
        }

        private void ChooseRoom(string roomName)
        {
            var room = RoomManager.Instance.Get(_roomInput.text, _shards[_shardInput.value]);
            if (room == null)
            {
                Debug.Log("invalid room");
                return;
            }
            if (OnChooseRoom != null) OnChooseRoom.Invoke(room);
        }

        private void InitializeChooser(string str)
        {
            var obj = new JSONObject(str);

            var shardObj = obj["shards"];
            if (shardObj != null)
            {
                _shardInput.gameObject.SetActive(true);

                _shards.Clear();
                var count = 0;
                foreach (var shardName in shardObj.keys)
                {
                    _shards.Add(shardName);
                    var roomList = shardObj[shardName].list;
                    if (roomList.Count > 0 && _roomInput.text.Length == 0)
                    {
                        _shardInput.value = count;
                        _roomInput.text = roomList[0].str;
                    }
                    count++;
                }
            } else
            {
                _shardInput.gameObject.SetActive(false);
                _shards.Clear();
                _shards.Add("shard0");
                _shardInput.value = 0;

                var roomObj = obj["rooms"];
                if (roomObj != null && roomObj.list.Count > 0)
                    _roomInput.text = roomObj.list[0].str;
            }

            _shardInput.options.Clear();
            foreach (var shardName in _shards)
            {
                _shardInput.options.Add(new TMP_Dropdown.OptionData(shardName));
            }

            ChooseRoom("");
        }
    }
}

/*{"ok":1,"shards":{"shard0":["W2S12","E22S24","E23S15"],"shard1":[],"shard2":[]}}*/