using UnityEngine;

namespace Screeps3D
{
    public class ControllerView : MonoBehaviour, IScreepsComponent
    {
        [SerializeField] private Renderer rend;
        private Texture2D texture;
        private Color controllerWhite;
        private Controller controller;

        public void Init(RoomObject roomObject)
        {
            if (!texture)
            {
                InitTexture();
            }
            controller = roomObject as Controller;
            UpdateTexture(roomObject.Data);
        }

        private void InitTexture()
        {
            texture = new Texture2D(8, 1);
            texture.filterMode = FilterMode.Point;
            rend.materials[0].mainTexture = texture;
            ColorUtility.TryParseHtmlString("#FDF5E6", out controllerWhite);
        }

        public void Delta(JSONObject data)
        {
            UpdateTexture(data);
        }

        private void UpdateTexture(JSONObject data)
        {
            if (data["level"] == null)
                return;
            var level = 0;
            for (var i = 0; i < 8; i++)
            {
                if (level < controller.Level)
                {
                    texture.SetPixel(i, 1, controllerWhite);
                } else
                {
                    texture.SetPixel(i, 1, Color.black);
                }
                level++;
            }
            texture.Apply();
        }
    }
}