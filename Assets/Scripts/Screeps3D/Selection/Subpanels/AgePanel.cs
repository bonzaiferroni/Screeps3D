using System;
using System.Globalization;
using TMPro;
using UnityEngine;
using Utils.Utils;

namespace Screeps3D.Selection
{
    public class AgePanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI label;
        [SerializeField] private ScaleVisAxes meter;
        
        private Creep _creep;

        public override string Name
        {
            get { return "age"; }
        }

        public override Type ObjectType
        {
            get { return typeof(Creep); }
        }

        public override void Load(RoomObject roomObject)
        {
            _creep = roomObject as Creep;
            ScreepsAPI.Instance.OnTick += OnTick;
            UpdateLabel();
        }

        private void OnTick(int obj)
        {
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            var ttl = _creep.AgeTime - ScreepsAPI.Instance.Time;
            label.text = string.Format("TTL: {0}", ttl);
            meter.Visible(ttl / 1500);
        }

        public override void Unload()
        {
            _creep = null;
            ScreepsAPI.Instance.OnTick -= OnTick;
        }
    }
}