using UnityEngine;

namespace Screeps3D
{
    public class CreepPartView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private Transform _partDisplay;

        internal Creep creep;
        internal CreepView view;

        public virtual void Init(RoomObject roomObject)
        {
            creep = roomObject as Creep;
            view = creep.View as CreepView;
        }

        public virtual void Delta(JSONObject data)
        {
        }

        protected void AdjustSize(string partType, float min, float flex)
        {
            var amount = 0f;
            foreach (var part in creep.Body.Parts)
            {
                if (part.type != partType)
                    continue;
                amount += part.hits;
            }

            var scaleAmount = 0f;
            if (amount > 0)
            {
                scaleAmount = (amount / 5000) * flex + min;
            }

            _partDisplay.transform.localScale = Vector3.one * scaleAmount;
        }

        protected Vector3 GetActionVector(JSONObject data)
        {
            return new Vector3(data["x"].n - creep.X, 0, creep.Y - data["y"].n);
        }
    }
}