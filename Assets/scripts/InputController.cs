using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputController {

    public struct TKeys {
        public KeyCode FORWARD {
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
            FORWARD = KeyCode.W;
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
        initActiveKeys();
        setInputKeys();
    }

    private void initActiveKeys() {
        activeKeys = new Dictionary<KeyCode, bool>();
        activeKeys[keys.FORWARD] = false;
        activeKeys[keys.BACKWARD] = false;
        activeKeys[keys.JUMP] = false;
        activeKeys[keys.BACK] = false;
    }

    private void setInputKeys() {
        //Input.
    }

    public Dictionary<KeyCode, bool> getActiveKeys() {
        if (Input.GetKey(keys.FORWARD))
            activeKeys[keys.FORWARD] = true;
        else
            activeKeys[keys.FORWARD] = false;
        if (Input.GetKey(keys.BACKWARD))
            activeKeys[keys.BACKWARD] = true;
        else
            activeKeys[keys.BACKWARD] = false;
        if (Input.GetKeyDown(keys.JUMP))
            activeKeys[keys.JUMP] = true;
        else
            activeKeys[keys.JUMP] = false;
        if (Input.GetKeyDown(keys.BACK))
            activeKeys[keys.BACK] = true;
        else
            activeKeys[keys.BACK] = false;
        return activeKeys;
    }
}
