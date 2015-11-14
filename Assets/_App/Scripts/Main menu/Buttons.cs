using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Buttons : MonoBehaviour {
	public GameObject overlay;
	public CameraBackground bg;

	void Start() {
		overlay.SetActive(false);
	}

	public void OnStartClick() {
		Debug.Log("Start");
		bg.Stop();
		overlay.SetActive(true);
		overlay.GetComponent<FadeIn>().StartFade(AfterFade_Start);
	}

	public void OnQuitClick() {
		Debug.Log("Quit");

		overlay.SetActive(true);
		overlay.GetComponent<FadeIn>().StartFade(AfterFade_Quit);
	}

	public void AfterFade_Start() {
		Application.LoadLevel(1);
	}

	public void AfterFade_Quit() {
		Application.Quit();
	}
}
