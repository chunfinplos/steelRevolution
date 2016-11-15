using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    private Dictionary<KeyCode, bool> activeKeys;

    public InputController(bool isDefault) {
        keys = new TKeys(isDefault);
        buttons = new TButtons(isDefault);

        mousePressed = false;
        activeKeys = new Dictionary<KeyCode, bool>();
        activeKeys[keys.FORDWARD] = false;
        activeKeys[keys.BACKWARD] = false;
        activeKeys[keys.JUMP] = false;
        activeKeys[keys.BACK] = false;
    }

    public Dictionary<KeyCode, bool> getActiveKeys() {
        if (Input.GetKeyDown(keys.FORDWARD))
            activeKeys[keys.FORDWARD] = true;
        if (Input.GetKeyDown(keys.BACKWARD))
            activeKeys[keys.BACKWARD] = true;
        if (Input.GetKeyDown(keys.JUMP))
            activeKeys[keys.JUMP] = true;
        if (Input.GetKeyDown(keys.BACK))
            activeKeys[keys.BACK] = true;
        return activeKeys;
    }
}
