using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screeps3D.Selection {
    public class HitpointsPanel : SelectionPanelComponent {
        [SerializeField] private LayoutElement element;
        [SerializeField] private TextMeshProUGUI label;
        private IHitpointsObject hitsObject;
        private RoomObject roomObject;

        public override float Height {
            get { return element.preferredHeight; }
        }

        public override void Load(RoomObject roomObject) {
            this.roomObject = roomObject;
            hitsObject = roomObject as IHitpointsObject;
            if (hitsObject != null) {
                element.preferredHeight = 30;
                roomObject.OnDelta += OnDelta;
                UpdateLabel();
            } else {
                element.preferredHeight = 0;
                label.text = "";
            }
        }

        private void UpdateLabel() {
            label.text = string.Format("hits: {0:n0} / {1:n0}", hitsObject.Hits, (long) hitsObject.HitsMax);
        }

        private void OnDelta(JSONObject obj) {
            var hitsData = obj["hits"];
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