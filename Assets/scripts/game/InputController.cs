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
            FORWARD = KeyCode.D;
            BACKWARD = KeyCode.A;
            JUMP = KeyCode.W;
            BACK = KeyCode.Escape;
        }
    };

    public struct TButtons {
        public int LEFTSHOOT {
            get; private set;
        }
        public int RIGHTSHOOT {
            get; private set;
        }
        public int RELOAD {
            get; private set;
        }

        public TButtons(bool isDefault) {
            LEFTSHOOT = 0;
            RIGHTSHOOT = 1;
            RELOAD = 2;
        }
    };

    public TKeys keys {
        get; private set;
    }
    public TButtons buttons {
        get; private set;
    }
    private Dictionary<int, Dictionary<inputEvt, bool>> activeButtons; // [DOWN,PRESSED,UP]

    private Dictionary<KeyCode, Dictionary<inputEvt, bool>> activeKeys; // [DOWN,PRESSED,UP]

    public InputController(bool isDefault) {
        Cursor.visible = true;
        keys = new TKeys(isDefault);
        buttons = new TButtons(isDefault);

        initActiveButtons();
        initActiveKeys();
    }

    private void initActiveButtons() {
        activeButtons = new Dictionary<int, Dictionary<inputEvt, bool>>();

        /* LEFTSHOOT */
        activeButtons.Add(buttons.LEFTSHOOT, new Dictionary<inputEvt, bool>());
        activeButtons[buttons.LEFTSHOOT][inputEvt.DOWN] = false;
        activeButtons[buttons.LEFTSHOOT][inputEvt.PRESSED] = false;
        activeButtons[buttons.LEFTSHOOT][inputEvt.UP] = false;

        /* RIGHTSHOOT */
        activeButtons.Add(buttons.RIGHTSHOOT, new Dictionary<inputEvt, bool>());
        activeButtons[buttons.RIGHTSHOOT][inputEvt.DOWN] = false;
        activeButtons[buttons.RIGHTSHOOT][inputEvt.PRESSED] = false;
        activeButtons[buttons.RIGHTSHOOT][inputEvt.UP] = false;

        /* RELOAD */
        activeButtons.Add(buttons.RELOAD, new Dictionary<inputEvt, bool>());
        activeButtons[buttons.RELOAD][inputEvt.DOWN] = false;
        activeButtons[buttons.RELOAD][inputEvt.PRESSED] = false;
        activeButtons[buttons.RELOAD][inputEvt.UP] = false;
    }

    private void initActiveKeys() {
        activeKeys = new Dictionary<KeyCode, Dictionary<inputEvt, bool>>();

        /* FORWARD */
        activeKeys.Add(keys.FORWARD, new Dictionary<inputEvt, bool>());
        activeKeys[keys.FORWARD][inputEvt.DOWN] = false;
        activeKeys[keys.FORWARD][inputEvt.PRESSED] = false;
        activeKeys[keys.FORWARD][inputEvt.UP] = false;

        /* BACKWARD */
        activeKeys.Add(keys.BACKWARD, new Dictionary<inputEvt, bool>());
        activeKeys[keys.BACKWARD][inputEvt.DOWN] = false;
        activeKeys[keys.BACKWARD][inputEvt.PRESSED] = false;
        activeKeys[keys.BACKWARD][inputEvt.UP] = false;

        /* JUMP */
        activeKeys.Add(keys.JUMP, new Dictionary<inputEvt, bool>());
        activeKeys[keys.JUMP][inputEvt.DOWN] = false;
        activeKeys[keys.JUMP][inputEvt.PRESSED] = false;
        activeKeys[keys.JUMP][inputEvt.UP] = false;

        /* BACK */
        activeKeys.Add(keys.BACK, new Dictionary<inputEvt, bool>());
        activeKeys[keys.BACK][inputEvt.DOWN] = false;
        activeKeys[keys.BACK][inputEvt.PRESSED] = false;
        activeKeys[keys.BACK][inputEvt.UP] = false;
    }

    public Dictionary<KeyCode, Dictionary<inputEvt, bool>> getActiveKeys() {
        /* FORWARD */
        if(Input.GetKeyDown(keys.FORWARD))
            activeKeys[keys.FORWARD][inputEvt.DOWN] = true;
        else
            activeKeys[keys.FORWARD][inputEvt.DOWN] = false;
        if(Input.GetKey(keys.FORWARD))
            activeKeys[keys.FORWARD][inputEvt.PRESSED] = true;
        else
            activeKeys[keys.FORWARD][inputEvt.PRESSED] = false;
        if(Input.GetKeyUp(keys.FORWARD))
            activeKeys[keys.FORWARD][inputEvt.UP] = true;
        else
            activeKeys[keys.FORWARD][inputEvt.UP] = false;

        /* BACKWARD */
        if(Input.GetKeyDown(keys.BACKWARD))
            activeKeys[keys.BACKWARD][inputEvt.DOWN] = true;
        else
            activeKeys[keys.BACKWARD][inputEvt.DOWN] = false;
        if(Input.GetKey(keys.BACKWARD))
            activeKeys[keys.BACKWARD][inputEvt.PRESSED] = true;
        else
            activeKeys[keys.BACKWARD][inputEvt.PRESSED] = false;
        if(Input.GetKeyUp(keys.BACKWARD))
            activeKeys[keys.BACKWARD][inputEvt.UP] = true;
        else
            activeKeys[keys.BACKWARD][inputEvt.UP] = false;

        /* JUMP */
        if(Input.GetKeyDown(keys.JUMP))
            activeKeys[keys.JUMP][inputEvt.DOWN] = true;
        else
            activeKeys[keys.JUMP][inputEvt.DOWN] = false;
        if(Input.GetKey(keys.JUMP))
            activeKeys[keys.JUMP][inputEvt.PRESSED] = true;
        else
            activeKeys[keys.JUMP][inputEvt.PRESSED] = false;
        if(Input.GetKeyUp(keys.JUMP))
            activeKeys[keys.JUMP][inputEvt.UP] = true;
        else
            activeKeys[keys.JUMP][inputEvt.UP] = false;

        /* BACK */
        if(Input.GetKeyDown(keys.BACK))
            activeKeys[keys.BACK][inputEvt.DOWN] = true;
        else
            activeKeys[keys.BACK][inputEvt.DOWN] = false;
        if(Input.GetKey(keys.BACK))
            activeKeys[keys.BACK][inputEvt.PRESSED] = true;
        else
            activeKeys[keys.BACK][inputEvt.PRESSED] = false;
        if(Input.GetKeyUp(keys.BACK))
            activeKeys[keys.BACK][inputEvt.UP] = true;
        else
            activeKeys[keys.BACK][inputEvt.UP] = false;

        return activeKeys;
    }

    public Dictionary<int, Dictionary<inputEvt, bool>> getActiveButtons() {
        /* LEFTSHOOT */
        if(Input.GetMouseButtonDown(buttons.LEFTSHOOT))
            activeButtons[buttons.LEFTSHOOT][inputEvt.DOWN] = true;
        else
            activeButtons[buttons.LEFTSHOOT][inputEvt.DOWN] = false;
        if(Input.GetMouseButton(buttons.LEFTSHOOT))
            activeButtons[buttons.LEFTSHOOT][inputEvt.PRESSED] = true;
        else
            activeButtons[buttons.LEFTSHOOT][inputEvt.PRESSED] = false;
        if(Input.GetMouseButtonUp(buttons.LEFTSHOOT))
            activeButtons[buttons.LEFTSHOOT][inputEvt.UP] = true;
        else
            activeButtons[buttons.LEFTSHOOT][inputEvt.UP] = false;

        /* RIGHTSHOOT */
        if(Input.GetMouseButtonDown(buttons.RIGHTSHOOT))
            activeButtons[buttons.RIGHTSHOOT][inputEvt.DOWN] = true;
        else
            activeButtons[buttons.RIGHTSHOOT][inputEvt.DOWN] = false;
        if(Input.GetMouseButton(buttons.RIGHTSHOOT))
            activeButtons[buttons.RIGHTSHOOT][inputEvt.PRESSED] = true;
        else
            activeButtons[buttons.RIGHTSHOOT][inputEvt.PRESSED] = false;
        if(Input.GetMouseButtonUp(buttons.RIGHTSHOOT))
            activeButtons[buttons.RIGHTSHOOT][inputEvt.UP] = true;
        else
            activeButtons[buttons.RIGHTSHOOT][inputEvt.UP] = false;

        /* RELOAD */
        if(Input.GetMouseButtonDown(buttons.RELOAD))
            activeButtons[buttons.RELOAD][inputEvt.DOWN] = true;
        else
            activeButtons[buttons.RELOAD][inputEvt.DOWN] = false;
        if(Input.GetMouseButton(buttons.RELOAD))
            activeButtons[buttons.RELOAD][inputEvt.PRESSED] = true;
        else
            activeButtons[buttons.RELOAD][inputEvt.PRESSED] = false;
        if(Input.GetMouseButtonUp(buttons.RELOAD))
            activeButtons[buttons.RELOAD][inputEvt.UP] = true;
        else
            activeButtons[buttons.RELOAD][inputEvt.UP] = false;

        return activeButtons;
    }
}