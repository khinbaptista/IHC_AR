using UnityEngine;
using System.Collections;

public class Toolbox : MonoBehaviour {

	public enum Tool
	{
		None = 0, Move = 1, Rotate = 2
	}

	[SerializeField][Tooltip("Positional reference")]
	private Transform reference;
	private Animator anim;
	private Transform selectedItem;
	private Tool activeTool;

	[SerializeField]
	private LayerMask objectsLayer;

	public float movementSpeed = 1f;
	public float rotationSpeed = 15f;
	public float idleTimeToDeselection = 5f;

	private float idleTimer;

	private Vector2 touchStart;
	private Vector2 deltaTouch;

	private Vector3 positionBeforeTouch;
	private Quaternion rotationBeforeTouch;

	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		if (Input.touchCount == 0 && selectedItem != null) {
			IdleTimer();
			return;
		}

		idleTimer = 0f;
		
		Touch t = Input.GetTouch(0);

		if (selectedItem == null)
			Select(t);

		else if (t.tapCount == 2)
			ClearSelection();

		else if (activeTool == Tool.Move)
			Move(t);
		else if (activeTool == Tool.Rotate)
			Rotate(t);
	}

	public void Show() {
		anim.SetBool("display", true);
	}

	public void Hide() {
		anim.SetBool("display", false);
	}

	public void NewItem(Transform item) {
		selectedItem = Instantiate(item);

		selectedItem.parent = reference;
		selectedItem.localPosition = Vector3.zero;
		selectedItem.localRotation = Quaternion.identity;

		activeTool = Tool.None;
	}

	public void SetTool(int tool) {
		activeTool = (Tool)tool;
		Debug.Log("Tool set to " + activeTool.ToString());
	}

	public void Switch() {
		Hide();
		UIManager.ShowCatalog(ItemSwitched, Show);
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
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(t.position.x, t.position.y));

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, objectsLayer)) {
				selectedItem = hit.collider.gameObject.transform;
				UIManager.ShowToolbox();
			}
		}
	}

	private void ClearSelection() {
		selectedItem = null;
		activeTool = Tool.None;
		UIManager.HideToolbox();
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

		// the following line does not take into account the position of the camera
		selectedItem.localPosition = positionBeforeTouch + new Vector3(deltaTouch.x, deltaTouch.y, 0) * movementSpeed;
	}

	private void Rotate(Touch t) {
		if (t.phase == TouchPhase.Began)
			rotationBeforeTouch = selectedItem.localRotation;

		CalculateDeltaTouch(t);

		// if this causes weird rotations: a) check order of quaternion multiplication or; b) change angular speed to radians
		selectedItem.localRotation = Quaternion.AngleAxis(deltaTouch.x * rotationSpeed, Vector3.up) * rotationBeforeTouch;
	}

	private void IdleTimer() {
		idleTimer += Time.deltaTime;

		if (idleTimer >= idleTimeToDeselection) {
			idleTimer = 0f;
			ClearSelection();
		}
	}
}
