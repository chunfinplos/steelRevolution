using UnityEngine;
using System.Collections;

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
        inputMgr.RegisterKeyDelegate(this, inputMgr.inputCtrl.keys.FORWARD, forward);
        inputMgr.RegisterKeyDelegate(this, inputMgr.inputCtrl.keys.BACKWARD, backward);
        rb = GetComponent<Rigidbody>();
        movement = Vector3.zero;
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
