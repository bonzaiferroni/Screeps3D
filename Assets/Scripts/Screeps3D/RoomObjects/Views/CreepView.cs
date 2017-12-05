using UnityEngine;

namespace Screeps3D
{
    internal class CreepView : ObjectView
    {
        [SerializeField] private Renderer _body;

        public Quaternion rotTarget;
        private Vector3 posTarget;
        private Vector3 posRef;
        private Creep _creep;

        internal override void Load(RoomObject roomObject)
        {
            base.Load(roomObject);
            _creep = roomObject as Creep;
            _body.material.mainTexture = _creep.Owner.badge;

            rotTarget = transform.rotation;
            posTarget = roomObject.Position;
        }

        internal override void Delta(JSONObject data)
        {
            base.Delta(data);

            var posDelta = posTarget - RoomObject.Position;

            if (posDelta.sqrMagnitude > .01)
            {
                posTarget = RoomObject.Position;
                rotTarget = Quaternion.LookRotation(posDelta);
            } 
        }

        private void Update()
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, posTarget, ref posRef, .5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * 5);
        }
    }
}