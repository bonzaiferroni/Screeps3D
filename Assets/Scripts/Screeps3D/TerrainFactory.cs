using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Screeps3D
{
    public class TerrainFactory : MonoBehaviour
    {
        [SerializeField] private GameObject wallPrototype;
        [SerializeField] private GameObject swampPrototype;

        public void Start()
        {
            // plainsPrototype.SetActive(false);
            swampPrototype.SetActive(false);
            wallPrototype.SetActive(false);
        }

        public GameObject Get(TerrainType type)
        {
            GameObject prototype;
            if (type == TerrainType.Swamp)
            {
                prototype = swampPrototype;
            } else if (type == TerrainType.Wall)
            {
                prototype = wallPrototype;
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