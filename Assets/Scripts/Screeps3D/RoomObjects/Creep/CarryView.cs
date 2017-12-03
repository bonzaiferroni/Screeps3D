using UnityEngine;

namespace Screeps3D
{
    public class CarryView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private GameObject carry;
        private Creep creep;

        public void Init(RoomObject roomObject)
        {
            creep = roomObject as Creep;
            carry.SetActive(creep.EnergyCapacity > 0);
        }

        public void Delta(JSONObject data)
        {
        }
    }
}