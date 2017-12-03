using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Screeps3D
{
    public class TerrainFactory : MonoBehaviour
    {
        [SerializeField] private GameObject _wallPrototype;
        [SerializeField] private GameObject _swampPrototype;

        public void Start()
        {
            // plainsPrototype.SetActive(false);
            _swampPrototype.SetActive(false);
            _wallPrototype.SetActive(false);
        }

        public GameObject Get(TerrainType type)
        {
            GameObject prototype;
            if (type == TerrainType.Swamp)
            {
                prototype = _swampPrototype;
            } else if (type == TerrainType.Wall)
            {
                prototype = _wallPrototype;
            } else
            {
                throw new Exception("invalid terrain type: " + type);
            }
            var go = Instantiate(prototype);
            return go;
        }
    }

    public enum TerrainType
    {
        Plains,
        Wall,
        Swamp,
    }
}