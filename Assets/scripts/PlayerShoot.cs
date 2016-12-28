using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShoot : AComponent {

    private InputMgr inputMgr;
    public GameObject prefabL;
    public GameObject prefabR;

    public float bulletSpeed;

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
        Vector3 targetPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mCode == inputMgr.inputCtrl.buttons.LEFTSHOOT && buttonData[inputEvt.DOWN]) {
            Vector3 sourcePoint = gameObject.transform.GetChild(0).transform.position;
            Quaternion sourceQ = gameObject.transform.GetChild(0).transform.rotation;
            sourceQ *= Quaternion.Euler(Vector3.right * 90);
            GameObject bullet = GameMgr.GetInstance().spawnerMgr.
                                        CreateNewGameObject(prefabL, sourcePoint, sourceQ);

            //bullet.transform.LookAt(targetPoint);
            //bullet.GetComponent<Rigidbody>().AddForce(sourcePoint * bulletSpeed);
            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * bulletSpeed);




        } else if(mCode == inputMgr.inputCtrl.buttons.RIGHTSHOOT) {
            

            //var position = Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            //position = 
            //var go = Instantiate(prefab, transform.position, Quaternion.identity) as GameObject;
            //go.transform.LookAt(position);
            //Debug.Log(position);
            //go.rigidbody.AddForce(go.transform.forward * 1000);
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