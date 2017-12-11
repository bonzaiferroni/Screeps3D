using UnityEngine;

namespace Common
{
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

            private float _current;
            private float _target;
            private float _targetRef;

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

                _target = target;
                
                if (instant)
                {
                    _current = _target;
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
                if (Mathf.Abs(_current - _target) < .0001f)
                {
                    if (_target == 0)
                    {
                        Scale(Vector3.zero);
                    }
                    else
                    {
                        Scale(_target);
                    }
                    enabled = false;
                    return;
                }

                _current = Mathf.SmoothDamp(_current, _target, ref _targetRef, _speed);
                Scale(_current);
            }

            private void Scale(float amount)
            {
                Scale(new Vector3(_x ? amount : 1, _y ? amount : 1, _z ? amount : 1));
            }

            private void Scale(Vector3 amount)
            {
                transform.localScale = amount;
            }
        }
    }
}