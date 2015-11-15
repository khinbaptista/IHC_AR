using UnityEngine;
using System.Collections;

public class MenuActions : MonoBehaviour {

	[SerializeField]
	private Catalog catalog;
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();
		Show();
	}

	public void OnCloseClick() {
		ConfirmationBox.Show("Exit?", OnConfirmExit, OnCancelExit);
		Hide();
	}

	public void OnCatalogClick() {
		catalog.Show(this.Show, this.Show);
		Hide();
	}

	public void Show() {
		anim.SetBool("displayMenu", true);

	}

	public void Hide() {
		anim.SetBool("displayMenu", false);
	}

	public void OnMenuShown() {

	}

	public void OnMenuHidden() {
		
	}

	public void OnConfirmExit() {
		Application.LoadLevel(0);
	}

	public void OnCancelExit() {
		Show();
	}
}
