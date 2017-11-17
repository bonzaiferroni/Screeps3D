using UnityEngine;

namespace Screeps3D {
    public class RoomObjectView : MonoBehaviour {

        public virtual void LoadObject(JSONObject obj) {
            transform.localPosition = GetPos(obj);
        }

        protected Vector3 GetPos(JSONObject obj) {
            var x = transform.localPosition.x;
            var xObj = obj["x"];
            if (xObj != null) {
                x = xObj.n;
            }
            var y = transform.localPosition.z;
            var yObj = obj["y"];
            if (yObj != null) {
                y = 49 - yObj.n;
            }
            return new Vector3(x, transform.localPosition.y, y);
        }

        public virtual void UpdateObject(JSONObject obj) {
            
        }

        public void KillObject() {
            Destroy(gameObject);
        }
    }
}