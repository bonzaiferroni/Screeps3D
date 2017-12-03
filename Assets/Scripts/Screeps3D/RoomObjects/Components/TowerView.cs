using UnityEngine;
using Utils.Utils;

namespace Screeps3D
{
    public class TowerView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private ScaleVisAxes energyDisplay;
        [SerializeField] private Transform rotationRoot;
        private Quaternion targetRot;
        private float nextRot;

        private IEnergyObject energyObject;
        private float targetRef;

        public void Init(RoomObject roomObject)
        {
            energyObject = roomObject as IEnergyObject;
            AdjustScale();
        }

        public void Delta(JSONObject data)
        {
            AdjustScale();
        }

        private void AdjustScale()
        {
            energyDisplay.Visible(energyObject.Energy / energyObject.EnergyCapacity);
        }

        private void Update()
        {
            if (Time.time > nextRot)
            {
                nextRot = Time.time + Random.value + 1;
                targetRot = rotationRoot.rotation * Quaternion.Euler(0, 180 * Random.value, 0);
            }

            rotationRoot.rotation = Quaternion.Slerp(rotationRoot.rotation, targetRot, Time.deltaTime);
        }
    }
}