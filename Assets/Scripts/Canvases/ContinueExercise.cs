using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueExercise : MonoBehaviour {

	public bool isContinuedAnimationToExercise = false;

	private Transform canvasListTransform;  // To grab "Canvas List" context

	void Start () {
		canvasListTransform = GameObject.Find ("/UI/Canvas List").transform;
	}

	// Update is called once per frame
	void Update () {
		if (this.isActiveAndEnabled) {
			if (!isContinuedAnimationToExercise) {
				isContinuedAnimationToExercise = true;
				GameObject.Find ("/ImageTarget/Beta")
					.GetComponent<BetaController> ().continueExerciseAnimation ();
				canvasListTransform.Find ("StopExerciseCanvas")
					.GetComponent<StopExercise> ().isStopedAnimationToExercise = false;
			}
		}
	}
}
