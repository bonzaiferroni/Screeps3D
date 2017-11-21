namespace Utils {
    using UnityEngine;

namespace Utils {
    public class ScaleVisAxes : MonoBehaviour, IVisibilityControl {

        [SerializeField] private bool x = true;
        [SerializeField] private bool y = true;
        [SerializeField] private bool z = true;
        [SerializeField] private bool animateOnStart = true;
        [SerializeField] private bool visibleOnStart = true;
        [SerializeField] private float speed = .2f;
        
        public bool IsVisible { get; private set; }

        private Vector3 current;
        private Vector3 target;
        private Vector3 targetRef;

        private void Start() {
            if (animateOnStart) {
                Visible(0, true);
                Visible(1);
            } 
            if (visibleOnStart) {
                Visible(1, true);
            }
        }
        
        public void Visible(bool shown, bool instant = false) {
            var target = shown ? 1 : 0;
            Visible(target, instant);
        }
        
        public void Visible(float target, bool instant = false) {
            enabled = true;
            IsVisible = true;

            if (float.IsNaN(target)) {
                target = 0;
            }

            this.target = transform.localScale;
            if (x) {
                this.target.x = target;
            }
            if (y) {
                this.target.y = target;
            }
            if (z) {
                this.target.z = target;
            }
            if (instant) {
                current = this.target;
            }
        }

        public void Show(bool instant = false) {
            Visible(true, instant);
        }

        public void Hide(bool instant = false) {
            Visible(false, instant);
        }

        public void Toggle(bool instant = false) {
            Visible(!IsVisible, instant);
        }

        public void Update() {
            if (Vector3.Distance(current, target) < .0001f) {
                enabled = false;
            }

            current = Vector3.SmoothDamp(current, target, ref targetRef, speed);
            Scale(current);
        }

        private void Scale(Vector3 amount) {
            transform.localScale = amount;
        }
    }
}
}