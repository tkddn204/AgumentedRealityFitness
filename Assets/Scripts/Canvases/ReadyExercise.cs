using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyExercise : MonoBehaviour {

	GameObject ContinueExercise;
	private Transform canvasListTransform;  // To grab "Canvas List" context

	public bool isReadiedAnimationToExercise = false;

	void Start() {
		canvasListTransform = GameObject.Find ("/UI/Canvas List").transform;
		ContinueExercise = canvasListTransform.Find ("ContinueExerciseCanvas/Text").gameObject;
	}

	// Update is called once per frame
	void Update () {
		if (this.isActiveAndEnabled) {
			ContinueExercise.GetComponent<ContinueExerciseText> ().Init ();
			if (!isReadiedAnimationToExercise) {
				isReadiedAnimationToExercise = true;
				GameObject.Find ("/ImageTarget/Beta")
					.GetComponent<BetaController> ().readyExeciseAnimation();
				canvasListTransform.Find ("ContinueExerciseCanvas")
					.GetComponent<ContinueExerciseCanvas> ().isContinuedAnimationToExercise = false;
			}
		}
	}
}
