using UnityEngine;

namespace Screeps3D {
    public class WorkView : MonoBehaviour, IObjectComponent {

        [SerializeField] private Transform partDisplay;
        
        private Creep creep;
        
        public void Init(RoomObject roomObject) {
            creep = roomObject as Creep;

            AdjustSize();
        }

        public void Delta(JSONObject data) {
            AdjustSize();
        }

        private void AdjustSize() {
            var amount = 0f;
            foreach (var part in creep.Body.Parts) {
                if (part.type != "work")
                    continue;
                amount += part.hits;
            }

            var scaleAmount = 0f;
            if (amount > 0) {
                scaleAmount = (amount / 5000) * .8f + .2f;
            }
            
            partDisplay.transform.localScale = Vector3.one * scaleAmount;
        }
    }
}