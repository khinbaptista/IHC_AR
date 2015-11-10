using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraBackground : MonoBehaviour
{
	public RawImage image;

	private WebCamTexture webcamTexture;

    void Start() {
		webcamTexture = new WebCamTexture();
		//webcamTexture.requestedHeight = Screen.height;
		//webcamTexture.requestedWidth = Screen.width;

		image.texture = webcamTexture;
		image.material.mainTexture = webcamTexture;

		webcamTexture.Play();
	}

	public void PlayPause() {
		if (webcamTexture.isPlaying)
			webcamTexture.Pause();
		else
			webcamTexture.Play();
	}

	public void Stop() {
		webcamTexture.Stop();	
    }
}
