﻿using UnityEngine;
using System.Collections;

public class GameMgr {

    private static GameMgr instance = null;

    private GameObject servers = null;

    public SpawnerMgr spawnerMgr { get; private set; }

    public static GameMgr GetInstance() {
        if(instance == null) {
            instance = new GameMgr();
        }
        return instance;
    }

    private GameMgr() {
        if(!servers) {
            servers = new GameObject("Servers");
            InputMgr inputMgr = AddServer<InputMgr>();
            SceneMgr sceneMgr = AddServer<SceneMgr>();
        }

        SceneMgr smAux = servers.GetComponent<SceneMgr>();
        spawnerMgr = new SpawnerMgr(smAux);
    }

    #region SERVERS

    protected T AddServer<T>() where T : Component {
        T t = servers.GetComponent<T>();
        if(t != null)
            Component.DestroyImmediate(t);

        t = servers.AddComponent<T>();
        return t;
    }

    protected bool RemoveServer<T>() where T : Component {
        T t = servers.GetComponent<T>();
        if(!t)
            return false;

        Component.Destroy(t);
        return true;
    }

    public T GetServer<T>() where T : Component {
        if(servers)
            return servers.GetComponent<T>();
        else
            return null;
    }

    #endregion
}
