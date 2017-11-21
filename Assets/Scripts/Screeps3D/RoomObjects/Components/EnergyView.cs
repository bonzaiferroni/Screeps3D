using UnityEngine;
using Utils;

namespace Screeps3D.Components {
    public class EnergyView : MonoBehaviour, IScreepsComponent {

        [SerializeField] private ScaleVis energyDisplay;
        
        private IEnergyObject energyObject;

        public void Init(RoomObject roomObject) {
            energyObject = roomObject as IEnergyObject;
            AdjustScale();
        }

        public void Delta(JSONObject data) {
            AdjustScale();
        }

        private void AdjustScale() {
            energyDisplay.Visible(energyObject.Energy / energyObject.EnergyCapacity);
        }
    }
}