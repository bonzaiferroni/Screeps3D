using UnityEngine;

namespace Screeps3D {
    public class Creep : ScreepsObject {

        [SerializeField] private ScreepsAPI api;
        
        public override void LoadObject(JSONObject obj) {
            base.LoadObject(obj);
            GetComponent<Renderer>().material.mainTexture = api.Me.badge;
        }
    }
}