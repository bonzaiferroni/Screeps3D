using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;

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

		private void DeselectObject(Object gameObj)
		{
			var id = gameObj.GetInstanceID();
			if (!_selections.ContainsKey(id)) return; // Early
			_selections[id].Dispose();
			_selections.Remove(id);
		}

		private void DeselectAll()
		{
			_selections.Values.ToList().ForEach(s => DeselectObject(s.gameObject));
		}

		private void RaycastToggle()
		{
			RaycastHit hitInfo;
			var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (!hit) return; // Early

			var obj = hitInfo.transform.gameObject;
			if (obj.GetComponent<ObjectView>() == null) return; // Early
			
			if (obj.gameObject.GetComponent<SelectionView>() == null)
				SelectObject(obj.gameObject);
			else
				DeselectObject(obj.gameObject);
		}

		private void SelectObject(GameObject gameObj)
		{
			if (!_selections.ContainsKey(gameObj.GetInstanceID()))
				_selections.Add(gameObj.GetInstanceID(),
					gameObj.AddComponent<SelectionView>());
		}

		private void SelectBoxedObjects()
		{
			foreach (var objectView in ObjectManager.Instance.GetViews())
			{
				var withinBounds = SelectionBox.IsWithinSelectionBox(objectView.gameObject);
				if (withinBounds)
					SelectObject(objectView.gameObject);
						
			}
		}
	}
}

