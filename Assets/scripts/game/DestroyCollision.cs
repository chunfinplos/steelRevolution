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

    private void OnCollisionEnter(Collision collision) {
        GameMgr.GetInstance().spawnerMgr.DestroyGameObject(gameObject);
    }
}
