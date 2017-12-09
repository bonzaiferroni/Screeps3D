using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D
{
    public static class Beam
    {
        private static readonly HashSet<string> CurrentBeams = new HashSet<string>();
        private const float BeamDuration = 1;
        private const float HalfDuration = BeamDuration / 2;
        public static IEnumerator Draw(RoomObject origin, JSONObject target, LineRenderer lineRenderer, BeamConfig config)
        {
            var id = origin.Id;
            if (CurrentBeams.Contains(id)) yield break; // Early
            CurrentBeams.Add(id);

            var endPos = PosUtility.Convert(target, origin.Room) + new Vector3(0, config.EndHeight, 0);
            var startPos = origin.View.transform.position + new Vector3(0, config.StartHeight, 0);
            var time = 0f;
            lineRenderer.startColor = config.Color;
            lineRenderer.endColor = config.Color;
            lineRenderer.SetPosition(0, startPos);
            lineRenderer.enabled = true;
            while (time < HalfDuration)
            {
                var factor = time / HalfDuration;
                var point = (endPos - startPos) * factor + startPos;
                time += Time.unscaledDeltaTime;
                lineRenderer.SetPosition(1, point);
                yield return null;
            }
            lineRenderer.SetPosition(1, endPos);
            while (time < BeamDuration)
            {
                var factor = (time - HalfDuration) / HalfDuration;
                var point = (endPos - startPos) * factor + startPos;
                time += Time.unscaledDeltaTime;
                lineRenderer.SetPosition(0, point);
                yield return null;
            }
            lineRenderer.enabled = false;
            CurrentBeams.Remove(id);
        }
    }
    
    public class BeamConfig
    {
        public Color Color { get; private set; }
        public float StartHeight { get; private set; }
        public float EndHeight { get; private set; }

        public BeamConfig(Color color, float startHeight, float endHeight)
        {
            Color = color;
            StartHeight = startHeight;
            EndHeight = endHeight;
        }
    }
}