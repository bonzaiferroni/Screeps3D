using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screeps3D.Selection {
    public class NamePanel : SelectionPanelComponent {
        
        [SerializeField] private LayoutElement element;
        [SerializeField] private TextMeshProUGUI label;
        
        private INamedObject selected;

        public override float Height {
            get { return element.preferredHeight; }
        }

        public override void Load(RoomObject roomObject) {
            selected = roomObject as INamedObject;
            if (selected != null) {
                element.preferredHeight = 30;
                label.text = selected.Name;
            } else {
                element.preferredHeight = 0;
                label.text = "";
            }
        }

        public override void Unload() {
            selected = null;
        }
    }
}