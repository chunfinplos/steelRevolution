using UnityEngine;
using System.Collections;

public static class GameMgrExtension {

    public static CustomMgrs GetCustomMgrs() {
        return GetCustomMgrsAux(GameMgr.GetInstance());
    }

    public static CustomMgrs GetCustomMgrsAux(this GameMgr gameMgr) {
        if(!gameMgr.IsCustomMgrInit()) {
            CustomMgrs customMgrs = new CustomMgrs(gameMgr);
            gameMgr.customMgrs = customMgrs;
        }
        return (CustomMgrs)gameMgr.customMgrs;
    }
}