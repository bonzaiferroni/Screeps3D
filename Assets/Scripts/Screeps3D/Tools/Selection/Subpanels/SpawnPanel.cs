using System;
using Common.Utils;
using Screeps3D.RoomObjects;
using TMPro;
using UnityEngine;
using WebSocketSharp;

namespace Screeps3D.Tools.Selection.Subpanels
{
    public class SpawnPanel : LinePanel
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private ScaleVisAxes _meter;
        private Spawn _spawn;
        private RoomObject _roomObject;

        public override string Name
        {
            get { return "Spawning"; }
        }

        public override Type ObjectType
        {
            get { return typeof(Spawn); }
        }

        public override void Load(RoomObject roomObject)
        {
            _roomObject = roomObject;
            roomObject.OnDelta += OnDelta;
            _spawn = roomObject as Spawn;
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            if (_spawn.SpawningName.IsNullOrEmpty())
            {
                _meter.SetVisibility(0);
                _label.text = "Idle";
                return;
            }
            var progress = _spawn.SpawningNeedTime - _spawn.SpawningRemainingTime;
            _meter.SetVisibility(progress / _spawn.SpawningNeedTime);
            _label.text = string.Format("{0:n0} / {1:n0} ({2})", progress, _spawn.SpawningNeedTime, _spawn.SpawningName);
        }

        private void OnDelta(JSONObject obj)
        {
            if (!obj.HasField("spawning")) return;
            UpdateLabel();
        }

        public override void Unload()
        {
            if (_roomObject == null)
                return;
            _roomObject.OnDelta -= OnDelta;
            _roomObject = null;
        }
    }
}