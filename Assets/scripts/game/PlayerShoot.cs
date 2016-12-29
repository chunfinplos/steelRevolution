using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShoot : AComponent {

    private InputMgr inputMgr;
    public GameObject prefabL;
    public GameObject prefabR;

    public float bulletSpeed;
    public Transform shootL;
    public Transform shootR;

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
        if(buttonData[inputEvt.DOWN]) {
            if(mCode == inputMgr.inputCtrl.buttons.LEFTSHOOT) {
                Shoot(true);
            } else if(mCode == inputMgr.inputCtrl.buttons.RIGHTSHOOT) {
                Shoot(false);
            } else {

            }
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

    private void Shoot(bool isLeft) {
        //Vector3 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 sourcePoint;
        Quaternion sourceQ;
        GameObject prefab;
        if(isLeft) {
            sourcePoint = shootL.position;
            sourceQ = shootL.rotation;
            prefab = prefabL;
        } else {
            sourcePoint = shootR.position;
            sourceQ = shootR.rotation;
            prefab = prefabR;
        }
        sourceQ *= Quaternion.Euler(Vector3.right * 90);
        sourceQ *= Quaternion.Euler(Vector3.up * 45);
        GameObject bullet = GameMgr.GetInstance().spawnerMgr.
                                    CreateNewGameObject(prefab, sourcePoint, sourceQ);

        

        //bullet.transform.LookAt(targetPoint);
        //bullet.GetComponent<Rigidbody>().AddForce(sourcePoint * bulletSpeed);
        bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * bulletSpeed);
    }
}