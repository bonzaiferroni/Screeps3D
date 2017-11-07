using UnityEngine;

namespace Screeps3D {
    public class ScreepsObject : MonoBehaviour {
        
        private Quaternion rotTarget;
        private Vector3 posTarget;
        private Vector3 posRef;

        public void LoadObject(JSONObject obj) {
            transform.position = GetPos(obj);
            rotTarget = transform.rotation;
            posTarget = transform.position;
        }

        private Vector3 GetPos(JSONObject obj) {
            var x = transform.position.x;
            var xObj = obj["x"];
            if (xObj != null) {
                x = -xObj.n;
            }
            var y = transform.position.z;
            var yObj = obj["y"];
            if (yObj != null) {
                y = yObj.n;
            }
            return new Vector3(x, transform.position.y, y);
        }

        public void UpdateObject(JSONObject obj) {
            var newPos = GetPos(obj);
            var delta = transform.position - newPos;
            if (delta.sqrMagnitude > .1) {
                enabled = true;
                rotTarget = Quaternion.LookRotation(delta);
            }
            posTarget = newPos;
        }

        private void Update() {
            if ((transform.position - posTarget).sqrMagnitude < .1) {
                enabled = false;
            }
            transform.position = Vector3.SmoothDamp(transform.position, posTarget, ref posRef, .5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime);
        }
    }
}