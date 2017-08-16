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

    private string step = "Step";
    public void nextStep()
    {
        animator.SetBool(step, true);
    }
    public void endStep()
    {
        animator.SetBool(step, false);
    }

	public void stopExerciseAnimation()
    {
        animator.SetBool(step, false);
        animator.SetInteger (exercise, 0);
	}
}
