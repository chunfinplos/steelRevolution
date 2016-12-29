using UnityEngine;
using System.Collections;

public class Rotate : AComponent {

    public bool rotate;
    private int mask;

    protected override void Awake() {
        base.Awake();
        mask = LayerMask.GetMask("Background") | LayerMask.GetMask("Shootable");
    }

    protected override void Start () {
        base.Start();
    }

    protected override void Update () {
        base.Update();
        if(rotate) {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if(Physics.Raycast(camRay, out hitPoint, 100f, mask)) {
                Vector3 playerToMouse = hitPoint.point;
                //playerToMouse.y = 0f;
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                //transform.Rotate(playerToMouse, Space.World);
                transform.LookAt(playerToMouse, Vector3.up);
            }
        }
    }

    //void FixedUpdate() {}
}
