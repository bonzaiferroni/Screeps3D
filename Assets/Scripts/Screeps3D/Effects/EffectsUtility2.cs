using Common;
using Screeps3D.RoomObjects;
using UnityEngine;

namespace Screeps3D.Effects
{
    public static class EffectsUtility2
    {
        public static void Beam(RoomObject origin, JSONObject target, BeamConfig config)
        {
            var startPos = origin.View.transform.position + new Vector3(0, config.StartHeight, 0);
            var endPos = PosUtility.Convert(target, origin.Room) + new Vector3(0, config.EndHeight, 0);
            Beam(startPos, endPos, config.Color);
        }
        
        public static void Beam(Vector3 startPos, Vector3 endPos, Color color)
        {
            var go = PoolLoader.Load(BeamEffect.PATH);
            var effect = go.GetComponent<BeamEffect>();
            effect.Load(startPos, endPos, color);
        }
    }
}