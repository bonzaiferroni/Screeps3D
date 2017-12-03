using UnityEngine;

namespace Screeps3D
{
    public class CarryView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private GameObject _carry;
        private Creep _creep;

        public void Init(RoomObject roomObject)
        {
            _creep = roomObject as Creep;
            _carry.SetActive(_creep.EnergyCapacity > 0);
        }

        public void Delta(JSONObject data)
        {
        }
    }
}