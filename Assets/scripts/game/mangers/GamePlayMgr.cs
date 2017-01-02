using UnityEngine;
using System.Collections;

public class GamePlayMgr {

    public GameObject player { get; private set; }

    public GamePlayMgr() {
        player = GameObject.FindWithTag("Player");
    }
}
