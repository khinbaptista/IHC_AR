using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {
	private Image img;
	public Text text;

	public float duration = 1;
	public float timer;

	Color color;

	public delegate void Callback(int param);
	public Callback OnFadeEnd;
	private int param;

	void Start () {
		text.enabled = false;

		img = GetComponent<Image>();
		color = img.color;
		color.a = 0f;

		img.color = color;
	}
	
	public void StartFade(Callback callback, int param) {
		OnFadeEnd = callback;
		this.param = param;
		StartCoroutine(Fade());
	}

	public IEnumerator Fade() {
		yield return null;

		text.enabled = true;
		timer = 0f;

		while (timer <= duration) {
			timer += Time.deltaTime;
			color.a = Mathf.Clamp01((timer / duration));

			img.color = color;

			if (timer <= duration)
				yield return null;
		}

		if (OnFadeEnd != null)
			OnFadeEnd(param);
	}
}
