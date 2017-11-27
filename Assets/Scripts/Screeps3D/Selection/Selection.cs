using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;
using Utils;

namespace Screeps3D.RoomObjects.Selection
{
	public class Selection : BaseSingleton<Selection>
	{
		[SerializeField] public GameObject CirclePrefab;
		[SerializeField] public GameObject LabelPrefab;
		
		private Vector3 _startPosition;
		private bool _isSelecting;
		private readonly Dictionary<int, SelectionView> _selections = new Dictionary<int, SelectionView>();

		private void Start()
		{
			SelectionView.CircleTemplate = CirclePrefab;
			SelectionView.LabelTemplate = LabelPrefab;
			
		}

		private void Update()
		{
			var ctrl = Input.GetKey(KeyCode.LeftControl);
		
			if (Input.GetMouseButtonDown(0))
			{
				_isSelecting = true;
				_startPosition = Input.mousePosition;
				if (!ctrl)
					DeselectAll();
			}

			if (Input.GetMouseButtonUp(0))
			{
				_isSelecting = false;
				var dragged = IsDragging();
				if (ctrl && !dragged)
				{
					var obj = HitViewObject();
					if (obj == null) return; // Early
					if (obj.gameObject.GetComponent<SelectionView>() == null)
						SelectObject(obj.gameObject);
					else
						DeselectObject(obj.gameObject);
				}
				else if (dragged)
				{
					SelectObjects();
				}
			}
		}

		private void OnGUI()
		{
			if (!_isSelecting || !IsDragging()) return; // Early
			SelectionBox.DrawSelectionBox(_startPosition, Input.mousePosition);
		}

		private GameObject HitViewObject()
		{
			RaycastHit hitInfo;
			var hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
			if (!hit) return null; // Early
			var obj = hitInfo.transform.gameObject;
			return obj.GetComponent<ObjectView>() != null ? obj : null;
		}

		private bool IsDragging()
		{
			var offset = Input.mousePosition - _startPosition;
			return offset.magnitude > 10;
		}

		private void DeselectObject(GameObject gameObject)
		{
			var id = gameObject.GetInstanceID();
			if (!_selections.ContainsKey(id)) return; // Early
			_selections[id].Dispose();
			_selections.Remove(id);
		}

		private void DeselectAll()
		{
			_selections.Values.ToList().ForEach(s => DeselectObject(s.gameObject));
		}

		private void SelectObject(GameObject gameObject)
		{
			if (!_selections.ContainsKey(gameObject.GetInstanceID()))
				_selections.Add(gameObject.GetInstanceID(),
					gameObject.AddComponent<SelectionView>());
		}

		private void SelectObjects()
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

