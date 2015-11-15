using UnityEngine;
using System.Collections;

public class Catalog : MonoBehaviour {
	private CatalogItem selected;
	private Animator anim;

	public delegate void Callback();
	public Callback _onCancel;

	private bool canceled;

	void Start () {
		anim = GetComponent<Animator>();
	}
	
	void Update () {
	
	}

	public void SetSelected(CatalogItem item) {
		selected = item;
	}

	public CatalogItem GetSelected() {
		return selected;
	}

	public void OnConfirm() {
		if (selected == null)
			return;

		Hide();
	}

	public void OnCancel() {
		if (selected != null) {
			// diselect
			selected = null;
		}

		canceled = true;
		Hide();
	}

	public void Show(Callback onCancel = null) {
		anim.SetBool("display", true);
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
	}
}
