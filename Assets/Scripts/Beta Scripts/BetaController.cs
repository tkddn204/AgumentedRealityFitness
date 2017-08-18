using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetaController : MonoBehaviour {

    public string exercise;

    private Animator animator;
    private AnimatorStateInfo currentAnimatorState;

    private Transform canvasListTransform;

    static int pushUpStateHash;
    static int sitUpStateHash;

    private string step = "Step";

    void Start() {
        animator = GetComponent<Animator>();
        canvasListTransform = GameObject.Find("/UI/Canvas List").transform;

        pushUpStateHash = Animator.StringToHash("push_up");
        sitUpStateHash = Animator.StringToHash("situps");
    }
    
    void FixedUpdate()
    {
        currentAnimatorState = animator.GetCurrentAnimatorStateInfo(0);
        if (currentAnimatorState.shortNameHash == pushUpStateHash ||
            currentAnimatorState.shortNameHash == sitUpStateHash)
        {
            canvasListTransform.Find("ContinueExerciseCanvas")
                .GetComponent<ContinueExerciseCanvas>().patnerCount
                = (int)currentAnimatorState.normalizedTime;
        }
    }

    public void readyExeciseAnimation() {
		animator.SetInteger (exercise, 1);
	}

	public void continueExerciseAnimation() {
		animator.SetInteger (exercise, 2);
	}

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
        animator.SetInteger (exercise, 0);
	}
}
