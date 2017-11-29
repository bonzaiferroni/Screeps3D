using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Screeps3D.Selection
{
    [DisallowMultipleComponent]
    internal class SelectionView : MonoBehaviour
    {
        private static readonly string[] NoLabel = {"rampart", "constructedWall", "extension"};
        private static readonly Dictionary<string, float> CircleSizes = new Dictionary<string, float>
        {   // Prefab default 0.75
            {"extension", 0.5f}
        };

        private string _type;
        private ObjectView _objectView;
        private GameObject _circle;
        private GameObject _label;


        private void Start()
        {
            _objectView = gameObject.GetComponent<ObjectView>();
            _type = _objectView.RoomObject.Type;
            _circle = CreateCircle();
            _label = CreateLabel();
            
        }

        public void Dispose()
        {
            if (_circle != null) Destroy(_circle);
            if (_label != null) Destroy(_label);
            Destroy(this);
        }

        private GameObject CreateLabel()
        {
            var roomObject = _objectView.RoomObject;
            var labelText = _objectView is CreepView
                ? ((Creep) roomObject).Name
                : roomObject.Type;

            if (NoLabel.Contains(_type))
                return null; // Early

            var label = Instantiate(Selection.LabelTemplate, _objectView.gameObject.transform);
            label.transform.localPosition = new Vector3(0, label.gameObject.transform.lossyScale.y + 1, 0);
            var textMesh = label.GetComponent<TextMeshPro>();
            textMesh.text = labelText;
            textMesh.enabled = true;
            return label;
        }

        private void OnGUI()
        {
            if (_label != null) _label.transform.rotation = Camera.main.transform.rotation;
        }

        private GameObject CreateCircle()
        {
            var go = Instantiate(Selection.CircleTemplate, _objectView.gameObject.transform);
            if (CircleSizes.ContainsKey(_type))
                go.GetComponent<Projector>().orthographicSize = CircleSizes[_type];
            return go;
        }
    }
}