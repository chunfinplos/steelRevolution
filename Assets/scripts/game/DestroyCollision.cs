using UnityEngine;
using System.Collections;

public class DestroyCollision : AComponent {

    public GameObject effect;
    
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
        if(effect) {
            GameObject explosion = GameMgr.GetInstance().spawnerMgr.CreateNewGameObject(effect,
                                                                 transform.position,
                                                                 transform.rotation);

            ParticleSystem particleSys = explosion.gameObject.transform.GetChild(0).
                                            GetComponent<ParticleSystem>();

            //particleSys.Clear();
            //particleSys.Play();
            particleSys.Emit(100);
        }
        GameMgr.GetInstance().spawnerMgr.DestroyGameObject(gameObject);
    }
}
