using UnityEngine;

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

    protected virtual void OnDisable() {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(rb) {
            rb.ResetInertiaTensor();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}