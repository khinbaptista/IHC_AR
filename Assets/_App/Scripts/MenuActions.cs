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
		UIManager.Confirmation("Exit?", OnConfirmExit, OnCancelExit);
	}

	public void OnCatalogClick() {
		UIManager.ShowCatalog(null);
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
