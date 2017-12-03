using UnityEngine;
using Utils;

namespace Screeps3D.Components
{
    public class EnergyView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private ScaleVis _energyDisplay;

        private IEnergyObject _energyObject;

        public void Init(RoomObject roomObject)
        {
            _energyObject = roomObject as IEnergyObject;
            AdjustScale();
        }

        public void Delta(JSONObject data)
        {
            AdjustScale();
        }

        private void AdjustScale()
        {
            _energyDisplay.Visible(_energyObject.Energy / _energyObject.EnergyCapacity);
        }
    }
}