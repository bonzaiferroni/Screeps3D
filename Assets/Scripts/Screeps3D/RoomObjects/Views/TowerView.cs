using System.Collections;
using System.Linq;
using Common.Utils;
using Screeps3D.Effects;
using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    public class TowerView : MonoBehaviour, IObjectViewComponent
    {
        [SerializeField] private ScaleVisAxes _energyDisplay;
        [SerializeField] private Transform _rotationRoot;
        private Quaternion _targetRot;
        private bool _idle;
        private float _nextRot;
        private bool _rotating;
        private Tower _tower;
        private LineRenderer _lineRenderer;
        private IEnumerator _rotator;

        public void Init()
        {
            _lineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        public void Load(RoomObject roomObject)
        {
            _tower = roomObject as Tower;
            AdjustScale();
        }

        public void Delta(JSONObject data)
        {
            AdjustScale();

            var action = _tower.Actions.FirstOrDefault(c => !c.Value.IsNull);
            if (action.Value == null)
            {
                _idle = true;
                return; // Early
            }
            _idle = false;
            if (_rotator != null) StopCoroutine(_rotator);

            var endPos = PosUtility.Convert(action.Value, _tower.Room);
            _rotationRoot.rotation = Quaternion.LookRotation(endPos - _tower.Position);
            var color = action.Key == "attack" ? Color.blue : action.Key == "heal" ? Color.green : Color.yellow;
            EffectsUtility2.Beam(_tower, action.Value, new BeamConfig(color, 0.6f, 0.3f));
            // StartCoroutine(Beam.Draw(_tower, action.Value, _lineRenderer, new BeamConfig(color, 0.6f, 0.3f)));
        }

        public void Unload(RoomObject roomObject)
        {
        }

        private void AdjustScale()
        {
            _energyDisplay.Visible(_tower.Energy / _tower.EnergyCapacity);
        }

        private void Update()
        {
            if (!_idle || _rotating || !(Time.time > _nextRot)) return; // Early
            
            _rotator = Rotate();
            StartCoroutine(_rotator);
        }

        private IEnumerator Rotate()
        {
            var direction = Random.value > 0.5 ? 1 : -1;
            _targetRot = _rotationRoot.rotation * Quaternion.Euler(0, 180 * Random.value * direction, 0);
            _rotating = true;
            while (_rotationRoot.rotation != _targetRot)
            {
                _rotationRoot.rotation = Quaternion.Slerp(_rotationRoot.rotation, _targetRot, Time.deltaTime);
                yield return null;
            }
            _nextRot = Time.time + Random.value + 1;
            _rotating = false;
        }

    }
}