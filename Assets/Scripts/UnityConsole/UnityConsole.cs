using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnityConsole : MonoBehaviour {

    struct Message {
        public Color color;
        public string text;
    }
    
    [SerializeField] private ConsoleLine prototype;
    [SerializeField] private TMP_InputField input;
    [SerializeField] public FadePanel panel;
    [SerializeField] public RectTransform content;
    private List<ConsoleLine> lines = new List<ConsoleLine>();
    private Queue<Message> messages = new Queue<Message>();
    private int index = 0;
    public Action<string> OnInput;
    private float nextMessage;

    private void Start() {
        prototype.Hide();
        input.onSubmit.AddListener(OnSubmit);
    }

    private void OnSubmit(string msg) {
        if (OnInput != null) OnInput.Invoke(msg);
        AddMessage(string.Format("> {0}", msg), Color.cyan);
    }

    public void AddMessage(string msg, Color color) {
        messages.Enqueue(new Message() { text = msg, color = color });
    }

    private void Update() {
        DisplayMessages();
    }

    private void DisplayMessages() {
        if (nextMessage > Time.time) {
            return;
        }

        if (messages.Count == 0) {
            return;
        }

        nextMessage = Time.time + .1f / messages.Count;
        var displayCount = Mathf.Max(messages.Count / 10, 1);
        for (var i = 0; i < displayCount; i++) {
            var message = messages.Dequeue();
            var line = GetLine();
            line.Text = message.text;
            line.Color = message.color;
            line.Show();
        }

        SetGeometry();
    }

    private void SetGeometry() {
        float height = 5;
        for (var i = 0; i < lines.Count; i++) {
            var line = lines[lines.Count - 1 - i];
            var rect = line.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(0, height);
            height += line.Height;
        }
        content.sizeDelta = new Vector2(0, height);
    }

    private ConsoleLine GetLine() {
        if (lines.Count < 100) {
            var line = Instantiate(prototype.gameObject).GetComponent<ConsoleLine>();
            line.transform.SetParent(prototype.transform.parent);
            lines.Add(line);
            return line;
        } else {
            var line = lines[0];
            lines.RemoveAt(0);
            lines.Add(line);
            return line;
        }
    }
}