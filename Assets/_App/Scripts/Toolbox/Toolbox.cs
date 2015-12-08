using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ObjectSelection))]
public class Toolbox : MonoBehaviour {

	public enum Tool {
		None = 0, Move = 1, Rotate = 2, Scale = 3
	}

	[SerializeField][Tooltip("Positional reference")]
	private Transform target;
	private Animator anim;
	private Transform selectedItem;
	private Tool activeTool;
	
	private LayerMask objectsLayer;
	public Camera cameraAR;

	public float movementSpeed = 1f;
	public float rotationSpeed = 0.001f;
	public float idleTimeToDeselection = 5f;

	private float idleTimer;

	private Vector2 touchStart;
	private Vector2 deltaTouch;

	private Vector3 positionBeforeTouch;
	private Quaternion rotationBeforeTouch;
	private float scaleBeforeTouch;

    // Fields for movement raycast
    private Transform myTransform;              // this transform
    private Vector3 targetPosition;             // The destination Point
    private float targetDistance;               //Distance between Char and Poit of Ray
    private RaycastHit rayHit;                  //Stores the Information of the Raycasthit
    private Vector3 eP;
    private float rangeDistance;

	void Start () {
		anim = GetComponent<Animator>();
		objectsLayer = LayerMask.NameToLayer("Objects");
	}
	
	void Update () {
		//Debug.Log("Touch count: " + Input.touchCount);

		//	if (Input.touchCount == 0) {

		if (!Input.GetMouseButton(0)) {
			if (selectedItem != null)
				IdleTimer();
			return;
		}

		//Debug.Log("Touch me!");
		idleTimer = 0f;
		
		//Touch t = Input.GetTouch(0);

        if (selectedItem == null)
            Select(); //Select(t);

      //  else if (t.tapCount == 2)
      //      ClearSelection();

        else if (activeTool == Tool.Move)
        {
            //    Move(t);
            Move();
        }
        else if (activeTool == Tool.Rotate)
        {
            //     Rotate(t);
            Rotate();
        }
        else if (activeTool == Tool.Scale)
        {
            //     Scale(t);
            Scale();
        }
	}

	public void Show() {
		anim.SetBool("display", true);
	}

	public void Hide() {
		anim.SetBool("display", false);
	}

	public void NewItem(Transform item) {
		selectedItem = Instantiate(item);

		selectedItem.parent = target;
		selectedItem.localPosition = Vector3.zero;
		selectedItem.localRotation = Quaternion.identity;

		activeTool = Tool.None;
	}

	public void SetTool(int tool) {
		activeTool = (Tool)tool;
		Debug.Log("Tool set to " + activeTool.ToString());
	}

	public void Remove() {
		Destroy(selectedItem.gameObject);
		ClearSelection();
	}

	public void Switch() {
		Hide();
		UIManager.ShowCatalog(ItemSwitched, ()=> { UIManager.ShowToolbox(); });
	}

	public void ItemSwitched() {
		Vector3 localPosition = selectedItem.localPosition;
		Quaternion localRotation = selectedItem.localRotation;

		NewItem(UIManager.GetCatalogSelectedItem());

		selectedItem.localPosition = localPosition;
		selectedItem.localRotation = localRotation;
		Show();
	}

	private void Select(Touch t) {
		if (t.phase == TouchPhase.Began && t.tapCount == 1) {
			RaycastHit hit;
			Ray ray = cameraAR.ScreenPointToRay(new Vector3(t.position.x, t.position.y));

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, objectsLayer)) {
				selectedItem = hit.collider.gameObject.transform;
				ObjectSelection.SelectObject(selectedItem);
				UIManager.ShowToolbox();
			}
		}
	}

    private void Select() {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cameraAR.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, objectsLayer)) {
				selectedItem = hit.collider.gameObject.transform;
				ObjectSelection.SelectObject(selectedItem);
				UIManager.ShowToolbox();

				Debug.Log("Object was hit!");
			}
			else
				Debug.Log("Nothing hit...");
        }
    }

	private void ClearSelection() {
		selectedItem = null;
		activeTool = Tool.None;

		ObjectSelection.DeselectObject();
		UIManager.HideToolbox();
	}

    private void CalculateDeltaTouch() {
        if (Input.GetMouseButtonDown(0)) {
            touchStart = new Vector2(Input.mousePosition.x, Input.mousePosition.z);
        } else if (Input.GetMouseButton(0)) {
            deltaTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.z) - touchStart;
        }
    }

	private void CalculateDeltaTouch(Touch t) {
		switch (t.phase) {
			case TouchPhase.Began:
				touchStart = t.position;
				break;
			case TouchPhase.Moved:
				deltaTouch = t.position - touchStart;
				break;
			case TouchPhase.Ended:

				break;
			default: break;
		}
	}

	private void Move(Touch t) {
		if (t.phase == TouchPhase.Began)
			positionBeforeTouch = selectedItem.localPosition;

		CalculateDeltaTouch(t);
		
		Vector3 direction = Camera.main.transform.rotation * new Vector3(deltaTouch.x, 0, deltaTouch.y);
		selectedItem.localPosition = positionBeforeTouch + direction * movementSpeed;
	}

    private void Move() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
            myTransform = selectedItem.transform;
            targetPosition = myTransform.position;
            rangeDistance = 0.5f;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
                selectedItem.position = targetPosition;
            } else {
                targetPosition = selectedItem.position;
            }
        }


      /*  if (Input.GetMouseButtonDown(0))
            positionBeforeTouch = selectedItem.localPosition;

        print(positionBeforeTouch);

        CalculateDeltaTouch();

        Vector3 direction = Camera.main.transform.rotation * new Vector3(deltaTouch.x, 0, deltaTouch.y);
        selectedItem.localPosition = positionBeforeTouch + direction/50 * movementSpeed;*/
    }

	private void Rotate(Touch t) {
		if (t.phase == TouchPhase.Began)
			rotationBeforeTouch = selectedItem.localRotation;

		CalculateDeltaTouch(t);

		// if this causes weird rotations: a) check order of quaternion multiplication or; b) change angular speed to radians
		selectedItem.localRotation = Quaternion.AngleAxis(deltaTouch.x * rotationSpeed, Vector3.up) * rotationBeforeTouch;
	}

    private void Rotate() {
        if (Input.GetMouseButtonDown(0)) {
            touchStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        } else if (Input.GetMouseButton(0)) {
            touchStart = deltaTouch;
            deltaTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            rotationBeforeTouch = selectedItem.localRotation;
            if (deltaTouch.x > touchStart.x) {
                selectedItem.localRotation = Quaternion.AngleAxis(rotationSpeed, Vector3.up) * rotationBeforeTouch;
            } else if (deltaTouch.x < touchStart.x) {
                selectedItem.localRotation = Quaternion.AngleAxis(-rotationSpeed, Vector3.up) * rotationBeforeTouch;
            }
        }
    }

	private void Scale(Touch t) {
		if (t.phase == TouchPhase.Began)
			scaleBeforeTouch = selectedItem.localScale.x;

		CalculateDeltaTouch(t);

		float factor = deltaTouch.magnitude;
		selectedItem.localScale = new Vector3(factor, factor, factor);
    }

    private void Scale() {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
        else if (Input.GetMouseButton(0))
        {
            touchStart = deltaTouch;
            deltaTouch = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            //scaleBeforeTouch = selectedItem.localScale.x;
            if (deltaTouch.y > touchStart.y)
            {
                selectedItem.localScale = new Vector3(1.1f * selectedItem.localScale.x, 1.1f * selectedItem.localScale.y, 1.1f * selectedItem.localScale.z);
            }
            else if (deltaTouch.y < touchStart.y)
            {
                selectedItem.localScale = new Vector3(0.9f * selectedItem.localScale.x, 0.9f * selectedItem.localScale.y, 0.9f * selectedItem.localScale.z);
            }
        }
     //   if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) {
     //       scaleBeforeTouch = selectedItem.localScale.x;
//
//}
        //if (Input.GetMouseButtonDown(0))
       // scaleBeforeTouch = selectedItem.localScale.x;

       // CalculateDeltaTouch();

       // float factor = deltaTouch.magnitude;
       // selectedItem.localScale = new Vector3(factor, factor, factor);
    }

	private void IdleTimer() {
		idleTimer += Time.deltaTime;

		if (idleTimer >= idleTimeToDeselection) {
			idleTimer = 0f;
			ClearSelection();
		}
	}
}
