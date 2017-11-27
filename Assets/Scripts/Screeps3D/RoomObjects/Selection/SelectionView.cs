using TMPro;
using UnityEngine;

namespace Screeps3D.Ui
{
    [DisallowMultipleComponent]
    internal class SelectionView : MonoBehaviour
    {
        public static GameObject CircleTemplate;
        public static GameObject LabelTemplate;

        public string Type;
        private ObjectView _objectView;
        private GameObject _circle;
        private GameObject _label;

        private void Start()
        {
            _objectView = gameObject.GetComponent<ObjectView>();
            Type = _objectView.RoomObject.Type;
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

            if (labelText == "extension" || labelText == "constructedWall" || labelText == "rampart")
                return null;

            var label = Instantiate(LabelTemplate, _objectView.gameObject.transform);
            var tmp = label.GetComponent<TextMeshPro>();
            tmp.text = labelText;
            var y = label.gameObject.transform.lossyScale.y + 1;
            label.transform.localPosition = new Vector3(0, y, 0);
            tmp.enabled = true;
            return label;
        }

        private void OnGUI()
        {
            if (_label != null) _label.transform.rotation = Camera.main.transform.rotation;
        }

        private GameObject CreateCircle()
        {
            return Instantiate(CircleTemplate, _objectView.gameObject.transform);
        }
    }
}