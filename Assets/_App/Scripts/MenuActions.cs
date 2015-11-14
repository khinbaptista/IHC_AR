using UnityEngine;
using System.Collections;

public class MenuActions : MonoBehaviour {
	private Animator anim;

	void Start() {
		anim = GetComponent<Animator>();

		anim.SetBool("displayMenu", true);
	}

	public void OnCloseClick() {
		ConfirmationBox.Show("Exit?", OnConfirmExit, OnCancelExit);
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
		anim.SetBool("displayMenu", true);
	}
}
