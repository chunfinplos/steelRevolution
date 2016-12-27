using UnityEngine;
using System.Collections;

public class PrebuildGameObjects : AComponent {

    public CacheData[] objectCache;
    protected bool isInstanciate = false;

    [System.Serializable]
    public class CacheData {
        public GameObject prefab;
        public int cacheSize;
    }

    protected override void Start() {
        base.Start();
        //IsLoadingFinish
        SceneMgr sceneManager = GameMgr.GetInstance().GetServer<SceneMgr>();
        if (sceneManager.IsLoadingFinish()) {
            GameMgr.GetInstance().spawnerMgr.InstanciateInitialObjects(this);
            isInstanciate = true;
        }

    }

    protected override void Update() {
        base.Update();
        if (!isInstanciate) {
            GameMgr.GetInstance().spawnerMgr.InstanciateInitialObjects(this);
            isInstanciate = true;
        }
    }

}
