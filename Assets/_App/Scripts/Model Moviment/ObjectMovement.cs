using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {
    private Transform myTransform;              // this transform
    private Vector3 targetPosition;             // The destination Point
    private float targetDistance;               //Distance between Char and Poit of Ray
    private RaycastHit rayHit;                  //Stores the Information of the Raycasthit
    private Vector3 eP;
    private float rangeDistance;
 
    void Start () {
            myTransform = transform;
            targetPosition = myTransform.position;
            rangeDistance = 0.5f;
    }

    void Update() {
        // Moves the Player if the Right Mouse Button was clicked
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
            Plane playerPlane = new Plane(Vector3.up, myTransform.position);
            float hitdist = 0.0f;
            if (playerPlane.Raycast(ray, out hitdist)) {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                targetPosition = ray.GetPoint(hitdist);
                rangeDistance = 0.5f;
            }
            // keep track of the distance between this gameObject and targetPosition
            targetDistance = Vector3.Distance(targetPosition, myTransform.position);
            // Set the Movement according to the State
            if (targetDistance > rangeDistance) {
                transform.position = targetPosition;
            } else {
                targetPosition = transform.position;
            }
        }
    }
}
