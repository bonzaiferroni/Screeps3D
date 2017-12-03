using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screeps3D.Selection
{
    public class HitpointsPanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI _label;
        private IHitpointsObject _hitsObject;
        private RoomObject _roomObject;

        public override string Name
        {
            get { return "hitpoints"; }
        }

        public override Type ObjectType
        {
            get { return typeof(IHitpointsObject); }
        }

        public override void Load(RoomObject roomObject)
        {
            _roomObject = roomObject;
            _hitsObject = roomObject as IHitpointsObject;
            roomObject.OnDelta += OnDelta;
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            _label.text = string.Format("hits: {0:n0} / {1:n0}", _hitsObject.Hits, (long) _hitsObject.HitsMax);
        }

        private void OnDelta(JSONObject obj)
        {
            var hitsData = obj["hits"];
            if (hitsData == null) return;
            UpdateLabel();
        }

        public override void Unload()
        {
            if (_roomObject == null)
                return;
            _roomObject.OnDelta -= OnDelta;
        }
    }
}