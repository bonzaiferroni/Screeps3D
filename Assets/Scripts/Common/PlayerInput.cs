using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Common
{
    public class PlayerInput : BaseSingleton<PlayerInput>
    {
        [SerializeField] private FadePanel _panel;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private TMP_InputField _input;
        [SerializeField] private Button _submit;
        [SerializeField] private Button _cancel;
        private Action<string> _onInput;

        public static void Get(string label, Action<string> onInput)
        {
            Instance.GetInput(label, onInput);
        }

        private void Start()
        {
            _submit.onClick.AddListener(OnSubmit);
            _input.onSubmit.AddListener(OnSubmit);
            _cancel.onClick.AddListener(OnCancel);
        }

        private void OnCancel()
        {
            _onInput(null);
            _panel.Hide();
        }

        private void OnSubmit()
        {
            OnSubmit(_input.text);
        }

        private void OnSubmit(string text)
        {
            _onInput(text);
            _panel.Hide();
        }

        private void GetInput(string label, Action<string> onInput)
        {
            _onInput = onInput;
            _label.text = label;
            _panel.Show();
        }
    }
}