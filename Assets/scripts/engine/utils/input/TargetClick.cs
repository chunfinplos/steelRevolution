using UnityEngine;
using System.Collections;

public class TargetClick {
    private GameObject gameObject;
    private Vector3 point;
    private float distance;
    private Camera camera;

    public TargetClick(GameObject gameObject, Vector3 point, float distance, Camera camera) {
        this.gameObject = gameObject;
        this.point = point;
        this.distance = distance;
        this.camera = camera;
    }
}