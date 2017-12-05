using UnityEngine;

namespace Screeps3D
{
    public class CarryView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private GameObject _carry;
        private Creep _creep;

        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            _creep = roomObject as Creep;
            _carry.SetActive(_creep.EnergyCapacity > 0);
        }

        public void Delta(JSONObject data)
        {
        }

        public void Unload(RoomObject roomObject)
        {
        }
    }
}