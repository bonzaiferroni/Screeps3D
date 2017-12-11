using System;
using Screeps3D.RoomObjects;
using TMPro;
using UnityEngine;

namespace Screeps3D.Tools.Selection.Subpanels
{
    public class CooldownPanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI _label;
        
        private Lab _lab;

        public override string Name
        {
            get { return "cooldown"; }
        }

        public override Type ObjectType
        {
            get { return typeof(Lab); }
        }

        public override void Load(RoomObject roomObject)
        {
            _lab = roomObject as Lab;
            _lab.OnDelta += OnDelta;
            UpdateDisplay();
        }

        private void OnDelta(JSONObject obj)
        {
            var cooldownData = obj["cooldown"];
            if (cooldownData == null)
                return;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            _label.text = string.Format("{0}", _lab.Cooldown);
        }

        public override void Unload()
        {
            _lab.OnDelta -= OnDelta;
            _lab = null;
        }
    }
}