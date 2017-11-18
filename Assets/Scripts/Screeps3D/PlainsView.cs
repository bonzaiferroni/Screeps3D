using UnityEngine;

namespace Screeps3D {
    public class PlainsView : MonoBehaviour {
        private Renderer rend;
        private float original;
        private float current;
        private float target;
        private float targetRef;

        public void Highlight() {
            if (!rend) {
                rend = GetComponent<Renderer>();
                original = rend.material.color.r;
            }
            target = original + .1f;
            enabled = true;
        }

        public void Dim() {
            target = original;
            enabled = true;
        }

        public void Update() {
            if (!rend || Mathf.Abs(current - target) < .001f) {
                enabled = false;
                return;
            }
            current = Mathf.SmoothDamp(rend.material.color.r, target, ref targetRef, 1);
            rend.material.color = new Color(current, current, current);
        }
    }
}