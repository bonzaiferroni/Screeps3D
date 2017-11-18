using System.Collections.Generic;
using UnityEngine;

namespace Screeps3D {
    public class WorldView : MonoBehaviour {

        private const int viewDistance = 2;
        
        [SerializeField] private RoomView roomPrototype;
        [SerializeField] private RoomChooser chooser;
        [SerializeField] private PlayerGaze playerGaze;
        
        private Dictionary<string, RoomView> visibleRooms = new Dictionary<string,RoomView>();

        private void Start() {
            chooser.OnChooseRoom += OnChoose;
            roomPrototype.gameObject.SetActive(false);
        }

        private void OnChoose(WorldCoord coord) {
            GenerateRoom(coord);
            TransportPlayer(coord.vector);
        }

        private void GenerateRoom(WorldCoord coord) {
            if (visibleRooms.ContainsKey(coord.key)) {
                return;
            }

            var view = Instantiate(roomPrototype.gameObject).GetComponent<RoomView>();
            view.gameObject.SetActive(true);
            view.transform.SetParent(transform);
            view.gameObject.name = coord.key;
            visibleRooms[coord.key] = view;
            view.transform.localPosition = coord.vector;
            view.Load(coord);
        }

        private void TransportPlayer(Vector3 pos) {
            playerGaze.transform.position = new Vector3(pos.x + 25, pos.y, pos.z + 25);
        }

        public void LoadNeighbors(WorldCoord coord) {
            for (var xDelta = -viewDistance; xDelta <= viewDistance; xDelta++) {
                for (var yDelta = -viewDistance; yDelta <= viewDistance; yDelta++) {
                    if (xDelta == 0 && yDelta == 0) continue;
                    var relativeCoord = coord.Relative(xDelta, yDelta);
                    GenerateRoom(relativeCoord);
                }
            }
        }
    }
}