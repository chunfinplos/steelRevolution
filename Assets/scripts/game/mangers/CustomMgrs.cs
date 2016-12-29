using UnityEngine;
using System.Collections;

public class CustomMgrs : GameMgr.ProjectSpecificMgrs {

    public GamePlayMgr gamePlayMgr { get; private set; }

    public CustomMgrs(GameMgr gameMgr) : base(gameMgr) {
        gamePlayMgr = new GamePlayMgr();
    }
}