using System.Collections;
using Common;
using Screeps3D.RoomObjects;
using TMPro;
using UnityEngine;

namespace Screeps3D.Effects
{
    public class SpeechEffect : MonoBehaviour
    {
        public const string PATH = "Prefabs/Effects/SpeechEffect";
        
        private const float SpeechDuration = 1;

        [SerializeField] private TextMeshPro _label;
        private float _time;
        private Vector3 _endPos;
        private Vector3 _startPos;

        public void Load(RoomObject creep, string message)
        {
            if (creep.View == null) return;
            
            _startPos = creep.View.transform.position + new Vector3(0, 2, 0);
            gameObject.transform.position = _startPos;
            _endPos = creep.View.transform.position + new Vector3(0, 4, 0);
            _label.enabled = false;
            _time = 0f;
            _label.text = message;
            StartCoroutine(Display());
        }

        private IEnumerator Display()
        {
            _label.enabled = true;
            
            while (_time < SpeechDuration)
            {
                var factor = _time / SpeechDuration;
                var point = (_endPos - _startPos) * factor + _startPos;
                _label.transform.position = point;
                _time += Time.unscaledDeltaTime;
                yield return null;
            }
            
            _label.enabled = false;
            PoolLoader.Return(PATH, gameObject);
        }
    }
}