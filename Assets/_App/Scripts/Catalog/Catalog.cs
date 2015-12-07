using UnityEngine;
using System.Collections;

public class Catalog : MonoBehaviour {
	private CatalogItem selected;
	private Animator anim;

	private Callback _onConfirm;
	private Callback _onCancel;

	private bool canceled;

	public bool Confirmed { get { return selected != null; } }
	
	private LayerMask objectsLayer;

	void Start () {
		anim = GetComponent<Animator>();
		objectsLayer = LayerMask.NameToLayer("Objects");
	}
	
	void Update () {
	
	}

	public void SetSelected(CatalogItem item) {
		if (selected != null)
			selected.SetSelection(false);
		
		selected = item;
		if (selected != null && selected.Item != null) {
			Debug.Log("Object layer " + objectsLayer.value);
			selected.Item.gameObject.layer = objectsLayer;
		}
	}

	public CatalogItem GetSelected() {
		return selected;
	}

	public void IgnoreSelected() {
		selected = null;
	}

	public void OnConfirm() {
		if (selected == null)
			return;

		UIManager.HideCatalog();
	}

	public void OnCancel() {
		if (selected != null) {
			selected.SetSelection(false);
			selected = null;
		}

		canceled = true;
		UIManager.HideCatalog();
	}

	public void Show(Callback onConfirm, Callback onCancel = null) {
		SetSelected(null);

		canceled = false;
		anim.SetBool("display", true);
		_onConfirm = onConfirm;
		_onCancel = onCancel;
	}

	public void Hide() {
		anim.SetBool("display", false);
	}

	public void OnCatalogShown() {

	}

	public void OnCatalogHidden() {
		if (canceled && _onCancel != null)
			_onCancel();
		else if (_onConfirm != null)
			_onConfirm();
	}
}
