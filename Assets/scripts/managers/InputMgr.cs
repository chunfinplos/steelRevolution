using UnityEngine;
using System.Collections.Generic;

public class InputMgr : AComponent {

    public class TargetClick {
        private GameObject gameObject;
        private Vector3 point;
        private float distance;
        private Camera camera;

        public TargetClick(GameObject gameObject, Vector3 point, float distance, Camera camera) {
            this.gameObject = gameObject;
            this.point = point;
            this.distance = distance;
            this.camera = camera;
        }
    }

    protected class TReturnData {
        public AComponent component;
        public ReturnDelegate retDel;
        public int n;
    }

    //public enum TMouseButtonID { LEFT = 0, RIGHT = 1 };
    //private bool mousePressed = false;

    //protected enum TButtonEvent { BEGIN, PRESSED, END, BEGIN_OVER, END_OVER };

    private InputController inputCtrl;

    public delegate void ClickEvent(TargetClick targetClick);
    protected ClickEvent clickBegin;
    protected ClickEvent clickPressed;
    protected ClickEvent clickEnd;
    private TargetClick targetClick;

    public delegate void ReturnDelegate();
    protected Dictionary<int, TReturnData> retDelMap = new Dictionary<int, TReturnData>();
    //protected Dictionary<int, AComponent> retDelGoMap = new Dictionary<int, AComponent>();

    #region MAIN
    
    protected override void Awake() {
        base.Awake();
        inputCtrl = new InputController(true);
    }

    protected override void Start() {
        base.Start();
        DontDestroyOnLoad(this);
    }

    protected override void Update() {
        base.Update();
        OnClick();
        OnReturn();
    }

    #endregion MAIN

    #region RETURN

    public void RegisterReturn(AComponent component, ReturnDelegate retDel) {
        if (retDelMap.ContainsKey(component.GetID())) {
            retDelMap[component.GetID()].component = component;
            retDelMap[component.GetID()].retDel += retDel;
            retDelMap[component.GetID()].n++;
        } else {
            TReturnData data = new TReturnData();
            data.component = component;
            data.retDel += retDel;
            data.n = 1;
            retDelMap.Add(component.GetID(), data);
        }
    }

    public void UnRegisterReturn(AComponent component, ReturnDelegate retDel) {
        if (retDelMap.ContainsKey(component.GetID())) {
            retDelMap[component.GetID()].component = component;
            retDelMap[component.GetID()].retDel -= retDel;
            retDelMap[component.GetID()].n--;

            if (retDelMap[component.GetID()].n <= 0) {
                retDelMap.Remove(component.GetID());
            }
        }
    }

    public void ReturnCallback() {
        ReturnDelegate callback = null;
        foreach (TReturnData data in retDelMap.Values) {
            if (data.retDel != null && data.component.gameObject.activeInHierarchy)
                callback += data.retDel;
        }
        if (callback != null)
            callback();
    }

    protected void OnReturn() {
        if (Input.GetKeyDown(inputCtrl.keys.BACK))
            ReturnCallback();
    }

    #endregion

    #region CLICK

    public void RegisterClickEvent(ClickEvent begin, ClickEvent end, ClickEvent pressed) {
        if (begin != null)
            clickBegin += begin;

        if (end != null)
            clickEnd += end;

        if (pressed != null)
            clickPressed += pressed;
    }

    public void UnRegisterClickEvent(ClickEvent begin, ClickEvent end, ClickEvent pressed) {
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
