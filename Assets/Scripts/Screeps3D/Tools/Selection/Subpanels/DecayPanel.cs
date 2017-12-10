using System;
using Screeps3D.RoomObjects;
using Screeps3D.Tools.Selection;
using Screeps_API;
using TMPro;
using UnityEngine;

namespace Screeps3D.Selection
{
    public class DecayPanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI _label;
        
        private IDecay _decay;

        public override string Name
        {
            get { return "decay"; }
        }

        public override Type ObjectType
        {
            get { return typeof(IDecay); }
        }

        public override void Load(RoomObject roomObject)
        {
            _decay = roomObject as IDecay;
            UpdateLabel();
            ScreepsAPI.Instance.OnTick += OnTick;
        }

        private void OnTick(long obj)
        {
            UpdateLabel();
        }

        public override void Unload()
        {
            _decay = null;
            ScreepsAPI.Instance.OnTick -= OnTick;
        }

        private void UpdateLabel()
        {
            _label.text = string.Format("{0:n0}", _decay.NextDecayTime - ScreepsAPI.Instance.Time);
        }
    }
}