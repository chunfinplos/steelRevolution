using UnityEngine;
using System.Collections;

public class Rotate : AComponent {

    public bool rotate;

    protected override void Start () {
        base.Start();
    }

    protected override void Update () {
        base.Update();
    }

    void FixedUpdate() {
        if (rotate) {
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitPoint;

            if (Physics.Raycast(camRay, out hitPoint, 100f, LayerMask.GetMask("Background"))) {
                Vector3 playerToMouse = hitPoint.point;
                //playerToMouse.y = 0f;
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
                //transform.Rotate(playerToMouse, Space.World);
                transform.LookAt(playerToMouse, Vector3.up);
            }
        }
    }
}
