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
    private Dictionary<KeyCode, Dictionary<KeyEvt, bool>> activeKeys; // [DOWN,PRESSED,UP]

    public InputController(bool isDefault) {
        keys = new TKeys(isDefault);
        buttons = new TButtons(isDefault);

        mousePressed = false;
        initActiveKeys();
        setInputKeys();
    }

    private void initActiveKeys() {
        activeKeys = new Dictionary<KeyCode, Dictionary<KeyEvt, bool>>();

        /* FORWARD */
        activeKeys.Add(keys.FORWARD, new Dictionary<KeyEvt, bool>());
        activeKeys[keys.FORWARD][KeyEvt.DOWN] = false;
        activeKeys[keys.FORWARD][KeyEvt.PRESSED] = false;
        activeKeys[keys.FORWARD][KeyEvt.UP] = false;

        /* BACKWARD */
        activeKeys.Add(keys.BACKWARD, new Dictionary<KeyEvt, bool>());
        activeKeys[keys.BACKWARD][KeyEvt.DOWN] = false;
        activeKeys[keys.BACKWARD][KeyEvt.PRESSED] = false;
        activeKeys[keys.BACKWARD][KeyEvt.UP] = false;

        /* JUMP */
        activeKeys.Add(keys.JUMP, new Dictionary<KeyEvt, bool>());
        activeKeys[keys.JUMP][KeyEvt.DOWN] = false;
        activeKeys[keys.JUMP][KeyEvt.PRESSED] = false;
        activeKeys[keys.JUMP][KeyEvt.UP] = false;

        /* BACK */
        activeKeys.Add(keys.BACK, new Dictionary<KeyEvt, bool>());
        activeKeys[keys.BACK][KeyEvt.DOWN] = false;
        activeKeys[keys.BACK][KeyEvt.PRESSED] = false;
        activeKeys[keys.BACK][KeyEvt.UP] = false;
    }

    private void setInputKeys() {
        //Input.
    }

    public Dictionary<KeyCode, Dictionary<KeyEvt, bool>> getActiveKeys() {
        /* FORWARD */
        if (Input.GetKeyDown(keys.FORWARD))
            activeKeys[keys.FORWARD][KeyEvt.DOWN] = true;
        else
            activeKeys[keys.FORWARD][KeyEvt.DOWN] = false;
        if (Input.GetKey(keys.FORWARD))
            activeKeys[keys.FORWARD][KeyEvt.PRESSED] = true;
        else
            activeKeys[keys.FORWARD][KeyEvt.PRESSED] = false;
        if (Input.GetKeyUp(keys.FORWARD))
            activeKeys[keys.FORWARD][KeyEvt.UP] = true;
        else
            activeKeys[keys.FORWARD][KeyEvt.UP] = false;

        /* BACKWARD */
        if (Input.GetKeyDown(keys.BACKWARD))
            activeKeys[keys.BACKWARD][KeyEvt.DOWN] = true;
        else
            activeKeys[keys.BACKWARD][KeyEvt.DOWN] = false;
        if (Input.GetKey(keys.BACKWARD))
            activeKeys[keys.BACKWARD][KeyEvt.PRESSED] = true;
        else
            activeKeys[keys.BACKWARD][KeyEvt.PRESSED] = false;
        if (Input.GetKeyUp(keys.BACKWARD))
            activeKeys[keys.BACKWARD][KeyEvt.UP] = true;
        else
            activeKeys[keys.BACKWARD][KeyEvt.UP] = false;

        /* JUMP */
        if (Input.GetKeyDown(keys.JUMP))
            activeKeys[keys.JUMP][KeyEvt.DOWN] = true;
        else
            activeKeys[keys.JUMP][KeyEvt.DOWN] = false;
        if (Input.GetKey(keys.JUMP))
            activeKeys[keys.JUMP][KeyEvt.PRESSED] = true;
        else
            activeKeys[keys.JUMP][KeyEvt.PRESSED] = false;
        if (Input.GetKeyUp(keys.JUMP))
            activeKeys[keys.JUMP][KeyEvt.UP] = true;
        else
            activeKeys[keys.JUMP][KeyEvt.UP] = false;

        /* BACK */
        if (Input.GetKeyDown(keys.BACK))
            activeKeys[keys.BACK][KeyEvt.DOWN] = true;
        else
            activeKeys[keys.BACK][KeyEvt.DOWN] = false;
        if (Input.GetKey(keys.BACK))
            activeKeys[keys.BACK][KeyEvt.PRESSED] = true;
        else
            activeKeys[keys.BACK][KeyEvt.PRESSED] = false;
        if (Input.GetKeyUp(keys.BACK))
            activeKeys[keys.BACK][KeyEvt.UP] = true;
        else
            activeKeys[keys.BACK][KeyEvt.UP] = false;

        return activeKeys;
    }
}
