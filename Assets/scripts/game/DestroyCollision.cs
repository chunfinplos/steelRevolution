using UnityEngine;
using System.Collections;

public class DestroyCollision : AComponent {



    #region MAIN

    protected override void Awake() {
        base.Awake();
    }

    protected override void Start() {
        base.Start();
    }

    protected override void Update() {
        base.Update();
    }

    //void FixedUpdate() {}

    #endregion

    void OnTriggerEnter(Collider other) {
        Debug.Log("COLLISION");
        Destroy(gameObject);
    }
}
