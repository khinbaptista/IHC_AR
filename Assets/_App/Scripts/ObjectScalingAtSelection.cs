using UnityEngine;
using System.Collections;

public class ObjectScalingAtSelection : MonoBehaviour {
    public int scalingDuration;
    public float maximumScale;

    private int frameCounter;
    private float scalingStep;
    private bool scalingUp;
    private bool scalingDown;
    private bool scaled;

	void Start () {
        frameCounter = scalingDuration;
        scalingStep = maximumScale / scalingDuration;
        scalingUp = false;
        scalingDown = false;
        scaled = false;
	}
	
	void Update () {
        if (Input.GetMouseButtonDown(0) && !scaled) {
            setUpScaleIncreasement();
        } else if (Input.GetMouseButtonUp(0) && scaled) {
            setUpScaleDecreasement();
        }
        updateScale();
	}

    private void setUpScaleIncreasement() {
        frameCounter = 0;
        scalingUp = true;
        scaled = true;
    }

    private void setUpScaleDecreasement() {
        if (scalingUp) {
            print("here...");
            frameCounter = scalingDuration - frameCounter;
            scalingUp = false;
        } else {
            frameCounter = 0;
        }
        scalingDown = true;
    }

    private void updateScale() {
        if (frameCounter < scalingDuration) {
            if (scalingUp) {
                transform.localScale += new Vector3 (scalingStep, scalingStep, scalingStep);
            } else if (scalingDown) {
                transform.localScale -= new Vector3(scalingStep, scalingStep, scalingStep);
            }
            frameCounter++;
        } else {
            if (scalingDown) {
                scaled = false;
            }
            scalingUp = false;
            scalingDown = false;
        }
    }
}
