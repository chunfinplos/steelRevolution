using UnityEngine;
using System.Collections.Generic;

public class SpawnerMgr {
    private Dictionary<string, List<GameObject>> m_cache = new Dictionary<string, List<GameObject>>();
    //private SceneMgr m_sceneMgr;
    private static int m_staticIDs = 0;
}
