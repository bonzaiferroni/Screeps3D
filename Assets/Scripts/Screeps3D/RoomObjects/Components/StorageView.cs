using UnityEngine;
using Utils.Utils;

namespace Screeps3D.Components
{
    public class StorageView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private ScaleVisAxes _energyDisplay;

        private Storage _energyObject;

        public void Init(RoomObject roomObject)
        {
            _energyObject = roomObject as Storage;
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