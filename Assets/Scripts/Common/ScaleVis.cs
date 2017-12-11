using System;
using Screeps3D.RoomObjects.Views;
using UnityEngine;

namespace Common
{
    public class ScaleVis : MonoBehaviour, IVisibilityControl
    {
        [SerializeField] private bool _animateOnStart = true;
        [SerializeField] private bool _visibleOnStart = true;
        [SerializeField] private float _speed = .2f;

        public bool IsVisible { get; private set; }

        public Action<bool> OnFinishedAnimation;

        private float _current;
        private float _target = 1;
        private float _targetRef;

        private void Start()
        {
            if (_animateOnStart)
            {
                Scale(0);
            }
            if (!IsVisible)
            {
                Visible(_visibleOnStart, !_animateOnStart);
            }
        }

        public void Visible(bool show, bool instant = false)
        {
            var target = show ? 1 : 0;
            Visible(target, instant);
        }

        public void Visible(float target, bool instant = false)
        {
            enabled = true;
            IsVisible = target > 0;

            if (float.IsNaN(target))
            {
                target = 0;
            }

            this._target = target;

            if (instant)
            {
                _current = target;
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
                if (OnFinishedAnimation != null)
                    OnFinishedAnimation(IsVisible);
                enabled = false;
            }

            _current = Mathf.SmoothDamp(_current, _target, ref _targetRef, _speed);
            Scale(_current);
        }

        private void Scale(float amount)
        {
            transform.localScale = Vector3.one * amount;
        }
    }
}