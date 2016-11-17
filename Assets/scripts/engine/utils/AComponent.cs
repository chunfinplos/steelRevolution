using UnityEngine;

/// <summary>
/// A component. class to encapsulate all GameObjects
/// </summary>
public abstract class AComponent : MonoBehaviour {

    private int m_id;
    static int m_idCount = 0;

    protected virtual void Awake() {
        m_id = m_idCount++;
    }
    
    public int GetID() {
        return m_id;
    }

    protected virtual void Start() {}
    
    protected virtual void Update() {}

    protected virtual void OnDestroy() {}
}
