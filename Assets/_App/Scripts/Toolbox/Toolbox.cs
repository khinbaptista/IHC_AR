﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ObjectSelection))]
public class Toolbox : MonoBehaviour {

	public enum Tool
	{
		None = 0, Move = 1, Rotate = 2, Scale = 3
	}

	[SerializeField][Tooltip("Positional reference")]
	private Transform target;
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
	private float scaleBeforeTouch;

	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void Update () {
		Debug.Log("Touch count: " + Input.touchCount);

		if (Input.touchCount == 0) {
			if (selectedItem != null)
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
		else if (activeTool == Tool.Scale)
			Scale(t);
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
		Destroy(selectedItem);
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
			Ray ray = Camera.main.ScreenPointToRay(new Vector3(t.position.x, t.position.y));

			if (Physics.Raycast(ray, out hit, Mathf.Infinity, objectsLayer)) {
				selectedItem = hit.collider.gameObject.transform;
				ObjectSelection.SelectObject(selectedItem);
				UIManager.ShowToolbox();
			}
		}
	}

	private void ClearSelection() {
		selectedItem = null;
		activeTool = Tool.None;

		ObjectSelection.DeselectObject();
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
		
		Vector3 direction = Camera.main.transform.rotation * new Vector3(deltaTouch.x, 0, deltaTouch.y);
		selectedItem.localPosition = positionBeforeTouch + direction * movementSpeed;
	}

	private void Rotate(Touch t) {
		if (t.phase == TouchPhase.Began)
			rotationBeforeTouch = selectedItem.localRotation;

		CalculateDeltaTouch(t);

		// if this causes weird rotations: a) check order of quaternion multiplication or; b) change angular speed to radians
		selectedItem.localRotation = Quaternion.AngleAxis(deltaTouch.x * rotationSpeed, Vector3.up) * rotationBeforeTouch;
	}

	private void Scale(Touch t) {
		if (t.phase == TouchPhase.Began)
			scaleBeforeTouch = selectedItem.localScale.x;

		CalculateDeltaTouch(t);

		float factor = deltaTouch.magnitude;
		selectedItem.localScale = new Vector3(factor, factor, factor);
    }

	private void IdleTimer() {
		idleTimer += Time.deltaTime;

		if (idleTimer >= idleTimeToDeselection) {
			idleTimer = 0f;
			ClearSelection();
		}
	}
}