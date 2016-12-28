using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShoot : AComponent {

    private InputMgr inputMgr;

    #region MAIN

    protected override void Awake() {
        base.Awake();
        inputMgr = GameMgr.GetInstance().GetServer<InputMgr>();

        registerButtons();
    }

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    //void FixedUpdate() {}

    #endregion

    private void registerButtons() {
        inputMgr.RegisterMouseDelegate(this, inputMgr.inputCtrl.buttons.LEFTSHOOT, button);
        inputMgr.RegisterMouseDelegate(this, inputMgr.inputCtrl.buttons.RIGHTSHOOT, button);
    }

    public void button(int mCode, Dictionary<inputEvt, bool> buttonData) {
        if(mCode == inputMgr.inputCtrl.buttons.LEFTSHOOT) {

        } else if(mCode == inputMgr.inputCtrl.buttons.RIGHTSHOOT) {
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            var position = Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            position = Camera.main.ScreenToWorldPoint(position);
            var go = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
            go.transform.LookAt(position);
            Debug.Log(position);
            go.rigidbody.AddForce(go.transform.forward * 1000);
        } else {

        }

        //string[] s = new string[] { "", "", "" };
        //if(buttonData[inputEvt.DOWN])
        //    s[0] = "DOWN";
        //if(buttonData[inputEvt.PRESSED])
        //    s[1] = "PRESSED";
        //if(buttonData[inputEvt.UP])
        //    s[2] = "UP";
        //Debug.Log(mCode + ": " + s[0] + " _ " + s[1] + " _ " + s[2]);
    }
}