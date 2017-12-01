using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Screeps3D.Selection
{
	public class Selection : BaseSingleton<Selection>
	{
		[SerializeField] private GameObject _circlePrefab;
		[SerializeField] private GameObject _labelPrefab;

		public static GameObject CircleTemplate;
		public static GameObject LabelTemplate;
		
		private Vector3 _startPosition;
		private bool _isSelecting;
		private readonly Dictionary<int, SelectionView> _selections = new Dictionary<int, SelectionView>();

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
		
			if (Input.GetMouseButtonDown(0))
			{
				_isSelecting = true;
				_startPosition = Input.mousePosition;
				if (!ctrlKey)
					DeselectAll();
			}

			if (Input.GetMouseButtonUp(0))
			{
				_isSelecting = false;
				if (BoxSelection())
					SelectBoxedObjects();
				else if (ctrlKey)
					RaycastToggle();
			}
		}

		private void OnGUI()
		{
			if (!_isSelecting || !BoxSelection()) return; // Early
			SelectionBox.DrawSelectionBox(_startPosition, Input.mousePosition);
		}

		private bool BoxSelection()
		{
			var offset = Input.mousePosition - _startPosition;
			return offset.magnitude > 10;
		}

		private void DeselectObject(ObjectView view)
		{
			var id = view.GetInstanceID();
			if (!_selections.ContainsKey(id)) return; // Early
			_selections[id].Dispose();
			_selections.Remove(id);
		}

		private void DeselectAll()
		{
			_selections.Values.ToList().ForEach(s => DeselectObject(s.Selected));
		}

		private void RaycastToggle()
		{
			RaycastHit hitInfo;
			var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (!hit) return; // Early

			var view = hitInfo.transform.gameObject.GetComponent<ObjectView>();
			if (!view) return; // Early
			
			if (view.GetComponent<SelectionView>() == null)
				SelectObject(view);
			else
				DeselectObject(view);
		}

		private void SelectObject(ObjectView view)
		{
			if (!_selections.ContainsKey(view.GetInstanceID()))
				_selections.Add(view.GetInstanceID(),
					view.gameObject.AddComponent<SelectionView>());
		}

		private void SelectBoxedObjects()
		{
			foreach (var objectView in ObjectManager.Instance.GetViews())
			{
				var withinBounds = SelectionBox.IsWithinSelectionBox(objectView.gameObject);
				if (withinBounds)
					SelectObject(objectView);
			}
		}
	}
}

