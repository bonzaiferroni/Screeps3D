using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Screeps3D.Selection
{
    [DisallowMultipleComponent]
    internal class SelectionView : MonoBehaviour
    {
        private static readonly Dictionary<string, float> CircleSizes = new Dictionary<string, float>
        {
            // Prefab default 0.75
            {"extension", 0.5f}
        };

        private string _type;
        private GameObject _circle;
        private GameObject _label;
        private Stack<GameObject> _labelPool = new Stack<GameObject>();
        private Stack<GameObject> _circlePool = new Stack<GameObject>();
        private Projector _projector;

        public ObjectView Selected { get; private set; }

        private void Start()
        {
            Selected = gameObject.GetComponent<ObjectView>();
            _type = Selected.RoomObject.Type;
            _circle = CreateCircle();
            _label = CreateLabel();
        }

        public void Dispose()
        {
            if (_circle != null)
            {
                _circle.SetActive(false);
                _circlePool.Push(_circle);
            }
            ;
            if (_label != null)
            {
                _label.SetActive(false);
                _labelPool.Push(_label);
            }
            Destroy(this);
        }

        private GameObject CreateLabel()
        {
            var nameObj = Selected.RoomObject as INamedObject;
            if (nameObj == null)
                return null; // Early

            GameObject label;
            if (_labelPool.Count > 0)
            {
                label = _labelPool.Pop();
                label.SetActive(true);
            } else
            {
                label = Instantiate(Selection.LabelTemplate);
            }
            label.transform.SetParent(Selected.gameObject.transform);
            label.transform.localPosition = new Vector3(0, label.gameObject.transform.lossyScale.y + 1, 0);
            var textMesh = label.GetComponent<TextMeshPro>();
            textMesh.text = nameObj.Name;
            textMesh.enabled = true;
            return label;
        }

        private void BillboardLabel()
        {
            _label.transform.rotation = Camera.main.transform.rotation;
        }

        private void Update()
        {
            if (_label != null) BillboardLabel();
            if (_circle != null) FadeInCircle();
        }

        private void FadeInCircle()
        {
            var color = _projector.material.color;
            if (color.a >= 1)
            {
                return;
            }
            color.a += Time.deltaTime / .2f;
            _projector.material.color = color;
        }

        private GameObject CreateCircle()
        {
            GameObject go;
            if (_circlePool.Count > 0)
            {
                go = _circlePool.Pop();
                go.SetActive(true);
            } 
            else
            {
                go = Instantiate(Selection.CircleTemplate);
            }
            _projector = go.GetComponent<Projector>();
            var color = _projector.material.color;
            color.a = 0;
            _projector.material.color = color;
            go.transform.SetParent(Selected.gameObject.transform, false);
            if (CircleSizes.ContainsKey(_type))
                _projector.orthographicSize = CircleSizes[_type];
            return go;
        }
    }
}