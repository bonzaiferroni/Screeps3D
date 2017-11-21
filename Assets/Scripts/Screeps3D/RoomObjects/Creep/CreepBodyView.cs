using UnityEngine;

namespace Screeps3D {
    public class CreepBodyView : MonoBehaviour, IScreepsComponent {

        [SerializeField] private Renderer rend;
        private Creep creep;
        private Texture2D texture;

        public void Init(RoomObject roomObject) {
            texture = new Texture2D(50, 1);
            rend.material.mainTexture = texture;
            creep = roomObject as Creep;
            UpdateView();
        }

        public void Delta(JSONObject data) {
            var bodyObj = data["body"];
            if (bodyObj == null)
                return;
            UpdateView();
        }

        private void UpdateView() {
            var frontIndex = 0;
            for (var i = 0; i < PartCount("ranged_attack"); i++) {
                texture.SetPixel(frontIndex, 0, Constants.RANGED_ATTACK_COLOR);
                frontIndex++;
            }
            for (var i = 0; i < PartCount("attack"); i++) {
                texture.SetPixel(frontIndex, 0, Color.red);
                frontIndex++;
            }
            for (var i = 0; i < PartCount("heal"); i++) {
                texture.SetPixel(frontIndex, 0, Color.green);
                frontIndex++;
            }
            for (var i = 0; i < PartCount("work"); i++) {
                texture.SetPixel(frontIndex, 0, Color.yellow);
                frontIndex++;
            }
            for (var i = 0; i < PartCount("claim"); i++) {
                texture.SetPixel(frontIndex, 0, Color.magenta);
                frontIndex++;
            }

            var backIndex = 0;
            for (; backIndex < PartCount("move"); backIndex++) {
                texture.SetPixel(49 - backIndex, 0, Color.gray);
            }

            var toughAlpha = Mathf.Min(PartCount("tough") / 10f, 1);
            var toughColor = new Color(1, 1, 1, toughAlpha);
            for (; frontIndex < 49 - (backIndex - 1); frontIndex++) {
                texture.SetPixel(frontIndex, 0, toughColor);
            }
            texture.Apply();
        }

        private int PartCount(string type) {
            var count = 0;
            foreach (var part in creep.Body.Parts) {
                if (part.type == type && part.hits > 0) {
                    count++;
                }
            }
            return count;
        }
    }
}