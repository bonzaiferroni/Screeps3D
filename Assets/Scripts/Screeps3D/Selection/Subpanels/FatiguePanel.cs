using System;
using TMPro;
using UnityEngine;

namespace Screeps3D.Selection
{
    public class FatiguePanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI label;
        
        private Creep _creep;

        public override string Name
        {
            get { return "fatigue"; }
        }

        public override Type ObjectType
        {
            get { return typeof(Creep); }
        }

        public override void Load(RoomObject roomObject)
        {
            _creep = roomObject as Creep;
            _creep.OnDelta += OnDelta;
            UpdateDisplay();
        }

        private void OnDelta(JSONObject obj)
        {
            var fatigueData = obj["fatigue"];
            if (fatigueData == null)
                return;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            label.text = string.Format("Fatigue: {0}", _creep.Fatigue);
        }

        public override void Unload()
        {
            _creep.OnDelta -= OnDelta;
        }
    }
}