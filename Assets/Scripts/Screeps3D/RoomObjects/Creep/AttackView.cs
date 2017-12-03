using UnityEngine;

namespace Screeps3D
{
    public class AttackView : CreepPartView
    {
        [SerializeField] private Animator _anim;

        public override void Init(RoomObject roomObject)
        {
            base.Init(roomObject);
            AdjustSize("attack", .5f, .5f);
        }

        public override void Delta(JSONObject delta)
        {
            AdjustSize("attack", .5f, .5f);

            if (creep.Actions.ContainsKey("attack") && !creep.Actions["attack"].IsNull)
            {
                var rotation = Quaternion.LookRotation(GetActionVector(creep.Actions["attack"]));
                view.rotTarget = rotation;
                _anim.SetTrigger("activate");
            }
        }
    }
}