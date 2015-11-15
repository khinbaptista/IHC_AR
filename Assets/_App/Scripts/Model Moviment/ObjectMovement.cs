using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {
    private Vector3 lastMousePosition;

	void Start () {

	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0)) {
            updateObjectPosition();
        }
	}

    private void updateObjectPosition() {
        Vector3 movement = Input.mousePosition - lastMousePosition;
        movement.x = movement.x / 50;
        movement.z = movement.y / 50;
        movement.y = 0.0f;
        transform.position += movement;
        lastMousePosition = Input.mousePosition;
    }
}
