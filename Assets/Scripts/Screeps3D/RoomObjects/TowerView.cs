using UnityEngine;
using Utils.Utils;

namespace Screeps3D {
    public class TowerView : MonoBehaviour, IScreepsComponent {
        
        [SerializeField] private ScaleVisAxes energyDisplay;
        
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