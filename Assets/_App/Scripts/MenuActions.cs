using UnityEngine;
using System.Collections;

public class MenuActions : MonoBehaviour {

	private Animator anim;
	private bool backMainMenu;

	void Start() {
		anim = GetComponent<Animator>();

		backMainMenu = false;
		anim.SetBool("displayMenu", true);
	}

	public void OnCloseClick() {
		backMainMenu = true;

		anim.SetBool("displayMenu", false);
	}

	public void OnMenuShow() {

	}

	public void OnMenuHide() {
		if (backMainMenu)
			Application.LoadLevel(0);
	}
}
