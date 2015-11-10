using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeIn : MonoBehaviour {
	private Image img;
	public Text text;

	public float duration = 1;
	public float timer;

	Color color;

	void Start () {
		text.enabled = false;

		img = GetComponent<Image>();
		color = img.color;
		color.a = 0f;

		img.color = color;
	}
	
	public void StartFade() {
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
	}
}
