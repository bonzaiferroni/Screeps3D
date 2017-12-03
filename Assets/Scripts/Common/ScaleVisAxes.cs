namespace Utils
{
    using UnityEngine;

    namespace Utils
    {
        public class ScaleVisAxes : MonoBehaviour, IVisibilityControl
        {
            [SerializeField] private bool _x = true;
            [SerializeField] private bool _y = true;
            [SerializeField] private bool _z = true;
            [SerializeField] private bool _animateOnStart = true;
            [SerializeField] private bool _visibleOnStart = true;
            [SerializeField] private float _speed = .2f;

            public bool IsVisible { get; private set; }

            private Vector3 _current;
            private Vector3 _target;
            private Vector3 _targetRef;

            private void Start()
            {
                if (_animateOnStart)
                {
                    Visible(0, true);
                    Visible(1);
                }
                if (_visibleOnStart)
                {
                    Visible(1, true);
                }
            }

            public void Visible(bool shown, bool instant = false)
            {
                var target = shown ? 1 : 0;
                Visible(target, instant);
            }

            public void Visible(float target, bool instant = false)
            {
                enabled = true;
                IsVisible = true;

                if (float.IsNaN(target))
                {
                    target = 0;
                }

                this._target = transform.localScale;
                if (_x)
                {
                    this._target.x = target;
                }
                if (_y)
                {
                    this._target.y = target;
                }
                if (_z)
                {
                    this._target.z = target;
                }
                if (instant)
                {
                    _current = this._target;
                }
            }

            public void Show(bool instant = false)
            {
                Visible(true, instant);
            }

            public void Hide(bool instant = false)
            {
                Visible(false, instant);
            }

            public void Toggle(bool instant = false)
            {
                Visible(!IsVisible, instant);
            }

            public void Update()
            {
                if (Vector3.Distance(_current, _target) < .0001f)
                {
                    enabled = false;
                }

                _current = Vector3.SmoothDamp(_current, _target, ref _targetRef, _speed);
                Scale(_current);
            }

            private void Scale(Vector3 amount)
            {
                transform.localScale = amount;
            }
        }
    }
}