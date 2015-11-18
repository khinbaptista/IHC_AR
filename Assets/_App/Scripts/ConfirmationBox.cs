using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfirmationBox : MonoBehaviour {

	static ConfirmationBox instance;

	public Callback OnConfirm;
	public Callback OnCancel;
	private Callback OnAnimationEnd;

	[SerializeField]
	private Text text;
	private Animator anim;

	void Start () {
		anim = GetComponent<Animator>();

		if (anim == null)
			enabled = false;

		instance = this;
	}
	
	void Update () {
	
	}

	static public void Show(string message, Callback onConfirm, Callback onCancel) {
		instance.text.text = message;

		instance.OnConfirm = onConfirm;
		instance.OnCancel = onCancel;
		instance.anim.SetBool("display", true);
	}
	
	public void OnConfirmClick() {
		OnAnimationEnd = OnConfirm;
		anim.SetBool("display", false);
	}

	public void OnCancelClick() {
		OnAnimationEnd = OnCancel;
		anim.SetBool("display", false);
	}

	public void AnimationFinished() {
		if (OnAnimationEnd != null)
			OnAnimationEnd();
	}
}
