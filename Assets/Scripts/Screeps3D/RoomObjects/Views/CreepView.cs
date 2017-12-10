using UnityEngine;

namespace Screeps3D.RoomObjects.Views
{
    internal class CreepView : ObjectView
    {
        [SerializeField] private Renderer _body;
        [SerializeField] private Transform _rotationRoot;

        private Quaternion _rotTarget;
        private Vector3 _posTarget;
        private Vector3 _posRef;
        private Creep _creep;

        internal override void Load(RoomObject roomObject)
        {
            base.Load(roomObject);
            _creep = roomObject as Creep;
            _body.material.mainTexture = _creep.Owner.badge;

            _rotTarget = transform.rotation;
            _posTarget = roomObject.Position;
        }

        internal override void Delta(JSONObject data)
        {
            base.Delta(data);

            var posDelta = _posTarget - RoomObject.Position;

            if (posDelta.sqrMagnitude > .01)
            {
                _posTarget = RoomObject.Position;
            } 
        }

        private void Update()
        {
            if (_creep == null)
                return;
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _posTarget, ref _posRef, .5f);
            _rotationRoot.transform.rotation = Quaternion.Slerp(_rotationRoot.transform.rotation, _creep.Rotation, 
                Time.deltaTime * 5);
        }
    }
}