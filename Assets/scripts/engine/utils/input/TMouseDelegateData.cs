using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TMouseDelegateData {
    public AComponent component {
        get; private set;
    }
    private Dictionary<int, mouseDelegate> mouseDelegateMap;
    public int n {
        get; private set;
    }

    public TMouseDelegateData(AComponent component, int mCode, mouseDelegate firstDel) {
        this.component = component;
        mouseDelegateMap = new Dictionary<int, mouseDelegate>();
        mouseDelegateMap.Add(mCode, firstDel);
    }

    public void addDelegate(int mCode, mouseDelegate mDel) {
        if (mouseDelegateMap.ContainsKey(mCode)) {
            mouseDelegateMap[mCode] += mDel;
        } else {
            mouseDelegateMap.Add(mCode, mDel);
        }
        mouseDelegateMap[mCode] += mDel;
        n++;
    }

    public void removeDelegate(int mCode, mouseDelegate mDel) {
        mouseDelegateMap[mCode] -= mDel;
        n--;
    }

    public void callDelegate(int mCode, Dictionary<inputEvt, bool> buttonData) {
        if (mouseDelegateMap.ContainsKey(mCode)) {
            if (mouseDelegateMap[mCode] != null) {
                mouseDelegateMap[mCode](mCode, buttonData);
            }
        }
    }
}