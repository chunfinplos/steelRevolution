using UnityEngine;
using System.Collections.Generic;

public delegate void keyDelegate(KeyCode kCode, Dictionary<inputEvt, bool> keyData);
public delegate void mouseDelegate(int mCode, Dictionary<inputEvt, bool> mouseData);
public enum inputEvt { DOWN, PRESSED, UP };

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
    
    protected Dictionary<int, TkeyDelegateData> kbDelegateMap;
    protected Dictionary<int, TMouseDelegateData> msDelegateMap;

    #region MAIN

    protected override void Awake() {
        base.Awake();
        inputCtrl = new InputController(true);

        kbDelegateMap = new Dictionary<int, TkeyDelegateData>();
        msDelegateMap = new Dictionary<int, TMouseDelegateData>();
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

    #region KEYS

    public void RegisterKeyDelegate(AComponent component, KeyCode kCode, keyDelegate kDel) {
        if (kbDelegateMap.ContainsKey(component.GetID())) {
            kbDelegateMap[component.GetID()].addDelegate(kCode, kDel);
        } else {
            kbDelegateMap[component.GetID()] = new TkeyDelegateData(component, kCode, kDel);
        }
    }

    public void UnRegisterKeyDelegate(AComponent component, KeyCode kCode, keyDelegate kDel) {
        if (kbDelegateMap.ContainsKey(component.GetID())) {
            kbDelegateMap[component.GetID()].removeDelegate(kCode, kDel);
            if (kbDelegateMap[component.GetID()].n <= 0) {
                kbDelegateMap.Remove(component.GetID());
            }
        }
    }

    public void KeyDelegateCB(Dictionary<KeyCode, Dictionary<inputEvt, bool>> activeKeys) {
        foreach (KeyValuePair<KeyCode, Dictionary<inputEvt, bool>> keyData in activeKeys) {
            if (keyData.Value[inputEvt.DOWN] ||
                keyData.Value[inputEvt.PRESSED] ||
                keyData.Value[inputEvt.UP]) {
                foreach (KeyValuePair<int, TkeyDelegateData> data in kbDelegateMap) {
                    if (data.Value.component.gameObject.activeInHierarchy) {
                        data.Value.callDelegate(keyData.Key, keyData.Value);
                    }
                }
            }
        }
    }

    protected void OnKey() {
        Dictionary<KeyCode, Dictionary<inputEvt, bool>> activeKeys = inputCtrl.getActiveKeys();
        KeyDelegateCB(activeKeys);
    }

    #endregion

    #region MOUSE

    public void RegisterMouseDelegate(AComponent component, int mCode, mouseDelegate mDel) {
        if (msDelegateMap.ContainsKey(component.GetID())) {
            msDelegateMap[component.GetID()].addDelegate(mCode, mDel);
        } else {
            msDelegateMap[component.GetID()] = new TMouseDelegateData(component, mCode, mDel);
        }
    }

    public void UnRegisterMouseDelegate(AComponent component, int mCode, mouseDelegate mDel) {
        if (msDelegateMap.ContainsKey(component.GetID())) {
            msDelegateMap[component.GetID()].removeDelegate(mCode, mDel);
            if (msDelegateMap[component.GetID()].n <= 0) {
                msDelegateMap.Remove(component.GetID());
            }
        }
    }

    public void mouseDelegateCB(Dictionary<int, Dictionary<inputEvt, bool>> activeButtons) {
        foreach (KeyValuePair<int, Dictionary<inputEvt, bool>> buttonData in activeButtons) {
            if (buttonData.Value[inputEvt.DOWN] ||
                buttonData.Value[inputEvt.PRESSED] ||
                buttonData.Value[inputEvt.UP]) {
                foreach (KeyValuePair<int, TMouseDelegateData> data in msDelegateMap) {
                    if (data.Value.component.gameObject.activeInHierarchy) {
                        data.Value.callDelegate(buttonData.Key, buttonData.Value);
                    }
                }
            }
        }
    }

    protected void OnClick() {
        Dictionary<int, Dictionary<inputEvt, bool>> activeButtons = inputCtrl.getActiveButtons();
        mouseDelegateCB(activeButtons);
    }

    //protected void CheckTouch(Vector3 mousePosition, bool begin) {
    //    bool isCollision = false;
    //    Camera[] cameras = Camera.allCameras;

    //    int i = cameras.Length - 1;
    //    while (!isCollision && i >= 0) {
    //        isCollision = ThrowRay(cameras[i], mousePosition, begin);
    //        i--;
    //    }
    //}

    //protected bool ThrowRay(Camera camera, Vector3 position, bool begin) {
    //    targetClick = null;

    //    Ray ray = camera.ScreenPointToRay(position);
    //    RaycastHit hit;
    //    bool collision = Physics.Raycast(ray, out hit);
    //    if (collision) {
    //        targetClick = new TargetClick(hit.collider.gameObject, hit.point, hit.distance, camera);
    //        if (begin) {
    //            if (clickBegin != null)
    //                clickBegin(targetClick);
    //        } else {
    //            if (clickPressed != null)
    //                clickPressed(targetClick);
    //        }
    //    }
    //    return collision;
    //}

    #endregion
}
