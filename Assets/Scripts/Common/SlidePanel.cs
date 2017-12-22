using System;
using System.Collections;
using UnityEngine;

namespace Common
{
    public class SlidePanel : FadePanel
    {
        [SerializeField] private float _offset = 30;
        [SerializeField] private Vector2 _direction = Vector2.down;

        private Vector2 _defaultPos;
        private RectTransform _rect;

        protected override void Awake()
        {
            base.Awake();
            _rect = transform as RectTransform;
            // ReSharper disable once PossibleNullReferenceException
            _defaultPos = _rect.anchoredPosition;
        }

        protected override void Modify(float amount)
        {
            base.Modify(amount);
            _rect.anchoredPosition = _defaultPos + _direction * (1 - amount) * _offset;
        }
    }
}