using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Screeps3D {
    public class TerrainViewer : MonoBehaviour {

        [SerializeField] private ScreepsAPI api;
        [SerializeField] private GameObject[] wallPrototypes;
        // [SerializeField] private GameObject plainsPrototype;
        [SerializeField] private GameObject swampPrototype;
        [SerializeField] private RoomChooser chooser;

        private List<GameObject> currentObjects = new List<GameObject>();
        
        public void Start() {
            chooser.OnChooseRoom += ViewRoom;
            
            // plainsPrototype.SetActive(false);
            swampPrototype.SetActive(false);
            foreach (var wall in wallPrototypes) {
                wall.SetActive(false);
            }
        }

        private void Callback(JSONObject obj) {
            try {
                var terrain = obj["terrain"].list[0]["terrain"].str;
                RenderRoom(terrain);
            } catch {
                Debug.Log("bad terrain data: " + obj);
            }
        }

        private void ViewRoom(string roomName) {
            api.Http.GetRoom(roomName, "shard0", Callback);
        }

        private void RenderRoom(string terrain) {
            ClearCurrent();
            Debug.Log("terrain: " + terrain);
            for (var x = 0; x < 50; x++) {
                for (var y = 0; y < 50; y++) {
                    var unit = terrain[x + y * 50];
                    if (unit == '0' || unit == '1') {
                        // RenderTerrain(x, y, TerrainType.Plains);
                    }
                    if (unit == '2' || unit == '3') {
                        RenderTerrain(x, y, TerrainType.Swamp);
                    }
                    if (unit == '1' || unit == '3') {
                        RenderTerrain(x, y, TerrainType.Wall);
                    }
                }
            }
        }

        private void ClearCurrent() {
            foreach (var go in currentObjects) {
                Destroy(go);
            }
            currentObjects.Clear();
        }

        private void RenderTerrain(int x, int y, TerrainType type) {
            var go = CloneTerrain(type);
            go.transform.position = new Vector3(-x, go.transform.position.y, y);
            go.SetActive(true);
            currentObjects.Add(go);
        }

        private GameObject CloneTerrain(TerrainType type) {
            GameObject prototype;
             if (type == TerrainType.Swamp) {
                prototype = swampPrototype;
            } else if (type == TerrainType.Wall) {
                prototype = wallPrototypes[(int) (wallPrototypes.Length * Random.value)];
                prototype.transform.rotation = prototype.transform.rotation *
                                               Quaternion.Euler(0, 90 * (int) (Random.value * 4), 0);
            } else {
                throw new Exception("invalid terrain type: " + type);
            }
            var go = Instantiate(prototype);
            go.transform.SetParent(prototype.transform.parent);
            return go;
        }
    }

    public enum TerrainType {
        Plains,
        Wall,
        Swamp,
    }
}