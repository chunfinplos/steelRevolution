using UnityEngine;
using System.Collections.Generic;

public class SpawnerMgr {

    private SceneMgr sceneMgr;

    private Dictionary<string, List<GameObject>> cache = new Dictionary<string, List<GameObject>>();
    private Transform lastRespawnPoint;
    private static int staticIDs = 0;

    public SpawnerMgr(SceneMgr sceneMgr) {
        this.sceneMgr = sceneMgr;

        Assert.AbortIfNot(sceneMgr != null, "Error: SceneMgr is NULL");

        sceneMgr.RegisterDestroyDelegate(OnDestroyCurrentScene);
    }

    protected void OnDestroyCurrentScene() {
        Debug.Log("OnDestroyCurrentScene");
        ClearCache();
    }

    public void ClearCache() {
        foreach (List<GameObject> a_list in cache.Values) {
            foreach (GameObject go in a_list) {
                GameObject.Destroy(go);
            }
            a_list.Clear();
        }
        cache.Clear();
    }

    public Transform GetPlayerSpawnerPoint() {
        return lastRespawnPoint;
    }

    public void ChangeSpawnPoint(Transform spawnPoint) {
        lastRespawnPoint = spawnPoint;
    }

    #region OBJECTS MANAGEMENT

    public GameObject CreateNewGameObject(GameObject prefab, Vector3 pos, Quaternion rot) {
        GameObject instance = null;
        if (cache.ContainsKey(prefab.name)) {
            List<GameObject> list = cache[prefab.name];
            if (list.Count > 0) {
                instance = list[0];
                list.RemoveAt(0);
                instance.SetActive(true);
                instance.transform.position = pos;
                instance.transform.rotation = rot;
            }
        }
        if (instance == null) {
            instance = Object.Instantiate(prefab, pos, rot) as GameObject;
            instance.name = prefab.name + "@" + staticIDs++;
            GameObject root = GameMgr.GetInstance().GetServer<SceneMgr>().GetCurrentSceneRoot();
            instance.transform.parent = root.transform;
        }
        return instance;
    }

    public void DestroyGameObject(GameObject prefab, bool clear = false) {
        if (clear)
            GameObject.Destroy(prefab);
        else {
            prefab.SetActive(false);
            string originalPrefabName = prefab.name;
            if (prefab.name.IndexOf("@") >= 0) {
                originalPrefabName = prefab.name.Split('@')[0];
            }
            if (!cache.ContainsKey(originalPrefabName)) {
                List<GameObject> list = new List<GameObject>();
                list.Add(prefab);
                cache.Add(originalPrefabName, list);
            } else {
                List<GameObject> list = cache[originalPrefabName];
                list.Add(prefab);
            }
        }
    }

    public void InstanciateInitialObjects(PrebuildGameObjects gameObjects) {
        ClearCache();
        foreach (PrebuildGameObjects.CacheData cd in gameObjects.objectCache) {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < cd.cacheSize; ++i) {
                GameObject newObject = Object.Instantiate(cd.prefab, Vector3.zero, Quaternion.identity) as GameObject;
                newObject.name = cd.prefab.name + "@" + staticIDs++;
                newObject.SetActive(false);
                list.Add(newObject);
                GameObject root = GameMgr.GetInstance().GetServer<SceneMgr>().GetCurrentSceneRoot();
                newObject.transform.parent = root.transform;
            }
            cache.Add(cd.prefab.name, list);
        }
    }

    #endregion
}
