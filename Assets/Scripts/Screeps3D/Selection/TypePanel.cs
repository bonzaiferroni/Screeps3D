using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screeps3D.Selection {
    public class TypePanel : SelectionPanelComponent {
        
        [SerializeField] private TMP_Text label;
        [SerializeField] private Button button;
        private RoomObject selected;
        public override float Height { get { return 30; }}

        private void Start() {
            button.onClick.AddListener(OnClick);
        }

        public override void Load(RoomObject roomObject) {
            label.text = roomObject.Type;
            selected = roomObject;
        }

        public override void Unload() {
            selected = null;
        }

        private void OnClick() {
            if (selected == null) return;
            Selection.Instance.DeselectObject(selected);
        }
    }
}