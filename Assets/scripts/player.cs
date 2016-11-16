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
        //inputMgr = GameMgr.GetInstance().GetServer<InputMgr>();
        //inputMgr.RegisterKeyDelegate(this, inputMgr.inputCtrl.keys.FORDWARD, fordward);
        rb = GetComponent<Rigidbody>();
        movement = Vector3.zero;
    }

    public void fordward() {
        Debug.Log("fordward");
    }

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        movement.Set(h, 0f, v);
        movement = movement.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
    }
}
