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

        internal override void Init(RoomObject roomObject)
        {
            base.Init(roomObject);
            _creep = roomObject as Creep;
            _body.material.mainTexture = _creep.Owner.badge;

            rotTarget = transform.rotation;
            posTarget = new Vector3(RoomObject.X, transform.localPosition.y, 49 - RoomObject.Y);;
            transform.localPosition = posTarget;
        }

        internal override void Delta(JSONObject data)
        {
            base.Delta(data);

            var newPos = new Vector3(RoomObject.X, transform.localPosition.y, 49 - RoomObject.Y);
            var posDelta = posTarget - newPos;

            if (posDelta.sqrMagnitude > 100)
            {
                posTarget = newPos;
            } else if (posDelta.sqrMagnitude > .01)
            {
                posTarget = newPos;
                rotTarget = Quaternion.LookRotation(posDelta);
            } 
        }

        private void Update()
        {
            transform.localPosition = Vector3.SmoothDamp(transform.localPosition, posTarget, ref posRef, .5f);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotTarget, Time.deltaTime * 5);
        }

        protected override void OnFinishedAnimation(bool isVisible)
        {
            // do nothing
        }

        internal override void Show()
        {
            if (!_vis.IsVisible)
            {
                transform.localPosition = PosToVector3();
            }
            _vis.Show();
        }
    }
}