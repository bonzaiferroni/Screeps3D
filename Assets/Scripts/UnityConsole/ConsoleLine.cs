using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleLine : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI text;
    private float target;
    private float targetRef;

    private float Alpha {
        get { return text.color.a; }
        set {
            var color = text.color;
            color.a = value;
            text.color = color;
        }
    }

    public string Text {
        get { return text.text; }
        set {
            text.text = value;
            text.parseCtrlCharacters = true;
        }
    }

    public Color Color {
        get { return text.color; }
        set {
            text.color = value;
            Alpha = 0;
        }
    }

    public void Hide() {
        enabled = true;
        Alpha = 0;
        target = 0;
    }

    public void Show() {
        enabled = true;
        target = 1;
    }

    public float Height {
        get { return text.preferredHeight;  }
    }

    public void Update() {
        if (Mathf.Abs(Alpha - target) < .01) {
            enabled = false;
            return;
        }

        Alpha = Mathf.SmoothDamp(Alpha, target, ref targetRef, .2f);
    }
}