using UnityEngine;
using System.Collections;

public class ScanPrompt : MonoBehaviour {

	public AREventHandler vuforia;
	private Animator anim;

	bool shown = false;

	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update() {
		if (!shown) {
			shown = true;
			UIManager.ShowScan();
		}
	}

	public void OnClick() {
		UIManager.HideScan();
		vuforia.AddNewTrackableSource();
	}

	public void Show() {
		anim.SetBool("display", true);
	}

	public void Hide() {
		anim.SetBool("display", false);
	}
}
