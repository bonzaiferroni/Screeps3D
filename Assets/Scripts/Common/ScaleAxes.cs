using UnityEngine;

namespace Common
{
    public class ScaleAxes : ScaleVisibility
    {
        [SerializeField] private bool _x = true;
        [SerializeField] private bool _y = true;
        [SerializeField] private bool _z = true;

        protected override void Scale(float amount)
        {
            var scale = Vector3.zero;
            if (amount > 0)
                scale = new Vector3(_x ? amount : 1, _y ? amount : 1, _z ? amount : 1);
            transform.localScale = scale;
        }
    }
}