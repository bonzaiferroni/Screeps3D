using System;
using Common.Utils;
using Screeps3D.RoomObjects;
using Screeps3D.Tools.Selection;
using TMPro;
using UnityEngine;

namespace Screeps3D.Selection
{
    public class ProgressPanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private ScaleVisAxes _meter;
        
        private IProgress _progressor;
        private float _height;
        private RoomObject _roomObject;

        public override float Height { get { return _height; } }

        public override string Name
        {
            get { return "progress"; }
        }

        public override Type ObjectType
        {
            get { return typeof(IProgress); }
        }

        public override void Load(RoomObject roomObject)
        {
            _progressor = roomObject as IProgress;
            if (_progressor.ProgressMax == 0)
            {
                gameObject.SetActive(false);
                _height = 0;
            } else
            {
                gameObject.SetActive(true);
                _height = rect.sizeDelta.y;
                _roomObject = roomObject;
                _roomObject.OnDelta += OnDelta;
                UpdateDisplay();
            }
        }

        private void UpdateDisplay()
        {
            _label.text = string.Format("{0:n0} / {1:n0}", _progressor.Progress, _progressor.ProgressMax);
            _meter.Visible(_progressor.Progress / _progressor.ProgressMax);
        }

        private void OnDelta(JSONObject obj)
        {
            UpdateDisplay();
        }

        public override void Unload()
        {
            _progressor = null;
            if (_roomObject != null)
                _roomObject.OnDelta -= OnDelta;
            _roomObject = null;
        }
    }
}