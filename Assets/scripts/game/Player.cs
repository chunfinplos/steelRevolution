﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : AComponent {

    private Rigidbody rb;

    public float camRayLength;
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

    /* States */
    private bool forward;
    private bool backward;
    private bool jump;
    private bool onGround;

    #region MAIN

    protected override void Awake() {
        base.Awake();
        inputMgr = GameMgr.GetInstance().GetServer<InputMgr>();

        rb = GetComponent<Rigidbody>();
        movement = Vector3.zero;
        onGround = true;

        registerKeys();
    }

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
        if(onGround) {
            if(forward) {
                if(jump) {
                    movement.Set(speed, jumpSpeed, 0f);
                    rb.AddForce(movement, ForceMode.Impulse);
                    onGround = false;
                } else {
                    movement.Set(speed, 0f, 0f);
                    movement = movement * Time.deltaTime;
                    rb.MovePosition(transform.position + movement);
                }
                gameObject.BroadcastMessage("OnMovementForward", 90);
            } else if(backward) {
                if(jump) {
                    movement.Set(-speed, jumpSpeed, 0f);
                    rb.AddForce(movement, ForceMode.Impulse);
                    onGround = false;
                } else {
                    movement.Set(-speed, 0f, 0f);
                    movement = movement * Time.deltaTime;
                    rb.MovePosition(transform.position + movement);
                }
                gameObject.BroadcastMessage("OnMovementBackward", 90);
            } else {
                gameObject.BroadcastMessage("OnMovementStop");
            }
            if(jump) {
                movement.Set(0f, jumpSpeed, 0f);
                rb.AddForce(movement, ForceMode.Impulse);
                onGround = false;
            }
        }
    }

    //void FixedUpdate() {
    //    bool isMoving = !forward && !backward && !jump && onGround;
    //    GetComponent<Animator>().SetBool("moving", isMoving);
    //}

    #endregion

    private void registerKeys() {
        inputMgr.RegisterKeyDelegate(this, inputMgr.inputCtrl.keys.FORWARD, key);
        inputMgr.RegisterKeyDelegate(this, inputMgr.inputCtrl.keys.BACKWARD, key);
        inputMgr.RegisterKeyDelegate(this, inputMgr.inputCtrl.keys.JUMP, key);
    }

    public void key(KeyCode kCode, Dictionary<inputEvt, bool> keyData) {
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
        //if (keyData[inputEvt.DOWN])
        //    s[0] = "DOWN";
        //if (keyData[inputEvt.PRESSED])
        //    s[1] = "PRESSED";
        //if (keyData[inputEvt.UP])
        //    s[2] = "UP";
        //Debug.Log(s[0] + " _ " + s[1] + " _ " + s[2]);
    }

    private void movementForward(Dictionary<inputEvt, bool> keyData) {
        if (keyData[inputEvt.DOWN] || keyData[inputEvt.PRESSED]) {
            forward = true;
        }
        if (keyData[inputEvt.UP]) {
            forward = false;
        }
    }

    private void movementBackward(Dictionary<inputEvt, bool> keyData) {
        if (keyData[inputEvt.DOWN] || keyData[inputEvt.PRESSED]) {
            backward = true;
        }
        if (keyData[inputEvt.UP]) {
            backward = false;
        }
    }

    private void movementJump(Dictionary<inputEvt, bool> keyData) {
        if (keyData[inputEvt.DOWN]) {
            jump = true;
        } else {
            jump = false;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Floor")) {
            onGround = true;
        }
    }
}
