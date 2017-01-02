using UnityEngine;
using System.Collections;

public class AutoDestroy : AComponent {

    public float destroyTime = 0.0f;

    #region MAIN

    protected override void Awake() {
        base.Awake();
    }

    protected override void Start() {
        base.Start();
        StartCoroutine(destroyLater(destroyTime));
    }

    protected override void Update() {
        base.Update();
    }

    //void FixedUpdate() {}

    #endregion

    private IEnumerator destroyLater(float time) {
        yield return new WaitForSeconds(time);
        GameMgr.GetInstance().spawnerMgr.DestroyGameObject(gameObject);
    }
}
