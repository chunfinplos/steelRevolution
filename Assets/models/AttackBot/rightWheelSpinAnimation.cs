using UnityEngine;
using System.Collections;

public class rightWheelSpinAnimation : MonoBehaviour {
    
	void Start () {
	
	}
	
	void Update () {
        transform.Rotate(0, Time.deltaTime * -90, 0);
    }
}
