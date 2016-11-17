using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : AComponent {

    private Rigidbody rb;

    public float speed;
    public float friction;
    public float lerpSpeed;
    public float xDeg;
    public float yDeg;
    private Quaternion fromRotation;
    private Quaternion toRotation;

    private Vector3 movement;

    private InputMgr inputMgr;

    protected override void Awake() {
        base.Awake();
        inputMgr = GameMgr.GetInstance().GetServer<InputMgr>();
        inputMgr.RegisterKeyDelegate(this, inputMgr.inputCtrl.keys.FORWARD, key);
        rb = GetComponent<Rigidbody>();
        movement = Vector3.zero;
    }

    public void key(Dictionary<KeyEvt, bool> keyData) {
        string[] s = new string[] { "","","" };
        if (keyData[KeyEvt.DOWN])
            s[0] = "DOWN";
        if (keyData[KeyEvt.PRESSED])
            s[1] = "PRESSED";
        if (keyData[KeyEvt.UP])
            s[2] = "UP";
        Debug.Log(s[0] + " _ " + s[1] + " _ " + s[2]);
    }

    public void forward() {
        Debug.Log("forward");
        movement.Set(1f, 0f, 0f);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        gameObject.BroadcastMessage("OnMovementForward", 90);
    }

    public void backward() {
        Debug.Log("backward");
        movement.Set(-1f, 0f, 0f);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        gameObject.BroadcastMessage("OnMovementBackward", 90);
    }

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    void FixedUpdate() {
        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        //movement.Set(h, 0f, v);
        //movement = movement.normalized * speed * Time.deltaTime;
        //rb.MovePosition(transform.position + movement);
    }
}
