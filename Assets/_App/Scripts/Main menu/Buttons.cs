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
		overlay.GetComponent<FadeIn>().StartFade();

		Application.LoadLevel(1);
	}

	public void OnQuitClick() {
		Debug.Log("Quit");
		Application.Quit();
	}
}
