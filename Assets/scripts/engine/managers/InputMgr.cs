using UnityEngine;
using System.Collections.Generic;

public delegate void keyDelegate(KeyCode kCode, Dictionary<KeyEvt, bool> keyData);
public enum KeyEvt { DOWN, PRESSED, UP };

public class InputMgr : AComponent {
    //public enum TMouseButtonID { LEFT = 0, RIGHT = 1 };
    //private bool mousePressed = false;

    //protected enum TButtonEvent { BEGIN, PRESSED, END, BEGIN_OVER, END_OVER };

    public InputController inputCtrl {
        get; private set;
    }

    public delegate void ClickDelegate(TargetClick targetClick);
    protected ClickDelegate clickBegin;
    protected ClickDelegate clickPressed;
    protected ClickDelegate clickEnd;
    private TargetClick targetClick;

    //public delegate void ReturnDelegate();
    protected Dictionary<int, TkeyDelegateData> delegateMap;
    //protected Dictionary<int, AComponent> retDelGoMap = new Dictionary<int, AComponent>();

    #region MAIN
    
    protected override void Awake() {
        base.Awake();
        inputCtrl = new InputController(true);

        delegateMap = new Dictionary<int, TkeyDelegateData>();
    }

    protected override void Start() {
        base.Start();
        DontDestroyOnLoad(this);
    }

    protected override void Update() {
        base.Update();
        OnClick();
        OnKey();
    }

    #endregion MAIN

    #region RETURN

    public void RegisterKeyDelegate(AComponent component, KeyCode kCode, keyDelegate kDel) {
        if (delegateMap.ContainsKey(component.GetID())) {
            delegateMap[component.GetID()].addDelegate(kCode, kDel);
        } else {
            delegateMap[component.GetID()] = new TkeyDelegateData(component, kCode, kDel);
        }
    }

    public void UnRegisterKeyDelegate(AComponent component, KeyCode kCode, keyDelegate kDel) {
        if (delegateMap.ContainsKey(component.GetID())) {
            delegateMap[component.GetID()].removeDelegate(kCode, kDel);
            if (delegateMap[component.GetID()].n <= 0) {
                delegateMap.Remove(component.GetID());
            }
        }
    }

    public void KeyDelegateCB(Dictionary<KeyCode, Dictionary<KeyEvt, bool>> activeKeys) {
        foreach (KeyValuePair<KeyCode, Dictionary<KeyEvt, bool>> keyData in activeKeys) {
            if (keyData.Value[KeyEvt.DOWN] ||
                keyData.Value[KeyEvt.PRESSED] ||
                keyData.Value[KeyEvt.UP]) {
                foreach (KeyValuePair<int, TkeyDelegateData> data in delegateMap) {
                    if (data.Value.component.gameObject.activeInHierarchy) {
                        data.Value.callDelegate(keyData.Key, keyData.Value);
                    }
                }
            }
        }
    }

    protected void OnKey() {
        Dictionary<KeyCode, Dictionary<KeyEvt, bool>> activeKeys = inputCtrl.getActiveKeys();
        KeyDelegateCB(activeKeys);
    }

    #endregion

    #region CLICK

    public void RegisterClickEvent(ClickDelegate begin, ClickDelegate end, ClickDelegate pressed) {
        if (begin != null)
            clickBegin += begin;

        if (end != null)
            clickEnd += end;

        if (pressed != null)
            clickPressed += pressed;
    }

    public void UnRegisterClickEvent(ClickDelegate begin, ClickDelegate end, ClickDelegate pressed) {
        if (begin != null)
            clickBegin -= begin;

        if (end != null)
            clickEnd -= end;

        if (pressed != null)
            clickPressed -= pressed;
    }

    protected void OnClick() {
        Vector3 mousePosition = Input.mousePosition;
        if (Input.GetMouseButtonDown(inputCtrl.buttons.LEFT)) {
            CheckTouch(mousePosition, true);
            inputCtrl.mousePressed = true;
        } else if (Input.GetMouseButton(inputCtrl.buttons.LEFT)) {
            CheckTouch(mousePosition, false);
            inputCtrl.mousePressed = true;
        } else if (inputCtrl.mousePressed) {
            if (clickEnd != null)
                clickEnd(targetClick);
            inputCtrl.mousePressed = false;
        }
    }

    protected void CheckTouch(Vector3 mousePosition, bool begin) {
        bool isCollision = false;
        Camera[] cameras = Camera.allCameras;

        int i = cameras.Length - 1;
        while (!isCollision && i >= 0) {
            isCollision = ThrowRay(cameras[i], mousePosition, begin);
            i--;
        }
    }

    protected bool ThrowRay(Camera camera, Vector3 position, bool begin) {
        targetClick = null;

        Ray ray = camera.ScreenPointToRay(position);
        RaycastHit hit;
        bool collision = Physics.Raycast(ray, out hit);
        if (collision) {
            targetClick = new TargetClick(hit.collider.gameObject, hit.point, hit.distance, camera);
            if (begin) {
                if (clickBegin != null)
                    clickBegin(targetClick);
            } else {
                if (clickPressed != null)
                    clickPressed(targetClick);
            }
        }
        return collision;
    }

    #endregion
}
