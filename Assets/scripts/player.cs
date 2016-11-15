using UnityEngine;
using System.Collections;

public class Player : AComponent {

    public float speed = 6f;
    public float friction;
    public float lerpSpeed;
    public float xDeg;
    public float yDeg;
    private Quaternion fromRotation;
    private Quaternion toRotation;

    private Vector3 movement;

    protected override void Awake() {
        base.Awake();
        GameMgr.GetInstance().GetServer<InputMgr>().RegisterReturn(this, back);
    }

    public void back() {
        Debug.Log("Back pressed");
        //movement.Set(h, 0f, v);
        //movement = movement.normalized * speed * Time.deltaTime;
        //playerRigidbody.MovePosition(transform.position + movement);
    }

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }
}
