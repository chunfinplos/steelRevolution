using UnityEngine;
using System.Collections;

public class WheelSpinAnimation : AComponent {

    public int side;

    private bool rotate;
    private int degrees;

    protected override void Start() {
        rotate = false;
        degrees = 0;
    }

    void FixedUpdate () {
        if (rotate) {
            transform.Rotate(0, Time.deltaTime * degrees * side, 0);
        }
    }

    void OnMovementForward(int degrees) {
        rotate = true;
        this.degrees = -degrees;
    }

    void OnMovementBackward(int degrees) {
        rotate = true;
        this.degrees = degrees;
    }
}