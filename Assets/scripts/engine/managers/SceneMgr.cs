using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Collections;

public delegate void OnSceneDestroy();
public delegate void OnAsyncLoadingProgress(float progress, bool finish);

public class SceneMgr : AComponent {

    protected struct TSceneInfo {
        public string name;
        public Dictionary<string, SubSceneInfo> subScenes;
    }

    protected struct LoadingAdditiveAsyncParam {
        public string sceneName;
        public bool inStack;

        public LoadingAdditiveAsyncParam(string a_s, bool a_i) {
            sceneName = a_s;
            inStack = a_i;
        }
    }

    /* Delegates */
    private OnSceneDestroy destroyDelegates = null;
    private OnAsyncLoadingProgress loadingDelegates = null;
    private Dictionary<string, OnAsyncLoadingProgress> asyncLoadingAditiveProgress = 
        new Dictionary<string, OnAsyncLoadingProgress>();

    /* Scenes */
    private GameObject cacheSceneRoot = null;
    private Stack<TSceneInfo> stackScenes = new Stack<TSceneInfo>();
    private Dictionary<string, GameObject> deactiveGOscenes = new Dictionary<string, GameObject>();

    /* SubScenes */
    private int numSubScenesLoading = 0;
    private string deferredSceneChange = "";

    /* Flags */
    private bool loadingAsync = false;

    #region MAIN

    protected override void Awake() {
        base.Awake();
        // Avoid destroy between scenes
        DontDestroyOnLoad(this);
        StoreLevelInfoInStack(SceneManager.GetActiveScene().name);
    }

    protected override void Update() {
        base.Update();

        if (numSubScenesLoading == 0) {
            if (deferredSceneChange != null && deferredSceneChange != "") {
                StartCoroutine("LoadingAsync", deferredSceneChange);
                deferredSceneChange = "";
            }
        }
    }

    #endregion MAIN

    #region STACK

    public void PushScene(string sceneName, bool asyn = false) {
        if (stackScenes.Count > 0) {
            TSceneInfo current = stackScenes.Peek();
            SuspendScene(current);
        }

        if (deactiveGOscenes.ContainsKey(sceneName)) {
            GameObject goScene = deactiveGOscenes[sceneName];
            StoreLevelInfoInStack(sceneName);
            goScene.SetActive(true);
            deactiveGOscenes.Remove(sceneName);
        } else {
            if (asyn) {
                numSubScenesLoading++;
                StartCoroutine("LoadingAdditiveAsync",
                                new LoadingAdditiveAsyncParam(sceneName, true));
            } else {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
                StoreLevelInfoInStack(sceneName);
            }
        }
    }

    protected TSceneInfo StoreLevelInfoInStack(string levelName) {
        TSceneInfo info = new TSceneInfo();
        info.name = levelName;
        info.subScenes = new Dictionary<string, SubSceneInfo>();
        stackScenes.Push(info);
        return info;
    }

    public int GetNumScenesStacked() {
        return stackScenes.Count;
    }

    public GameObject GetCurrentSceneRoot() {
        string rootName = stackScenes.Peek().name;
        if (cacheSceneRoot == null) {
            cacheSceneRoot = GameObject.Find(rootName);
        } else if (cacheSceneRoot.name != rootName) {
            cacheSceneRoot = GameObject.Find(rootName);
        }
        return cacheSceneRoot;
    }

    public string GetCurrentSceneName() {
        return stackScenes.Peek().name;
    }

    #endregion

    #region SCENES MANAGEMENT

    public bool IsLoadingFinish() {
        return !loadingAsync;
    }

    public void ChangeScene(string sceneName, string next = "") {
        stackScenes.Clear();
        
        //if (next != "" || next == null) {
        //    GameMgr.GetInstance().GetStorageMgr().SetVolatile(SCENE_SECTION, NEXT_SCENE, next);
        //}

        if (destroyDelegates != null) {
            destroyDelegates();
        }
        
        //GameMgr.GetInstance().GetServer<MemoryMgr>().GarbageRecolect(true);
        SceneManager.LoadScene(sceneName);
        StoreLevelInfoInStack(sceneName);
    }

    public void ChangeAsyncScene(string sceneName) {
        if (numSubScenesLoading == 0) {
            StartCoroutine("LoadingAsync", sceneName);
        } else {
            deferredSceneChange = sceneName;
        }
    }

    protected IEnumerator LoadingAsync(string sceneName) {
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        // Stop all loadings, this scene is mandatory
        loadingAsync = true;
        stackScenes.Clear();
        StoreLevelInfoInStack(sceneName);
        do {
            if (loadingDelegates != null) {
                loadingDelegates(op.progress, op.isDone);
            }
            yield return new WaitForEndOfFrame();
        } while (!op.isDone);
        loadingAsync = false;
    }

    protected IEnumerator LoadingAdditiveAsync(LoadingAdditiveAsyncParam param) {
        AsyncOperation op = SceneManager.LoadSceneAsync(param.sceneName, LoadSceneMode.Additive);
        do {
            if (asyncLoadingAditiveProgress.ContainsKey(param.sceneName)) {
                OnAsyncLoadingProgress progress = asyncLoadingAditiveProgress[param.sceneName];
                if (progress != null)
                    progress(op.progress, op.isDone);
            }
            yield return new WaitForEndOfFrame();
        } while (!op.isDone);

        if (param.inStack)
            StoreLevelInfoInStack(param.sceneName);
        else {
            StoreSubSceneInCurrentScene(param.sceneName);
        }
        numSubScenesLoading--;
        yield return null;
    }

    protected SubSceneInfo StoreSubSceneInCurrentScene(string subSceneName) {
        TSceneInfo info = stackScenes.Peek();
        SubSceneInfo subSceneInfo = new SubSceneInfo();
        subSceneInfo.m_name = subSceneName;
        subSceneInfo.m_active = true;
        if (info.subScenes.ContainsKey(subSceneInfo.m_name))
            info.subScenes[subSceneInfo.m_name] = subSceneInfo;
        else
            info.subScenes.Add(subSceneInfo.m_name, subSceneInfo);
        return subSceneInfo;
    }

    protected void SuspendScene(TSceneInfo sceneInfo) {
        foreach (SubSceneInfo ssinfo in sceneInfo.subScenes.Values) {
            if (ssinfo.m_active) {
                GameObject subSceneRoot = GameObject.Find(ssinfo.m_name);
                subSceneRoot.SetActive(false);
                deactiveGOscenes.Add(ssinfo.m_name, subSceneRoot);
                ssinfo.m_active = false;
            }
        }
        GameObject root = GameObject.Find(sceneInfo.name);
        if (!deactiveGOscenes.ContainsKey(sceneInfo.name))
            deactiveGOscenes.Add(sceneInfo.name, root);
        else
            deactiveGOscenes[sceneInfo.name] = root;
        root.SetActive(false);
    }

    public void ReturnScene(bool clearCurrentScene) {
        Assert.AbortIfNot(stackScenes.Count > 1, "Error, No hay escena a la cual volver");

        TSceneInfo current = stackScenes.Pop();
        if (clearCurrentScene)
            DestroyScene(current);
        else
            DeactiveScene(current);

        TSceneInfo previousScene = stackScenes.Peek();
        BackToLifeScene(previousScene);
    }

    protected void DestroyScene(TSceneInfo sceneInfo) {
        foreach (SubSceneInfo ssinfo in sceneInfo.subScenes.Values) {
            GameObject subSceneRoot = GameObject.Find(ssinfo.m_name);
            Destroy(subSceneRoot);
        }
        GameObject root = GameObject.Find(sceneInfo.name);
        if (destroyDelegates != null)
            destroyDelegates();
        Destroy(root);
    }

    protected void DeactiveScene(TSceneInfo sceneInfo) {
        foreach (SubSceneInfo ssinfo in sceneInfo.subScenes.Values) {
            GameObject subSceneRoot = GameObject.Find(ssinfo.m_name);
            Destroy(subSceneRoot);
        }
        sceneInfo.subScenes.Clear();
        GameObject root = GameObject.Find(sceneInfo.name);
        deactiveGOscenes.Add(sceneInfo.name, root);
        root.SetActive(false);
    }

    protected void BackToLifeScene(TSceneInfo sceneInfo) {
        foreach (SubSceneInfo ssinfo in sceneInfo.subScenes.Values) {
            if (!ssinfo.m_active) {
                GameObject subSceneRoot = deactiveGOscenes[ssinfo.m_name];
                subSceneRoot.SetActive(true);
                deactiveGOscenes.Remove(ssinfo.m_name);
                ssinfo.m_active = true;
            }
        }
        GameObject root = deactiveGOscenes[sceneInfo.name];
        deactiveGOscenes.Remove(sceneInfo.name);
        root.SetActive(true);
    }

    #endregion

    #region SUBSCENES

    public void LoadSubScene(string subScene, bool asyn = false) {
        if (!loadingAsync) {
            if (!stackScenes.Peek().subScenes.ContainsKey(subScene)) {
                if (deactiveGOscenes.ContainsKey(subScene)) {
                    GameObject goSubScene = deactiveGOscenes[subScene];
                    StoreSubSceneInCurrentScene(subScene);
                    goSubScene.SetActive(true);
                    deactiveGOscenes.Remove(subScene);
                } else {
                    if (asyn) {
                        numSubScenesLoading++;
                        StartCoroutine("LoadingAdditiveAsync",
                                       new LoadingAdditiveAsyncParam(subScene, false));
                    } else {
                        SceneManager.LoadScene(subScene, LoadSceneMode.Additive);
                        StoreSubSceneInCurrentScene(subScene);
                    }
                }
            } else {
                Debug.LogWarning("Subscene " + subScene + " already active in: " +
                                 stackScenes.Peek().name);
            }
        }
    }

    public void UnloadSubScene(string subScene, bool clearSubScene) {
        if (!loadingAsync) {
            Assert.AbortIfNot(stackScenes.Count > 0, "Error there is not any scene loaded.");

            TSceneInfo current = stackScenes.Peek();

            Assert.AbortIfNot(current.subScenes.ContainsKey(subScene), 
                              "Error: Subscene not exists: " + subScene);
            if (current.subScenes.ContainsKey(subScene)) {
                SubSceneInfo subSceneInfo = current.subScenes[subScene];
                GameObject subSceneRoot = GameObject.Find(subSceneInfo.m_name);
                if (clearSubScene) {
                    GameObject.Destroy(subSceneRoot);
                } else {
                    subSceneRoot.SetActive(false);
                    deactiveGOscenes.Add(subScene, subSceneRoot);
                }
                current.subScenes.Remove(subScene);
            } else {
                Debug.LogWarning("Can't remove subscene " + subScene + ", It is not active.");
            }
        }
    }

    #endregion

    #region DELEGATES

    public void RegisterDestroyDelegate(OnSceneDestroy cb) {
        destroyDelegates += cb;
    }

    public void UnRegisterDestroyDelegate(OnSceneDestroy cb) {
        destroyDelegates -= cb;
    }

    public void RegisterloadingDelegate(OnAsyncLoadingProgress cb) {
        loadingDelegates += cb;
    }

    public void UnRegisterloadingDelegate(OnAsyncLoadingProgress cb) {
        loadingDelegates -= cb;
    }

    public void RegisterLoadingAdditiveDelegate(string subScene, OnAsyncLoadingProgress cb) {
        if (!asyncLoadingAditiveProgress.ContainsKey(subScene)) {
            asyncLoadingAditiveProgress.Add(subScene, cb);
        } else {
            asyncLoadingAditiveProgress[subScene] += cb;
        }
    }

    public void uNRegisterLoadingAdditiveDelegate(string subScene, OnAsyncLoadingProgress cb) {
        if (!asyncLoadingAditiveProgress.ContainsKey(subScene)) {
            asyncLoadingAditiveProgress.Add(subScene, cb);
        } else {
            asyncLoadingAditiveProgress[subScene] += cb;
        }
    }

    #endregion
}
