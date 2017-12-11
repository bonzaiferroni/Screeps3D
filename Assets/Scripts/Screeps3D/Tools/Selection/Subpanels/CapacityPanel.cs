using System;
using System.Linq;
using Common.Utils;
using Screeps3D.RoomObjects;
using TMPro;
using UnityEngine;

namespace Screeps3D.Tools.Selection.Subpanels
{
    public class CapacityPanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private ScaleVisAxes _meter;
        private IStoreObject _storeObject;
        private RoomObject _roomObject;

        public override string Name
        {
            get { return "capacity"; }
        }

        public override Type ObjectType
        {
            get { return typeof(IStoreObject); }
        }

        public override void Load(RoomObject roomObject)
        {
            _roomObject = roomObject;
            roomObject.OnDelta += OnDelta;
            _storeObject = roomObject as IStoreObject;
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            _meter.Visible(_storeObject.TotalResources / _storeObject.StoreCapacity);
            _label.text = string.Format("{0:n0} / {1:n0}", _storeObject.TotalResources,
                (long) _storeObject.StoreCapacity);
        }

        private void OnDelta(JSONObject obj)
        {
            if (obj.Count > 0)
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