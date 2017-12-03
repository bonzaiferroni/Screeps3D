using UnityEngine;
using UnityEngine.EventSystems;

namespace Screeps3D
{
    public class PlayerGaze : MonoBehaviour
    {

        private void Update()
        {
            if (!ScreepsAPI.Instance || !ScreepsAPI.Instance.IsConnected)
            {
                return;
            }

            var ray = Camera.main.ViewportPointToRay(new Vector3(.5f, .5f, 0));
            if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonUp(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            }

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 200, 1 << 10))
            {
                var roomView = hit.collider.GetComponent<RoomView>();
                if (roomView == null)
                {
                    return;
                }

                roomView.Target();
            }
        }
    }
}