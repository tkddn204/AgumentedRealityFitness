using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopExercise : MonoBehaviour {

	public bool isStopedAnimationToExercise = false;

	private Transform canvasListTransform;  // To grab "Canvas List" context

	void Start () {
		canvasListTransform = GameObject.Find ("/UI/Canvas List").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.isActiveAndEnabled) {
			if (!isStopedAnimationToExercise) {
				isStopedAnimationToExercise = true;
				GameObject.Find ("/ImageTarget/Beta")
					.GetComponent<BetaController> ().stopExerciseAnimation ();
				canvasListTransform.Find ("ReadyExerciseCanvas")
					.GetComponent<ReadyExercise> ().isReadiedAnimationToExercise = false;
			}
		}
	}
}
