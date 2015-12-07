using UnityEngine;
using System.Collections;

public class ObjectSelection : MonoBehaviour {
	public static ObjectSelection Instance;

	private Transform item;
	private float startScale;
	public float deltaScale = 0.3f;
	public float deltaTime = 0.3f;
	private float timer;

	private float alpha = 0;
	private bool growing = false;
	private bool shrinking = false;

	void Start() {
		Instance = this;
	}

	public static void SelectObject(Transform selected) {
		Instance.item = selected;
		Instance.Grow();
	}

	public static Transform LastSelectedObject() {
		return Instance.item;
	}

	public static void DeselectObject() {
		Instance.Shrink();
	}

	private void Grow() {
		if (item != null) {
			growing = true;
			startScale = item.localScale.x;
			timer = 0;
		}
	}

	private void Shrink() {
		if (item != null) {
			shrinking = true;
			startScale = item.localScale.x;
			timer = 0;
		}
	}

	void Update() {

		if (item != null) {
			if (growing) {
				alpha = Mathf.Clamp01(timer / deltaTime);
				timer += Time.deltaTime;

				float factor = Mathf.Lerp(startScale, startScale + deltaScale, alpha);
				item.localScale = new Vector3(factor, factor, factor);

				if (timer >= deltaTime) {
					growing = false;
				}

			}
			else if (shrinking) {
				alpha = Mathf.Clamp01(timer / deltaTime);
				timer += Time.deltaTime;

				if (timer >= deltaTime)
					growing = false;

				float factor = Mathf.Lerp(startScale + deltaScale, startScale, alpha);
				item.localScale = new Vector3(factor, factor, factor);
			}
		}

	}
}
