using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screeps3D.Selection {
    public class EnergyPanel : SelectionPanelComponent {
        [SerializeField] private LayoutElement element;
        [SerializeField] private TextMeshProUGUI label;
        private IEnergyObject energyObject;
        private RoomObject roomObject;

        public override float Height {
            get { return element.preferredHeight; }
        }

        public override void Load(RoomObject roomObject) {
            this.roomObject = roomObject;
            energyObject = roomObject as IEnergyObject;
            if (energyObject != null) {
                element.preferredHeight = 30;
                roomObject.OnDelta += OnDelta;
                UpdateLabel();
            } else {
                element.preferredHeight = 0;
                label.text = "";
            }
        }

        private void UpdateLabel() {
            label.text = string.Format("energy: {0:n0} / {1:n0}", energyObject.Energy, (long) energyObject.EnergyCapacity);
        }

        private void OnDelta(JSONObject obj) {
            var hitsData = obj["energy"];
            if (hitsData == null) return;
            UpdateLabel();
        }

        public override void Unload() {
            if (roomObject == null) 
                return;
            roomObject.OnDelta -= OnDelta;
        }
    }
}