using UnityEngine;

namespace Screeps3D {
    public class Creep : RoomObjectView {

        [SerializeField] private ScreepsAPI api;
        
        private Quaternion rotTarget;
        private Vector3 posTarget;
        private Vector3 posRef;
        
        public override void LoadObject(JSONObject obj) {
            base.LoadObject(obj);
            GetComponent<Renderer>().material.mainTexture = api.Me.badge;
            rotTarget = transform.rotation;
            posTarget = transform.localPosition;
        }

        public override void UpdateObject(JSONObject obj) {
            base.UpdateObject(obj);
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