using UnityEngine;

namespace Screeps3D {
    public class ScreepsObject : MonoBehaviour {
        
        private Quaternion rotTarget;
        private Vector3 posTarget;
        private Vector3 posRef;

        public virtual void LoadObject(JSONObject obj) {
            transform.localPosition = GetPos(obj);
            rotTarget = transform.rotation;
            posTarget = transform.localPosition;
        }

        private Vector3 GetPos(JSONObject obj) {
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

        public void UpdateObject(JSONObject obj) {
            var newPos = GetPos(obj);
            var delta = transform.localPosition - newPos;
            if (delta.sqrMagnitude > .1) {
                enabled = true;
                rotTarget = Quaternion.LookRotation(delta);
            }
            posTarget = newPos;
        }

        private void Update() {
            if ((transform.localPosition - posTarget).sqrMagnitude < .01) {
                enabled = false;
            }
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, posTarget, ref posRef, .5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * 5);
        }
    }
}