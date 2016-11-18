using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : AComponent {

    private Rigidbody rb;

    public float speed;
    public float jumpSpeed;
    public float friction;
    public float lerpSpeed;
    public float xDeg;
    public float yDeg;
    private Quaternion fromRotation;
    private Quaternion toRotation;

    private Vector3 movement;

    private InputMgr inputMgr;
    private bool forward;
    private bool backward;
    private bool jump;
    private bool onGround;

    protected override void Awake() {
        base.Awake();
        inputMgr = GameMgr.GetInstance().GetServer<InputMgr>();

        rb = GetComponent<Rigidbody>();
        movement = Vector3.zero;
        onGround = true;

        registerKeys();
    }

    private void registerKeys() {
        inputMgr.RegisterKeyDelegate(this, inputMgr.inputCtrl.keys.FORWARD, key);
        inputMgr.RegisterKeyDelegate(this, inputMgr.inputCtrl.keys.BACKWARD, key);
        inputMgr.RegisterKeyDelegate(this, inputMgr.inputCtrl.keys.JUMP, key);
    }

    public void key(KeyCode kCode, Dictionary<KeyEvt, bool> keyData) {
        if (onGround) {
            if (kCode == inputMgr.inputCtrl.keys.FORWARD) {
                movementForward(keyData);
            } else if (kCode == inputMgr.inputCtrl.keys.BACKWARD) {
                movementBackward(keyData);
            } else if (kCode == inputMgr.inputCtrl.keys.JUMP) {
                movementJump(keyData);
            }
        } else {
            forward = false;
            backward = false;
            jump = false;
        }
        //string[] s = new string[] { "","","" };
        //if (keyData[KeyEvt.DOWN])
        //    s[0] = "DOWN";
        //if (keyData[KeyEvt.PRESSED])
        //    s[1] = "PRESSED";
        //if (keyData[KeyEvt.UP])
        //    s[2] = "UP";
        //Debug.Log(s[0] + " _ " + s[1] + " _ " + s[2]);
    }

    private void movementForward(Dictionary<KeyEvt, bool> keyData) {
        if (keyData[KeyEvt.DOWN] || keyData[KeyEvt.PRESSED]) {
            forward = true;
        }
        if (keyData[KeyEvt.UP]) {
            forward = false;
        }
    }

    private void movementBackward(Dictionary<KeyEvt, bool> keyData) {
        if (keyData[KeyEvt.DOWN] || keyData[KeyEvt.PRESSED]) {
            backward = true;
        }
        if (keyData[KeyEvt.UP]) {
            backward = false;
        }
    }

    private void movementJump(Dictionary<KeyEvt, bool> keyData) {
        if (keyData[KeyEvt.DOWN]) {
            jump = true;
        } else {
            jump = false;
        }
    }

    //public void forward() {
    //    Debug.Log("forward");
    //    movement.Set(1f, 0f, 0f);
    //    movement = movement.normalized * speed * Time.deltaTime;
    //    rb.MovePosition(transform.position + movement);
    //    gameObject.BroadcastMessage("OnMovementForward", 90);
    //}

    //public void backward() {
    //    Debug.Log("backward");
    //    movement.Set(-1f, 0f, 0f);
    //    movement = movement.normalized * speed * Time.deltaTime;
    //    rb.MovePosition(transform.position + movement);
    //    gameObject.BroadcastMessage("OnMovementBackward", 90);
    //}

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    void FixedUpdate() {
        if (onGround) {
            if (forward) {
                if (jump) {
                    movement.Set(1f, 1f, 0f);
                    rb.AddForce(movement * jumpSpeed);
                    onGround = false;
                } else {
                    movement.Set(1f, 0f, 0f);
                    movement = movement.normalized * speed * Time.deltaTime;
                    rb.MovePosition(transform.position + movement);
                }
                gameObject.BroadcastMessage("OnMovementForward", 90);
            }
        }
        //if (backward) {
        //    movement.Set(-1f, 0f, 0f);
        //    movement = movement.normalized * speed * Time.deltaTime;
        //    rb.MovePosition(transform.position + movement);
        //    gameObject.BroadcastMessage("OnMovementBackward", 90);
        //}

        //if (!forward && !backward) {
        //    gameObject.BroadcastMessage("OnMovementStop");
        //}

        //if (jump) {
        //    movement.Set(0f, 1f, 0f);
        //    movement = movement.normalized * jumpSpeed * Time.deltaTime;
        //    rb.MovePosition(transform.position + movement);
        //}


        //float h = Input.GetAxisRaw("Horizontal");
        //float v = Input.GetAxisRaw("Vertical");
        //movement.Set(h, 0f, v);
        //movement = movement.normalized * speed * Time.deltaTime;
        //rb.MovePosition(transform.position + movement);
    }
}
