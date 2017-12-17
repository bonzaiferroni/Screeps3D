using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class VerticalPanelGroup : MonoBehaviour
    {
        [SerializeField] private VerticalPanelElement _element;
        [SerializeField] private float _spacing;
        [SerializeField] private float _bottomPadding;
        
        private List<VerticalPanelElement> _elements = new List<VerticalPanelElement>();
        private bool _updateGeometry;
        private RectTransform _rect;

        public List<VerticalPanelElement> Elements { get { return _elements;  }}

        public RectTransform Rect
        {
            get
            {
                if (!_rect)
                    _rect = GetComponent<RectTransform>();
                return _rect;
            }
        }

        public void AddElement(VerticalPanelElement element)
        {
            element.transform.SetParent(transform, false);
            element.Group = this;
            Elements.Add(element);
            _updateGeometry = true;
        }

        public void RemoveElement(VerticalPanelElement element)
        {
            Elements.Remove(element);
            _updateGeometry = true;
        }

        public void ClearElements()
        {
            Elements.Clear();
        }

        public void UpdateGeometry()
        {
            _updateGeometry = true;
        }

        private void Update()
        {
            if (!_updateGeometry)
                return;
            _updateGeometry = false;

            var height = 0f;
            foreach (var element in _elements)
            {
                if (!element.IsVisible)
                    continue;
                element.TargetPos = -height;
                height += element.Height + _spacing;
            }
            height += _bottomPadding;

            Rect.sizeDelta = Vector2.up * height;
            
            if (_element)
            {
                _element.Height = height;
            }
        }
    }
}