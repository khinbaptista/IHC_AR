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
		UIManager.Confirmation("Tem certeza que deseja sair?", OnConfirmExit, OnCancelExit);
	}

	public void OnInfoClick() {
		UIManager.Information("Trabalho em progresso", UIManager.ShowToolbox);
	}

	public void OnCatalogClick() {
		UIManager.ShowCatalog(UIManager.NewItemAdded);
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
