using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaController : MonoBehaviour {

	public string exercise;

	private Animator animator;

	void Start () {
		animator = GetComponent<Animator> ();
	}

	void Update () {
	}

	public void readyExeciseAnimation() {
		animator.SetInteger (exercise, 1);
	}

	public void continueExerciseAnimation() {
		animator.SetInteger (exercise, 2);
	}

	public void stopExerciseAnimation() {
		animator.SetInteger (exercise, 0);
	}
}
