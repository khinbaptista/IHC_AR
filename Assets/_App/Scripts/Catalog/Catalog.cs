using UnityEngine;
using System.Collections;

public class Catalog : MonoBehaviour {
	private CatalogItem selected;
	private Animator anim;

	public delegate void Callback();
	private Callback _onConfirm;
	private Callback _onCancel;

	private bool canceled;

	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void Update () {
	
	}

	public void SetSelected(CatalogItem item) {
		if (selected != null)
			selected.SetSelection(false);

		selected = item;
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

		Hide();
	}

	public void OnCancel() {
		if (selected != null) {
			selected.SetSelection(false);
			selected = null;
		}

		canceled = true;
		Hide();
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
