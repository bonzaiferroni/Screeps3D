using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class FlagView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private MeshRenderer rend;
        private Flag _flag;

        public void Init()
        {
        }

        public void Load(RoomObject roomObject)
        {
            _flag = roomObject as Flag;
        }

        public void Delta(JSONObject data)
        {
        }

        public void Unload(RoomObject roomObject)
        {
            _flag = null;
        }

        private void Update()
        {
            if (_flag == null)
                return;

            var primary = Constants.FLAG_COLORS[_flag.PrimaryColor];
            var secondary = Constants.FLAG_COLORS[_flag.SecondaryColor];
            rend.materials[0].color = Color.Lerp(rend.materials[0].color, primary, Time.deltaTime);
            rend.materials[1].color = Color.Lerp(rend.materials[1].color, secondary, Time.deltaTime);
        }
    }
}