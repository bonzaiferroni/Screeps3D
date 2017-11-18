using UnityEngine;

namespace Screeps3D.Components {
    public class EnergyView : MonoBehaviour, IObjectComponent {

        [SerializeField] private Transform energy;
        
        private IEnergyObject energyObject;

        public void Init(RoomObject roomObject) {
            energyObject = roomObject as IEnergyObject;
            AdjustScale();
        }

        public void Delta(JSONObject data) {
            AdjustScale();
        }

        private void AdjustScale() {
            energy.localScale = Vector3.one * (energyObject.Energy / energyObject.EnergyCapacity);
        }
    }
}