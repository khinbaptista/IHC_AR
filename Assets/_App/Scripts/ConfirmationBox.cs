using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConfirmationBox : MonoBehaviour {

	public delegate void Callback();

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
	}
	
	void Update () {
	
	}

	public void Show(string message) {
		text.text = message;
		anim.SetBool("display", true);
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
		OnAnimationEnd();
	}
}
