using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InformationBox : MonoBehaviour {

	static InformationBox instance;

	[SerializeField]
	private Text text;
	private Animator anim;
	private Callback onFinish;
	
	void Start () {
		anim = GetComponent<Animator>();

		if (anim == null)
			enabled = false;

		instance = this;
	}
	
	public static void Show(string message, Callback OnFinish = null) {
		instance.text.text = message;
		instance.anim.SetBool("display", true);

		instance.onFinish = OnFinish;
	}

	public void Hide() {
		anim.SetBool("display", false);
		UIManager.ShowMenu();
	}

	public void AnimationFinished() {
		if (onFinish != null)
			onFinish();
	}
}
