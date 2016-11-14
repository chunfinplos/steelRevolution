using UnityEngine;
using System.Collections;

public class InputController {

    public struct TKeys {
        public KeyCode FORDWARD {
            get; private set;
        }
        public KeyCode BACKWARD {
            get; private set;
        }
        public KeyCode JUMP {
            get; private set;
        }
        public KeyCode BACK {
            get; private set;
        }

        public TKeys(bool isDefault) {
            FORDWARD = KeyCode.W;
            BACKWARD = KeyCode.S;
            JUMP = KeyCode.Space;
            BACK = KeyCode.Escape;
        }
    };

    public struct TButtons {
        public int LEFT {
            get; private set;
        }
        public int RIGHT {
            get; private set;
        }

        public TButtons(bool isDefault) {
            LEFT = 0;
            RIGHT = 1;
        }
    };

    public TKeys keys {
        get; private set;
    }
    public TButtons buttons {
        get; private set;
    }
    public bool mousePressed;

    public InputController(bool isDefault) {
        keys = new TKeys(isDefault);
        buttons = new TButtons(isDefault);
        mousePressed = false;
    }
}
