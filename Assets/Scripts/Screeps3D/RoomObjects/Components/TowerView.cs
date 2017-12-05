using System.Collections;
using UnityEngine;
using Utils.Utils;

namespace Screeps3D
{
    public class TowerView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private ScaleVisAxes _energyDisplay;
        [SerializeField] private Transform _rotationRoot;
        private Quaternion _targetRot;
        private float _nextRot;
        private bool _rotating;

        private IEnergyObject energyObject;
        private float targetRef;

        public void Init(RoomObject roomObject)
        {
            energyObject = roomObject as IEnergyObject;
            AdjustScale();
        }

        public void Delta(JSONObject data)
        {
            AdjustScale();
        }

        private void AdjustScale()
        {
            _energyDisplay.Visible(energyObject.Energy / energyObject.EnergyCapacity);
        }

        private void Update()
        {
            if (!_rotating && Time.time > _nextRot)
                StartCoroutine(Rotate());

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