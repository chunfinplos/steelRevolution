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

    //private void OnEnable() {
    //    Physics.IgnoreCollision(GetComponent<CapsuleCollider>(),
    //        GameMgrExtension.GetCustomMgrs().gamePlayMgr.player.GetComponent<CapsuleCollider>());
    //}

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("COLLISION");
        GameMgr.GetInstance().spawnerMgr.DestroyGameObject(gameObject);
    }
}
