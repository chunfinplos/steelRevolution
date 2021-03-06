﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TKeyDelegateData {
    public AComponent component {
        get; private set;
    }
    private Dictionary<KeyCode, keyDelegate> keyDelegateMap;
    public int n {
        get; private set;
    }

    public TKeyDelegateData(AComponent component, KeyCode kCode, keyDelegate firstDel) {
        this.component = component;
        keyDelegateMap = new Dictionary<KeyCode, keyDelegate>();
        keyDelegateMap.Add(kCode, firstDel);
    }

    public void addDelegate(KeyCode kCode, keyDelegate kDel) {
        if (keyDelegateMap.ContainsKey(kCode)) {
            keyDelegateMap[kCode] += kDel;
        } else {
            keyDelegateMap.Add(kCode, kDel);
        }
        n++;
    }

    public void removeDelegate(KeyCode kCode, keyDelegate kDel) {
        keyDelegateMap[kCode] -= kDel;
        n--;
    }

    public void callDelegate(KeyCode kCode, Dictionary<inputEvt, bool> keyData) {
        if (keyDelegateMap.ContainsKey(kCode)) {
            if (keyDelegateMap[kCode] != null) {
                keyDelegateMap[kCode](kCode, keyData);
            }
        }
    }
}