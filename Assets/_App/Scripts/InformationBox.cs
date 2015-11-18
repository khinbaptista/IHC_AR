using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InformationBox : MonoBehaviour {

	static InformationBox instance;

	[SerializeField]
	private Text text;
	private Animator anim;
	
	void Start () {
		anim = GetComponent<Animator>();

		if (anim == null)
			enabled = false;

		instance = this;
	}
	
	public static void Show(string message) {
		instance.text.text = message;
		instance.anim.SetBool("display", true);
	}

	public void Hide() {
		anim.SetBool("display", false);
		UIManager.ShowMenu();
	}

	public void AnimationFinished() {

	}
}
