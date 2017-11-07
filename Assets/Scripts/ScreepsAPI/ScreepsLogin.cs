using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screeps3D {
    public class ScreepsLogin : MonoBehaviour {
        [SerializeField] private ScreepsAPI api;
        [SerializeField] private Toggle save;
        [SerializeField] private TMP_InputField email;
        [SerializeField] private TMP_InputField password;
        [SerializeField] private Button connect;
        [SerializeField] public FadePanel panel;
        public Action<Credentials, Address> OnSubmit;
        public string secret = "abc123";

        private void Start() {
            connect.onClick.AddListener(OnClick);
            this.save.onValueChanged.AddListener(UpdateSaveSetting);
            RefreshSavedSettings();
            api.OnConnectionStatusChange += OnConnectionStatusChange;
            panel.Show(false, true);
            panel.Show(); // fade in
        }

        private void OnConnectionStatusChange(bool isConnected) {
            panel.Show(!isConnected);
        }

        private void RefreshSavedSettings() {
            var save = PlayerPrefs.GetInt("saveCredentials");
            Debug.Log($"save value: {save}");
            if (save == 1) {
                this.save.isOn = true;
                var email = PlayerPrefs.GetString("email");
                if (email != null) {
                    this.email.text = email;
                }
                var encryptedPassword = PlayerPrefs.GetString("password");
                var password = Crypto.DecryptStringAES(encryptedPassword, secret);
                this.password.text = password;
            }
        }

        private void UpdateSaveSetting(bool value) {
            PlayerPrefs.SetInt("saveCredentials", value ? 1 : 0);
            if (!value) {
                PlayerPrefs.SetString("email", "");
                PlayerPrefs.SetString("password", "");
            }
        }

        private void OnClick() {
            if (save.isOn) {
                PlayerPrefs.SetString("email", email.text);
                var password = this.password.text;
                var encryptedPassword = Crypto.EncryptStringAES(password, secret);
                PlayerPrefs.SetString("password", encryptedPassword);
            }
            
            var credentials = new Credentials {email = email.text, password = password.text};
            var address = new Address {hostName = "screeps.com", path = "/"};
            api.Connect(credentials, address);
        }
    }
	
    public struct Credentials {
        public string email;
        public string password;
    }
	
    public struct Address {
        public string hostName;
        public string path;
	
        public string Http(string path = "") {
            if (path.StartsWith("/") && this.path.EndsWith("/")) {
                path = path.Substring(1);
            }
            return $"https://{hostName}{this.path}{path}";
        }
    }
}