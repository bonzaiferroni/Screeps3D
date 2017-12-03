using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnityConsole : MonoBehaviour
{
    struct Message
    {
        public Color color;
        public string text;
    }

    public Action<string> OnInput;
    
    [SerializeField] private ConsoleLine _prototype;
    [SerializeField] private TMP_InputField _input;
    [SerializeField] public FadePanel _panel;
    [SerializeField] public RectTransform _content;
    private List<ConsoleLine> _lines = new List<ConsoleLine>();
    private Queue<Message> _messages = new Queue<Message>();
    private int _index = 0;
    private float _nextMessage;

    private void Start()
    {
        _prototype.Hide();
        _input.onSubmit.AddListener(OnSubmit);
    }

    private void OnSubmit(string msg)
    {
        if (OnInput != null) OnInput.Invoke(msg);
        AddMessage(string.Format("> {0}", msg), Color.cyan);
    }

    public void AddMessage(string msg, Color color)
    {
        _messages.Enqueue(new Message() {text = msg, color = color});
    }

    private void Update()
    {
        DisplayMessages();
    }

    private void DisplayMessages()
    {
        if (_nextMessage > Time.time)
        {
            return;
        }

        if (_messages.Count == 0)
        {
            return;
        }

        _nextMessage = Time.time + .1f / _messages.Count;
        var displayCount = Mathf.Max(_messages.Count / 10, 1);
        for (var i = 0; i < displayCount; i++)
        {
            var message = _messages.Dequeue();
            var line = GetLine();
            line.Text = message.text;
            line.Color = message.color;
            line.Show();
        }

        SetGeometry();
    }

    private void SetGeometry()
    {
        float height = 5;
        for (var i = 0; i < _lines.Count; i++)
        {
            var line = _lines[_lines.Count - 1 - i];
            var rect = line.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, height);
            height += line.Height;
        }
        _content.sizeDelta = new Vector2(0, height);
    }

    private ConsoleLine GetLine()
    {
        if (_lines.Count < 100)
        {
            var line = Instantiate(_prototype.gameObject).GetComponent<ConsoleLine>();
            line.transform.SetParent(_prototype.transform.parent);
            _lines.Add(line);
            return line;
        } else
        {
            var line = _lines[0];
            _lines.RemoveAt(0);
            _lines.Add(line);
            return line;
        }
    }
}