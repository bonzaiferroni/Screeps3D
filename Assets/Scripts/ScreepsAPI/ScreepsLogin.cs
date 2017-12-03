using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Screeps3D
{
    public class ScreepsLogin : MonoBehaviour
    {
        [SerializeField] private ScreepsAPI _api;
        [SerializeField] private Toggle _save;
        [SerializeField] private Toggle _ssl;
        [SerializeField] private TMP_InputField _server;
        [SerializeField] private TMP_InputField _port;
        [SerializeField] private TMP_InputField _email;
        [SerializeField] private TMP_InputField _passwordInput;
        [SerializeField] private Button _connect;
        [SerializeField] public FadePanel panel;
        public Action<Credentials, Address> OnSubmit;
        public string secret = "abc123";

        private void Start()
        {
            _connect.onClick.AddListener(OnClick);
            this._save.onValueChanged.AddListener(UpdateSaveSetting);
            RefreshSavedSettings();
            _api.OnConnectionStatusChange += OnConnectionStatusChange;
            panel.Show(false, true);
            panel.Show(); // fade in
        }

        private void OnConnectionStatusChange(bool isConnected)
        {
            panel.Show(!isConnected);
        }

        private void RefreshSavedSettings()
        {
            var save = PlayerPrefs.GetInt("saveCredentials");
            Debug.Log(string.Format("save value: {0}", save));
            if (save == 1)
            {
                this._save.isOn = true;
                var port = PlayerPrefs.GetString("port");
                if (port != null)
                {
                    this._server.text = port;
                }


                var server = PlayerPrefs.GetString("server");
                if (server != null)
                {
                    this._server.text = server;
                }
                var email = PlayerPrefs.GetString("email");
                if (email != null)
                {
                    this._email.text = email;
                }
                var encryptedPassword = PlayerPrefs.GetString("password");
                var password = Crypto.DecryptStringAES(encryptedPassword, secret);
                var ssl = PlayerPrefs.GetInt("ssl");
                this._ssl.isOn = ssl == 1;
                this._passwordInput.text = password;
            }
        }

        private void UpdateSaveSetting(bool value)
        {
            PlayerPrefs.SetInt("saveCredentials", value ? 1 : 0);
            if (!value)
            {
                PlayerPrefs.SetString("email", "");
                PlayerPrefs.SetString("password", "");
            }
        }

        private void OnClick()
        {
            if (_save.isOn)
            {
                PlayerPrefs.SetString("port", _port.text);
                PlayerPrefs.SetString("server", _server.text);
                PlayerPrefs.SetString("email", _email.text);
                var password = this._passwordInput.text;
                var encryptedPassword = Crypto.EncryptStringAES(password, secret);
                PlayerPrefs.SetString("password", encryptedPassword);
                PlayerPrefs.SetInt("ssl", _ssl.isOn ? 1 : 0);
            }

            var credentials = new Credentials
            {
                email = _email.text,
                password = _passwordInput.text
            };
            var address = new Address
            {
                hostName = _server.text,
                path = "/",
                ssl = this._ssl.isOn,
                port = _port.text
            };
            _api.Connect(credentials, address);
        }
    }

    public struct Credentials
    {
        public string email;
        public string password;
    }

    public struct Address
    {
        public bool ssl;
        public string hostName;
        public string port;
        public string path;

        public string Http(string path = "")
        {
            if (path.StartsWith("/") && this.path.EndsWith("/"))
            {
                path = path.Substring(1);
            }
            var protocol = ssl ? "https" : "http";
            var port = hostName.ToLowerInvariant() == "screeps.com" ? "" : string.Format(":{0}", this.port);
            // Debug.Log(string.Format("{0}://{1}{2}{3}{4}", protocol, hostName, port, this.path, path));
            return string.Format("{0}://{1}{2}{3}{4}", protocol, hostName, port, this.path, path);
        }
    }
}