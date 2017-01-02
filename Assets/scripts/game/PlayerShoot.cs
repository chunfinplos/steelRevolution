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

    public float timeBetweenShotsL = 0.5f;
    public float timeBetweenShotsR = 1.0f;

    private float timeSinceLastShotL = 0.0f;
    private float timeSinceLastShotR = 0.0f;

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
        timeSinceLastShotL += Time.deltaTime;
        timeSinceLastShotR += Time.deltaTime;
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
                ShootL();
            } else if(mCode == inputMgr.inputCtrl.buttons.RIGHTSHOOT) {
                ShootR();
            } else {

            }
        }
    }

    private void ShootL() {
        //Debug.Log(timeSinceLastShotL + " >= " + timeBetweenShotsL);
        if(timeSinceLastShotL >= timeBetweenShotsL) {
            GameObject bullet = createProyectil(prefabL, shootL.position, shootL.rotation);
            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * bulletSpeed);
            timeSinceLastShotL = 0.0f;
        } else {
            Debug.Log("can't shoot L");
        }
    }

    private void ShootR() {
        if(timeSinceLastShotR >= timeBetweenShotsR) {
            GameObject bullet = createProyectil(prefabR, shootR.position, shootR.rotation);
            bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * bulletSpeed);
            timeSinceLastShotR = 0.0f;
        } else {
            Debug.Log("can't shoot R");
        }
    }

    private GameObject createProyectil(GameObject prefab, Vector3 sourcePoint, Quaternion sourceQ) {
        sourceQ *= Quaternion.Euler(Vector3.right * 90);
        sourceQ *= Quaternion.Euler(Vector3.up * 45);
        GameObject bullet = GameMgr.GetInstance().spawnerMgr.
                                    CreateNewGameObject(prefab, sourcePoint, sourceQ);

        Physics.IgnoreCollision(bullet.GetComponent<CapsuleCollider>(),
            GameMgrExtension.GetCustomMgrs().gamePlayMgr.player.GetComponent<CapsuleCollider>());

        return bullet;
    }
}