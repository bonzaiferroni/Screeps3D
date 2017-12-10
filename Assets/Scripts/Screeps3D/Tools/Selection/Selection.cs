using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Screeps3D.RoomObjects;
using Screeps3D.RoomObjects.Views;
using Screeps3D.Selection;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Screeps3D.Tools.Selection
{
    public class Selection : BaseSingleton<Selection>
    {
        [SerializeField] private GameObject _circlePrefab;
        [SerializeField] private GameObject _labelPrefab;

        public static GameObject CircleTemplate;
        public static GameObject LabelTemplate;

        private Vector3 _startPosition;
        private bool _isSelecting;
        private readonly Dictionary<string, RoomObject> _selections = new Dictionary<string, RoomObject>();

        public Action<RoomObject> OnSelect;
        public Action<RoomObject> OnDeselect;

        private void Start()
        {
            CircleTemplate = _circlePrefab;
            LabelTemplate = _labelPrefab;
        }

        private void Update()
        {
            var ctrlKey = Input.GetKey(KeyCode.LeftControl);
            var overUI = EventSystem.current.IsPointerOverGameObject();
            var dragging = IsDragging();

            ObjectView rayTarget = null;
            if (!dragging && !overUI)
            {
                rayTarget = Rayprobe();
            }
            SelectionOutline.DrawOutline(rayTarget);

            if (Input.GetMouseButtonDown(0) && !overUI)
            {
                _isSelecting = true;
                _startPosition = Input.mousePosition;
                if (!ctrlKey)
                    DeselectAll();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isSelecting = false;
                if (dragging)
                {
                    SelectBoxedObjects();
                } else if (!overUI && rayTarget)
                {
                    ToggleSelection(rayTarget.RoomObject);
                }
            }
        }

        private void OnGUI()
        {
            if (!IsDragging()) return; // Early
            SelectionBox.DrawSelectionBox(_startPosition, Input.mousePosition);
        }

        private bool IsDragging()
        {
            return _isSelecting && (Input.mousePosition - _startPosition).magnitude > 10;
        }

        private ObjectView Rayprobe()
        {
            RaycastHit hitInfo;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 100);
            var hit = Physics.Raycast(ray, out hitInfo);
            if (!hit) return null; // Early

            return hitInfo.transform.gameObject.GetComponent<ObjectView>();
        }

        public void ToggleSelection(RoomObject obj)
        {
            if (_selections.ContainsKey(obj.Id))
            {
                DeselectObject(obj);
            } else
            {
                SelectObject(obj);
            }
        }

        public void DeselectObject(RoomObject obj)
        {
            if (!_selections.ContainsKey(obj.Id)) return; // Early

            if (obj.View)
            {
                var selectionView = obj.View.GetComponent<SelectionView>();
                if (selectionView)
                {
                    selectionView.Dispose();
                }
            }

            _selections.Remove(obj.Id);
            if (OnDeselect != null)
                OnDeselect(obj);
        }

        private void DeselectAll()
        {
            _selections.Values.ToList().ForEach(obj => DeselectObject(obj));
        }

        private void SelectObject(RoomObject obj)
        {
            if (!_selections.ContainsKey(obj.Id))
            {
                _selections.Add(obj.Id, obj);
                if (obj.View)
                {
                    obj.View.gameObject.AddComponent<SelectionView>();
                }
                if (OnSelect != null)
                    OnSelect(obj);
            }
        }

        private void SelectBoxedObjects()
        {
            foreach (var kvp in ObjectManager.Instance.RoomObjects)
            {
                if (kvp.Value.View == null)
                    continue;
                var withinBounds = SelectionBox.IsWithinSelectionBox(kvp.Value.View);
                if (withinBounds)
                    SelectObject(kvp.Value);
            }
        }
    }
}