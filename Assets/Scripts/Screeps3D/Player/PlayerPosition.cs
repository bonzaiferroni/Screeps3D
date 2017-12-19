using UnityEngine;

namespace Screeps3D.Player
{
    public class PlayerPosition : MonoBehaviour
    {
        private int _xPos;
        private int _yPos;
        private int _shardLevel;

        private void Update()
        {
            var xPos = (int) transform.position.x / 50;
            var yPos = (int) transform.position.z / 50;
            var shardLevel = (int) (transform.position.y / Constants.ShardHeight);
            if (_xPos == xPos && _yPos == yPos && shardLevel == _shardLevel)
                return;
            _xPos = xPos;
            _yPos = yPos;
            _shardLevel = shardLevel;
        }
    }
}