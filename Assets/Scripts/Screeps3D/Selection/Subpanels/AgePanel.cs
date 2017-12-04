using System;
using System.Globalization;
using TMPro;
using UnityEngine;

namespace Screeps3D.Selection
{
    public class AgePanel : Subpanel
    {
        [SerializeField] private TextMeshProUGUI label;
        
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
            label.text = _creep.AgeTime.ToString(CultureInfo.InvariantCulture);
        }

        public override void Unload()
        {
            _creep = null;
        }
    }
}