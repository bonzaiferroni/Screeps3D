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
        OnInput?.Invoke(msg);
        AddMessage($"> {msg}", Color.cyan);
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
    }

    private ConsoleLine GetLine() {
        if (lines.Count < 100) {
            var line = Instantiate(prototype.gameObject).GetComponent<ConsoleLine>();
            line.transform.SetParent(prototype.transform.parent);
            lines.Add(line);
            return line;
        } else {
            if (index >= lines.Count) {
                index = 0;
            }
            var line = lines[index++];
            line.transform.SetAsLastSibling();
            return line;
        }
    }
}