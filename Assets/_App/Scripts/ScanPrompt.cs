using UnityEngine;
using System.Collections;

public class ScanPrompt : MonoBehaviour {

	public MenuActions menu;
	private Animator anim;

	bool shown = false;

	void Start () {
		anim = GetComponent<Animator>();
		//Display();
	}

	void Update() {
		if (!shown) {
			shown = true;
			Display();
		}
	}

	public void Display() {
		menu.Hide();
		anim.SetBool("display", true);
	}

	public void Hide() {
		anim.SetBool("display", false);
		menu.Show();
	}
}
